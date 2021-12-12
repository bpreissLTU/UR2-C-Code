using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UR2_FINAL_PROJECT_CODE_PREISS
{
    public partial class Form1 : Form
    {
        VideoCapture _capture;
        Thread _captureThread;
        SerialPort arduinoSerial = new SerialPort();
        Thread serialMonitoringThread;
        bool enableCoordinateSending = true;
        char shapeIdentifier;

        public Form1()
        {
            InitializeComponent();
        }

        /***************************************************************************/
        // Main Image Processing Function -
        // 
        /***************************************************************************/
        private void ProcessImage()
        {
            while(_capture.IsOpened)
            {
                // Create source Mat
                Mat sourceFrame = _capture.QueryFrame();

                // Resize to sourcePictureBox aspect ratio
                int newHeight = (sourceFrame.Size.Height * sourcePictureBox.Size.Width) / sourceFrame.Size.Width;
                Size newSize = new Size(sourcePictureBox.Size.Width, newHeight);
                CvInvoke.Resize(sourceFrame, sourceFrame, newSize);

                // Display the image in sourcePictureBox
                sourcePictureBox.Image = sourceFrame.Bitmap;

                // Create an image of the source frame (to be used when warping the image)
                Image<Bgr, byte> sourceFrameWarped = sourceFrame.ToImage<Bgr, byte>();

                // Isolate the ROI: Convert to grayscale and then apply binary threshold
                Image<Gray, byte> grayImage = sourceFrame.ToImage<Gray, byte>().ThresholdBinary(new Gray(125), new Gray(255));
                grayPictureBox.Image = grayImage.Bitmap;    // Shown for testing

                Image<Bgr, byte> detailedImage;

                using (VectorOfVectorOfPoint shapeContours = new VectorOfVectorOfPoint())
                using (VectorOfPoint approxContour = new VectorOfPoint())
                using (VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint())
                {
                    // Build a list of contours (based on the source binary image)
                    CvInvoke.FindContours(grayImage, contours, null, RetrType.List, ChainApproxMethod.ChainApproxSimple);

                    /**********************************************************************************************************************/
                    //
                    // Select the largest contour (To find white paper to warp with)

                    double maxArea = 0;
                    int chosen = 0;

                    for (int i = 0; i < contours.Size; i++)
                    {
                        VectorOfPoint contour = contours[i];
                        double area = CvInvoke.ContourArea(contour);

                        if (area > maxArea)
                        {
                            maxArea = area;
                            chosen = i;
                        }
                    }
                    // Get minimal rectangle that contains the contour
                    Rectangle boundingBox = CvInvoke.BoundingRectangle(contours[chosen]);

                    // Create a slightly larger bounding rectangle, we'll set it as the ROI for warping later
                    sourceFrameWarped.ROI = new Rectangle((int)Math.Min(0, boundingBox.X - 30), 
                        (int)Math.Min(0, boundingBox.Y - 30), (int)Math.Max(sourceFrameWarped.Width - 1, 
                        boundingBox.X + boundingBox.Width + 30), (int)Math.Max(sourceFrameWarped.Height - 1, 
                        boundingBox.Y + boundingBox.Height + 30));

                    // Warp the image & output it
                    warpedPictureBox.Image = WarpImage(sourceFrameWarped, contours[chosen]).Bitmap;
                    //
                    /**********************************************************************************************************************/
                    //
                    // Copy the warped image for adding detail later and create a binary threshold warped image
                    detailedImage = WarpImage(sourceFrameWarped, contours[chosen]);
                    Image<Gray, byte> grayWarpedImage = detailedImage.Convert<Gray, byte>().ThresholdBinary(new Gray(125), new Gray(255));

                    // Output warped binary image
                    binaryWarpedPictureBox.Image = grayWarpedImage.Bitmap;
                    //
                    /**********************************************************************************************************************/
                    //
                    // Build a list of contours (for the warped binary image)
                    CvInvoke.FindContours(grayWarpedImage, shapeContours, null, RetrType.List, ChainApproxMethod.ChainApproxSimple);

                    double shapeArea = 0;

                    for (int i = 0; i < shapeContours.Size; i++)
                    {
                        VectorOfPoint shapeContour = shapeContours[i];

                        // Identify TRIANGLES & SQUARES
                        CvInvoke.ApproxPolyDP(shapeContour, approxContour, CvInvoke.ArcLength(shapeContour, true) * 0.05, true);

                        shapeArea = CvInvoke.ContourArea(shapeContour); // Find the area of the shape

                        // Get an array of points in the contour
                        Point[] points = approxContour.ToArray();

                        // SQUARE
                        if (points.Length == 4 && shapeArea < 3000)
                        {
                            shapeIdentifier = 'S';

                            CvInvoke.Polylines(detailedImage, approxContour, true, new Bgr(Color.Blue).MCvScalar,5);

                            // Get smallest rectangle that contains the contour
                            Rectangle boundingBoxSquare = CvInvoke.BoundingRectangle(approxContour);

                            // Draw the bounding box on the display frame
                            CvInvoke.Rectangle(detailedImage, boundingBoxSquare, new Bgr(Color.Red).MCvScalar);

                            // Find the middle of the bounding box and place a dot
                            Point center = new Point(boundingBoxSquare.X + boundingBoxSquare.Width / 2, 
                                boundingBoxSquare.Y + boundingBoxSquare.Height / 2);
                            for (int x = 0; x < points.Length; x++)
                            {
                                CvInvoke.Circle(detailedImage, center, 1, new Bgr(Color.Green).MCvScalar, 2);
                            }
                            string shapeText = "SQUARE";
                            MarkDetectedObject(detailedImage, shapeContours[i], boundingBoxSquare, shapeArea, shapeText);

                            // Send coordinates to Arduino
                            if(enableCoordinateSending)
                            {
                                int xCoord = -1;
                                int yCoord = -1;
                                if (int.TryParse(center.X.ToString(), out xCoord) && int.TryParse(center.Y.ToString(), out yCoord))
                                {
                                    byte[] buffer = new byte[4] { Encoding.ASCII.GetBytes("<")[0], Convert.ToByte(xCoord), 
                                        Convert.ToByte(yCoord), Encoding.ASCII.GetBytes(">")[0] };
                                    arduinoSerial.Write(buffer, 0, 4);
                                    enableCoordinateSending = false;
                                }
                                else
                                {
                                    MessageBox.Show("Unable to parse coordinates.", "Shape: Square");
                                }
                            }
                           
                        }

                        // TRIANGLE
                        if (points.Length == 3 && shapeArea < 1500)
                        {
                            shapeIdentifier = 'T';

                            CvInvoke.Polylines(detailedImage, approxContour, true, new Bgr(Color.Yellow).MCvScalar, 5);

                            Rectangle boundingBoxTriangle = CvInvoke.BoundingRectangle(approxContour);

                            CvInvoke.Rectangle(detailedImage, boundingBoxTriangle, new Bgr(Color.Blue).MCvScalar);

                            Point center = new Point(boundingBoxTriangle.X + boundingBoxTriangle.Width / 2, 
                                boundingBoxTriangle.Y + boundingBoxTriangle.Height / 2);
                            for (int x = 0; x < points.Length; x++)
                            {
                                CvInvoke.Circle(detailedImage, center, 1, new Bgr(Color.Green).MCvScalar, 2);
                            }
                            string shapeText = "TRIANGLE";
                            MarkDetectedObject(detailedImage, shapeContours[i], boundingBoxTriangle, shapeArea, shapeText);

                            // Send coordinates to Arduino
                            if (enableCoordinateSending)
                            {
                                int xCoord = -1;
                                int yCoord = -1;
                                if (int.TryParse(center.X.ToString(), out xCoord) && int.TryParse(center.Y.ToString(), out yCoord))
                                {
                                    byte[] buffer = new byte[5] { Encoding.ASCII.GetBytes("<")[0], Convert.ToByte(xCoord),
                                        Convert.ToByte(yCoord), Convert.ToByte(shapeIdentifier),Encoding.ASCII.GetBytes(">")[0] };
                                    arduinoSerial.Write(buffer, 0, 5);
                                    enableCoordinateSending = false;
                                }
                                else
                                {
                                    MessageBox.Show("Unable to parse coordinates.", "Shape: Square");
                                }
                            }
                        }
                    }

                    // Output detailed image
                    detailedPictureBox.Image = detailedImage.Bitmap;
                }
            }
        }

        /***************************************************************************/
        // Image Warping Function -
        // 
        /***************************************************************************/
        private static Image<Bgr, Byte> WarpImage(Image<Bgr, byte> frame, VectorOfPoint contour)
        {
            // Set the output size
            var size = new Size(frame.Width, frame.Height);
            
            using (VectorOfPoint approxContour = new VectorOfPoint())
            {
                CvInvoke.ApproxPolyDP(contour, approxContour, CvInvoke.ArcLength(contour, true) * 0.05, true);

                // Get an array of points in the contour
                Point[] points = approxContour.ToArray();

                // If array length isn't 4, something went wrong, abort warping process
                if (points.Length != 4)
                {
                    return frame;
                }

                IEnumerable<Point> query = points.OrderBy(point => point.Y).ThenBy(point => point.X);
                PointF[] ptsSrc = new PointF[4];
                PointF[] ptsDst = new PointF[] { new PointF(0, 0), new PointF(size.Width - 1, 0), new PointF(0, size.Height - 1), new PointF(size.Width - 1, size.Height - 1) };

                for (int i = 0; i < 4; i++)
                {
                    ptsSrc[i] = new PointF(query.ElementAt(i).X, query.ElementAt(i).Y);
                }

                using (var matrix = CvInvoke.GetPerspectiveTransform(ptsSrc, ptsDst))
                {
                    using (var cutImagePortion = new Mat())
                    {
                        CvInvoke.WarpPerspective(frame, cutImagePortion, matrix, size, Inter.Cubic);

                        return cutImagePortion.ToImage<Bgr, Byte>();//.Flip((FlipType)1);  // Flip the image horizontally (over the y - axis)
                    }
                }
            }
        }

        /***************************************************************************/
        // Mark ROI Function -
        //
        // Marks the ROI with a bounding box.
        /***************************************************************************/
        private static void MarkDetectedObject(Image<Bgr, byte> frame, VectorOfPoint contour, Rectangle boundingBox, double area, string shape)
        {
            // Write information next to marked object
            Point center = new Point(boundingBox.X + boundingBox.Width / 2, boundingBox.Y + boundingBox.Height / 2);
            var info = new string[] { $"Area: {area}", $"Position: {center.X}, {center.Y}", $"{shape}" };
            WriteMultilineText(frame, info, new Point(center.X, boundingBox.Bottom + 12));
        }

        /***************************************************************************/
        // Write Text Function -
        //
        // Writes text underneath ROI.
        /***************************************************************************/
        private static void WriteMultilineText(Image<Bgr, byte> frame, string[] lines, Point origin)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                int y = i * 10 + origin.Y;  // Moving down on each line
                CvInvoke.PutText(frame, lines[i], new Point(origin.X - 25, y), FontFace.HersheyPlain, 0.5, new Bgr(Color.Red).MCvScalar);
            }
        }

        private void MonitorSerialData()
        {
            while(true)
            {
                // Block until \n character is received, extract the command data

                string msg = arduinoSerial.ReadLine();

                // Confirm the string has both < and > characters
                if(msg.IndexOf("<") == -1 || msg.IndexOf(">") == -1)
                {
                    continue;
                }

                // Remove everything before (and including) the '<' character
                msg = msg.Substring(msg.IndexOf("<") + 1);

                // Remove everything after (and including) the '>' character
                msg = msg.Remove(msg.IndexOf(">"));

                // If the resulting string is empty, disregard and move on
                if (msg.Length == 0)
                {
                    continue;
                }

                // Parse the command
                if (msg.Substring(0, 1) == "S")
                {
                    // Command is to suspend, toggle states accordingly
                    ToggleFieldAvailability(msg.Substring(1, 1) == "1");
                }
                else if (msg.Substring(0, 1) == "P")
                {
                    // Command is to display the point data, output to the text field
                    Invoke(new Action(() =>
                    {
                        coordsTextBox.Text = $"{msg.Substring(1)}";
                    }));
                }
            }
        }

        private void ToggleFieldAvailability(bool suspend)
        {
            Invoke(new Action(() =>
            {
                enableCoordinateSending = !suspend;
                lockStateToolStripStatusLabel.Text = $"State:{(suspend ? "Locked" : "Unlocked")}";
            }));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Create the capture object and processing thread
            _capture = new VideoCapture(1);
            _captureThread = new Thread(ProcessImage);
            _captureThread.Start();

            try
            {
                arduinoSerial.PortName = "COM5";
                arduinoSerial.BaudRate = 9600;
                arduinoSerial.Open();
                serialMonitoringThread = new Thread(MonitorSerialData);
                serialMonitoringThread.Start();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Initializing COM Port");
                Close();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _captureThread.Abort();
            serialMonitoringThread.Abort();
        }
    }
}

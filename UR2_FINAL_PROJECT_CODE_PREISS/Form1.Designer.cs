
namespace UR2_FINAL_PROJECT_CODE_PREISS
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.sourcePictureBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.warpedPictureBox = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.binaryWarpedPictureBox = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.grayPictureBox = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.detailedPictureBox = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.coordsTextBox = new System.Windows.Forms.TextBox();
            this.lockStateToolStripStatusLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.thetaOneTextBox = new System.Windows.Forms.TextBox();
            this.thetaTwoTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.sourcePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.warpedPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.binaryWarpedPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grayPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.detailedPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // sourcePictureBox
            // 
            this.sourcePictureBox.Location = new System.Drawing.Point(16, 15);
            this.sourcePictureBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.sourcePictureBox.Name = "sourcePictureBox";
            this.sourcePictureBox.Size = new System.Drawing.Size(281, 240);
            this.sourcePictureBox.TabIndex = 0;
            this.sourcePictureBox.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 258);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Source Image:";
            // 
            // warpedPictureBox
            // 
            this.warpedPictureBox.Location = new System.Drawing.Point(305, 15);
            this.warpedPictureBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.warpedPictureBox.Name = "warpedPictureBox";
            this.warpedPictureBox.Size = new System.Drawing.Size(281, 240);
            this.warpedPictureBox.TabIndex = 2;
            this.warpedPictureBox.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(301, 258);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Warped Image:";
            // 
            // binaryWarpedPictureBox
            // 
            this.binaryWarpedPictureBox.Location = new System.Drawing.Point(305, 278);
            this.binaryWarpedPictureBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.binaryWarpedPictureBox.Name = "binaryWarpedPictureBox";
            this.binaryWarpedPictureBox.Size = new System.Drawing.Size(281, 240);
            this.binaryWarpedPictureBox.TabIndex = 4;
            this.binaryWarpedPictureBox.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(301, 522);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(158, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Warped Image (Binary):";
            // 
            // grayPictureBox
            // 
            this.grayPictureBox.Location = new System.Drawing.Point(16, 278);
            this.grayPictureBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grayPictureBox.Name = "grayPictureBox";
            this.grayPictureBox.Size = new System.Drawing.Size(281, 240);
            this.grayPictureBox.TabIndex = 6;
            this.grayPictureBox.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 522);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(139, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Gray Image (Binary):";
            // 
            // detailedPictureBox
            // 
            this.detailedPictureBox.Location = new System.Drawing.Point(677, 15);
            this.detailedPictureBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.detailedPictureBox.Name = "detailedPictureBox";
            this.detailedPictureBox.Size = new System.Drawing.Size(281, 240);
            this.detailedPictureBox.TabIndex = 8;
            this.detailedPictureBox.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(673, 258);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "Detailed Image:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(673, 311);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(108, 17);
            this.label6.TabIndex = 10;
            this.label6.Text = "Current Coords:";
            // 
            // coordsTextBox
            // 
            this.coordsTextBox.Location = new System.Drawing.Point(789, 311);
            this.coordsTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.coordsTextBox.Name = "coordsTextBox";
            this.coordsTextBox.Size = new System.Drawing.Size(261, 22);
            this.coordsTextBox.TabIndex = 11;
            // 
            // lockStateToolStripStatusLabel
            // 
            this.lockStateToolStripStatusLabel.AutoSize = true;
            this.lockStateToolStripStatusLabel.Location = new System.Drawing.Point(852, 258);
            this.lockStateToolStripStatusLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lockStateToolStripStatusLabel.Name = "lockStateToolStripStatusLabel";
            this.lockStateToolStripStatusLabel.Size = new System.Drawing.Size(198, 17);
            this.lockStateToolStripStatusLabel.TabIndex = 12;
            this.lockStateToolStripStatusLabel.Text = "lockStateToolStripStatusLabel";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(673, 365);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 17);
            this.label7.TabIndex = 13;
            this.label7.Text = "thetaOne:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(671, 413);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 17);
            this.label8.TabIndex = 14;
            this.label8.Text = "thetaTwo:";
            // 
            // thetaOneTextBox
            // 
            this.thetaOneTextBox.Location = new System.Drawing.Point(752, 360);
            this.thetaOneTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.thetaOneTextBox.Name = "thetaOneTextBox";
            this.thetaOneTextBox.Size = new System.Drawing.Size(132, 22);
            this.thetaOneTextBox.TabIndex = 15;
            // 
            // thetaTwoTextBox
            // 
            this.thetaTwoTextBox.Location = new System.Drawing.Point(752, 408);
            this.thetaTwoTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.thetaTwoTextBox.Name = "thetaTwoTextBox";
            this.thetaTwoTextBox.Size = new System.Drawing.Size(132, 22);
            this.thetaTwoTextBox.TabIndex = 16;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.thetaTwoTextBox);
            this.Controls.Add(this.thetaOneTextBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lockStateToolStripStatusLabel);
            this.Controls.Add(this.coordsTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.detailedPictureBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.grayPictureBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.binaryWarpedPictureBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.warpedPictureBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sourcePictureBox);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sourcePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.warpedPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.binaryWarpedPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grayPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.detailedPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox sourcePictureBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox warpedPictureBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox binaryWarpedPictureBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox grayPictureBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox detailedPictureBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox coordsTextBox;
        private System.Windows.Forms.Label lockStateToolStripStatusLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox thetaOneTextBox;
        private System.Windows.Forms.TextBox thetaTwoTextBox;
    }
}


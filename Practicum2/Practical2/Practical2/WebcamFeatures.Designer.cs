namespace Practical2
{
    partial class WebcamFeatures
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
            this.components = new System.ComponentModel.Container();
            this.box = new Emgu.CV.UI.ImageBox();
            this.OpenButton = new System.Windows.Forms.Button();
            this.RecordButton = new System.Windows.Forms.Button();
            this.lbl_RCTime = new System.Windows.Forms.Label();
            this.ResetButton = new System.Windows.Forms.Button();
            this.ViewButton = new System.Windows.Forms.Button();
            this.WindowsSwitch = new System.Windows.Forms.CheckBox();
            this.EyesSwitch = new System.Windows.Forms.CheckBox();
            this.DetectionboxSwitch = new System.Windows.Forms.CheckBox();
            this.ColorButton = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            ((System.ComponentModel.ISupportInitialize)(this.box)).BeginInit();
            this.SuspendLayout();
            // 
            // box
            // 
            this.box.Location = new System.Drawing.Point(12, 83);
            this.box.Name = "box";
            this.box.Size = new System.Drawing.Size(640, 480);
            this.box.TabIndex = 2;
            this.box.TabStop = false;
            // 
            // OpenButton
            // 
            this.OpenButton.Location = new System.Drawing.Point(93, 48);
            this.OpenButton.Name = "OpenButton";
            this.OpenButton.Size = new System.Drawing.Size(75, 23);
            this.OpenButton.TabIndex = 3;
            this.OpenButton.Text = "Open";
            this.OpenButton.UseVisualStyleBackColor = true;
            this.OpenButton.Click += new System.EventHandler(this.OpenButton_Click);
            // 
            // RecordButton
            // 
            this.RecordButton.Location = new System.Drawing.Point(12, 48);
            this.RecordButton.Name = "RecordButton";
            this.RecordButton.Size = new System.Drawing.Size(75, 23);
            this.RecordButton.TabIndex = 5;
            this.RecordButton.Text = "Record";
            this.RecordButton.UseVisualStyleBackColor = true;
            this.RecordButton.Click += new System.EventHandler(this.RecordButton_Click);
            // 
            // lbl_RCTime
            // 
            this.lbl_RCTime.AutoSize = true;
            this.lbl_RCTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_RCTime.ForeColor = System.Drawing.Color.Red;
            this.lbl_RCTime.Location = new System.Drawing.Point(425, 22);
            this.lbl_RCTime.Name = "lbl_RCTime";
            this.lbl_RCTime.Size = new System.Drawing.Size(0, 16);
            this.lbl_RCTime.TabIndex = 6;
            // 
            // ResetButton
            // 
            this.ResetButton.Location = new System.Drawing.Point(174, 48);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(75, 23);
            this.ResetButton.TabIndex = 7;
            this.ResetButton.Text = "Reset";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // ViewButton
            // 
            this.ViewButton.Location = new System.Drawing.Point(12, 15);
            this.ViewButton.Name = "ViewButton";
            this.ViewButton.Size = new System.Drawing.Size(156, 23);
            this.ViewButton.TabIndex = 9;
            this.ViewButton.Text = "Switch to Face Detection";
            this.ViewButton.UseVisualStyleBackColor = true;
            this.ViewButton.Click += new System.EventHandler(this.ViewButton_Click);
            // 
            // WindowsSwitch
            // 
            this.WindowsSwitch.AutoSize = true;
            this.WindowsSwitch.Location = new System.Drawing.Point(12, 48);
            this.WindowsSwitch.Name = "WindowsSwitch";
            this.WindowsSwitch.Size = new System.Drawing.Size(65, 17);
            this.WindowsSwitch.TabIndex = 10;
            this.WindowsSwitch.Text = "Regions";
            this.WindowsSwitch.UseVisualStyleBackColor = true;
            this.WindowsSwitch.Visible = false;
            this.WindowsSwitch.CheckedChanged += new System.EventHandler(this.WindowsSwitch_CheckedChanged);
            // 
            // EyesSwitch
            // 
            this.EyesSwitch.AutoSize = true;
            this.EyesSwitch.Location = new System.Drawing.Point(93, 48);
            this.EyesSwitch.Name = "EyesSwitch";
            this.EyesSwitch.Size = new System.Drawing.Size(49, 17);
            this.EyesSwitch.TabIndex = 11;
            this.EyesSwitch.Text = "Eyes";
            this.EyesSwitch.UseVisualStyleBackColor = true;
            this.EyesSwitch.Visible = false;
            this.EyesSwitch.CheckedChanged += new System.EventHandler(this.EyesSwitch_CheckedChanged);
            // 
            // DetectionboxSwitch
            // 
            this.DetectionboxSwitch.AutoSize = true;
            this.DetectionboxSwitch.Location = new System.Drawing.Point(174, 48);
            this.DetectionboxSwitch.Name = "DetectionboxSwitch";
            this.DetectionboxSwitch.Size = new System.Drawing.Size(93, 17);
            this.DetectionboxSwitch.TabIndex = 12;
            this.DetectionboxSwitch.Text = "Detection Box";
            this.DetectionboxSwitch.UseVisualStyleBackColor = true;
            this.DetectionboxSwitch.Visible = false;
            this.DetectionboxSwitch.CheckedChanged += new System.EventHandler(this.DetectionboxSwitch_CheckedChanged);
            // 
            // ColorButton
            // 
            this.ColorButton.Location = new System.Drawing.Point(12, 48);
            this.ColorButton.Name = "ColorButton";
            this.ColorButton.Size = new System.Drawing.Size(156, 23);
            this.ColorButton.TabIndex = 13;
            this.ColorButton.Text = "Select Tracker Color";
            this.ColorButton.UseVisualStyleBackColor = true;
            this.ColorButton.Visible = false;
            this.ColorButton.Click += new System.EventHandler(this.ColorButton_Click);
            // 
            // WebcamFeatures
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 573);
            this.Controls.Add(this.ColorButton);
            this.Controls.Add(this.DetectionboxSwitch);
            this.Controls.Add(this.EyesSwitch);
            this.Controls.Add(this.WindowsSwitch);
            this.Controls.Add(this.ViewButton);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.lbl_RCTime);
            this.Controls.Add(this.RecordButton);
            this.Controls.Add(this.OpenButton);
            this.Controls.Add(this.box);
            this.Name = "WebcamFeatures";
            this.Text = "Video Mode";
            ((System.ComponentModel.ISupportInitialize)(this.box)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Emgu.CV.UI.ImageBox box;
        private System.Windows.Forms.Button OpenButton;
        private System.Windows.Forms.Button RecordButton;
        private System.Windows.Forms.Label lbl_RCTime;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.Button ViewButton;
        private System.Windows.Forms.CheckBox WindowsSwitch;
        private System.Windows.Forms.CheckBox EyesSwitch;
        private System.Windows.Forms.CheckBox DetectionboxSwitch;
        private System.Windows.Forms.Button ColorButton;
        private System.Windows.Forms.ColorDialog colorDialog1;
    }
}


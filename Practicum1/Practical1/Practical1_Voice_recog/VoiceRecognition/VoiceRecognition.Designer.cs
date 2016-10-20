namespace VoiceControl
{
    partial class VoiceRecognition
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
            this.hearBox = new System.Windows.Forms.TextBox();
            this.SwitchButton = new System.Windows.Forms.Button();
            this.VolumeSwitch = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TextSpeed = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.MouseSpeed = new System.Windows.Forms.NumericUpDown();
            this.SpeakerButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.VolumeSwitch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MouseSpeed)).BeginInit();
            this.SuspendLayout();
            // 
            // hearBox
            // 
            this.hearBox.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hearBox.Location = new System.Drawing.Point(12, 258);
            this.hearBox.Multiline = true;
            this.hearBox.Name = "hearBox";
            this.hearBox.Size = new System.Drawing.Size(332, 124);
            this.hearBox.TabIndex = 4;
            // 
            // SwitchButton
            // 
            this.SwitchButton.Location = new System.Drawing.Point(23, 12);
            this.SwitchButton.Name = "SwitchButton";
            this.SwitchButton.Size = new System.Drawing.Size(145, 23);
            this.SwitchButton.TabIndex = 5;
            this.SwitchButton.Text = "Mouse Movement";
            this.SwitchButton.UseVisualStyleBackColor = true;
            this.SwitchButton.Click += new System.EventHandler(this.SwitchButton_Click);
            // 
            // VolumeSwitch
            // 
            this.VolumeSwitch.Location = new System.Drawing.Point(98, 58);
            this.VolumeSwitch.Name = "VolumeSwitch";
            this.VolumeSwitch.Size = new System.Drawing.Size(70, 20);
            this.VolumeSwitch.TabIndex = 6;
            this.VolumeSwitch.Value = new decimal(new int[] {
            70,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Volume";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Text Speed";
            // 
            // TextSpeed
            // 
            this.TextSpeed.Location = new System.Drawing.Point(98, 84);
            this.TextSpeed.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.TextSpeed.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.TextSpeed.Name = "TextSpeed";
            this.TextSpeed.Size = new System.Drawing.Size(70, 20);
            this.TextSpeed.TabIndex = 6;
            this.TextSpeed.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Mouse Speed";
            // 
            // MouseSpeed
            // 
            this.MouseSpeed.Location = new System.Drawing.Point(98, 110);
            this.MouseSpeed.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.MouseSpeed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MouseSpeed.Name = "MouseSpeed";
            this.MouseSpeed.Size = new System.Drawing.Size(70, 20);
            this.MouseSpeed.TabIndex = 6;
            this.MouseSpeed.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.MouseSpeed.ValueChanged += new System.EventHandler(this.MouseSpeed_ValueChanged);
            // 
            // SpeakerButton
            // 
            this.SpeakerButton.Location = new System.Drawing.Point(13, 229);
            this.SpeakerButton.Name = "SpeakerButton";
            this.SpeakerButton.Size = new System.Drawing.Size(75, 23);
            this.SpeakerButton.TabIndex = 8;
            this.SpeakerButton.Text = "Speak";
            this.SpeakerButton.UseVisualStyleBackColor = true;
            this.SpeakerButton.Click += new System.EventHandler(this.SpeakerButton_Click);
            // 
            // VoiceRecognition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(356, 488);
            this.Controls.Add(this.SpeakerButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MouseSpeed);
            this.Controls.Add(this.TextSpeed);
            this.Controls.Add(this.VolumeSwitch);
            this.Controls.Add(this.SwitchButton);
            this.Controls.Add(this.hearBox);
            this.Name = "VoiceRecognition";
            this.Text = "Voice Recognition";
            ((System.ComponentModel.ISupportInitialize)(this.VolumeSwitch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MouseSpeed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox hearBox;
        private System.Windows.Forms.Button SwitchButton;
        private System.Windows.Forms.NumericUpDown VolumeSwitch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown TextSpeed;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown MouseSpeed;
        private System.Windows.Forms.Button SpeakerButton;
    }
}


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
            this.speakButton = new System.Windows.Forms.Button();
            this.hearButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // hearBox
            // 
            this.hearBox.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hearBox.Location = new System.Drawing.Point(12, 103);
            this.hearBox.Multiline = true;
            this.hearBox.Name = "hearBox";
            this.hearBox.Size = new System.Drawing.Size(332, 124);
            this.hearBox.TabIndex = 4;
            // 
            // button1
            // 
            this.speakButton.Location = new System.Drawing.Point(13, 26);
            this.speakButton.Name = "button1";
            this.speakButton.Size = new System.Drawing.Size(75, 23);
            this.speakButton.TabIndex = 5;
            this.speakButton.Text = "Speak";
            this.speakButton.UseVisualStyleBackColor = true;
            this.speakButton.Click += new System.EventHandler(this.speakButton_Click);

            // 
            // button2
            // 
            this.hearButton.Location = new System.Drawing.Point(13, 56);
            this.hearButton.Name = "button2";
            this.hearButton.Size = new System.Drawing.Size(75, 23);
            this.hearButton.TabIndex = 6;
            this.hearButton.Text = "Hear";
            this.hearButton.UseVisualStyleBackColor = true;
            this.hearButton.Click += new System.EventHandler(this.hearButton_Click);
            // 
            // VoiceRecognition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(356, 296);
            this.Controls.Add(this.hearButton);
            this.Controls.Add(this.speakButton);
            this.Controls.Add(this.hearBox);
            this.Name = "VoiceRecognition";
            this.Text = "VoiceControl";
            this.Load += new System.EventHandler(this.VoiceRecognition_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox hearBox;
        private System.Windows.Forms.Button speakButton;
        private System.Windows.Forms.Button hearButton;
    }
}


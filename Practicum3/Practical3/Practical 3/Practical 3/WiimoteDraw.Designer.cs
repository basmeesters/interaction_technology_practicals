namespace Practical_3_Template
{
    partial class WiimoteDraw
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
            this.pencilbutton = new System.Windows.Forms.Button();
            this.eraserbutton = new System.Windows.Forms.Button();
            this.circlebutton = new System.Windows.Forms.Button();
            this.squarebutton = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drawpanel = new Practical_3_Template.DBPanel();
            this.linebutton = new System.Windows.Forms.Button();
            this.colorbutton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pencilbutton
            // 
            this.pencilbutton.Location = new System.Drawing.Point(12, 27);
            this.pencilbutton.Name = "pencilbutton";
            this.pencilbutton.Size = new System.Drawing.Size(75, 45);
            this.pencilbutton.TabIndex = 0;
            this.pencilbutton.Text = "Brush";
            this.pencilbutton.UseVisualStyleBackColor = true;
            this.pencilbutton.Click += new System.EventHandler(this.pencilbutton_Click);
            // 
            // eraserbutton
            // 
            this.eraserbutton.Location = new System.Drawing.Point(12, 78);
            this.eraserbutton.Name = "eraserbutton";
            this.eraserbutton.Size = new System.Drawing.Size(75, 45);
            this.eraserbutton.TabIndex = 1;
            this.eraserbutton.Text = "Eraser";
            this.eraserbutton.UseVisualStyleBackColor = true;
            this.eraserbutton.Click += new System.EventHandler(this.eraserbutton_Click);
            // 
            // circlebutton
            // 
            this.circlebutton.Location = new System.Drawing.Point(12, 180);
            this.circlebutton.Name = "circlebutton";
            this.circlebutton.Size = new System.Drawing.Size(75, 45);
            this.circlebutton.TabIndex = 2;
            this.circlebutton.Text = "Circle";
            this.circlebutton.UseVisualStyleBackColor = true;
            this.circlebutton.Click += new System.EventHandler(this.circlebutton_Click);
            // 
            // squarebutton
            // 
            this.squarebutton.Location = new System.Drawing.Point(12, 231);
            this.squarebutton.Name = "squarebutton";
            this.squarebutton.Size = new System.Drawing.Size(75, 45);
            this.squarebutton.TabIndex = 3;
            this.squarebutton.Text = "Rectangle";
            this.squarebutton.UseVisualStyleBackColor = true;
            this.squarebutton.Click += new System.EventHandler(this.squarebutton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitbutton_Click);
            // 
            // drawpanel
            // 
            this.drawpanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.drawpanel.BackColor = System.Drawing.Color.White;
            this.drawpanel.Location = new System.Drawing.Point(103, 27);
            this.drawpanel.Name = "drawpanel";
            this.drawpanel.Size = new System.Drawing.Size(1200, 700);
            this.drawpanel.TabIndex = 5;
            this.drawpanel.Paint += new System.Windows.Forms.PaintEventHandler(this.drawpanel_Paint);
            // 
            // linebutton
            // 
            this.linebutton.Location = new System.Drawing.Point(12, 129);
            this.linebutton.Name = "linebutton";
            this.linebutton.Size = new System.Drawing.Size(75, 45);
            this.linebutton.TabIndex = 0;
            this.linebutton.Text = "Line";
            this.linebutton.UseVisualStyleBackColor = true;
            this.linebutton.Click += new System.EventHandler(this.linebutton_Click);
            // 
            // colorbutton
            // 
            this.colorbutton.Location = new System.Drawing.Point(12, 282);
            this.colorbutton.Name = "colorbutton";
            this.colorbutton.Size = new System.Drawing.Size(75, 45);
            this.colorbutton.TabIndex = 0;
            this.colorbutton.Text = "Select Color";
            this.colorbutton.UseVisualStyleBackColor = true;
            this.colorbutton.Click += new System.EventHandler(this.colorbutton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 507);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 45);
            this.button1.TabIndex = 6;
            this.button1.Text = "Switch to Drum";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // WiimoteDraw
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.colorbutton);
            this.Controls.Add(this.linebutton);
            this.Controls.Add(this.drawpanel);
            this.Controls.Add(this.squarebutton);
            this.Controls.Add(this.circlebutton);
            this.Controls.Add(this.eraserbutton);
            this.Controls.Add(this.pencilbutton);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "WiimoteDraw";
            this.Text = "Wiimote Draw";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button pencilbutton;
        private System.Windows.Forms.Button eraserbutton;
        private System.Windows.Forms.Button circlebutton;
        private System.Windows.Forms.Button squarebutton;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Button linebutton;
        private Practical_3_Template.DBPanel drawpanel;
        private System.Windows.Forms.Button colorbutton;
        private System.Windows.Forms.Button button1;
    }
}


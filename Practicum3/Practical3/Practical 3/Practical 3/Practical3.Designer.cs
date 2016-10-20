using System;
using System.Drawing;
using System.Windows.Forms;

namespace Practical_3_Template
{
    partial class Practical3
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Move along, nothing to see here
        DateTime prev;
        float dt;
        BufferedGraphics graphics;
        BufferedGraphicsContext context;

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

        private void OnResize(object sender, EventArgs e)
        {
            // Re-create the graphics buffer for a new window size.
            context.MaximumBuffer = new Size(this.Width + 1, this.Height + 1);
            if (graphics != null)
            {
                graphics.Dispose();
                graphics = null;
            }
            graphics = context.Allocate(this.CreateGraphics(),
                new Rectangle(0, 0, this.Width, this.Height));
            graphics.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Globals.Graphics = graphics.Graphics;
            this.Refresh();
        }

        private void Init()
        {
            prev = DateTime.Now;
            DoubleBuffered = true;
            context = BufferedGraphicsManager.Current;
            context.MaximumBuffer = new Size(this.Width + 1, this.Height + 1);
            graphics = context.Allocate(this.CreateGraphics(),
                 new Rectangle(0, 0, this.Width, this.Height));
            graphics.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Globals.Graphics = graphics.Graphics;
            this.Resize += new EventHandler(OnResize);
            this.Paint += new PaintEventHandler(OnPaint);
        }

        void CalculateDT()
        {
            TimeSpan s = DateTime.Now.Subtract(prev);
            dt = (float)s.TotalSeconds;
            prev = DateTime.Now;
        }

        void OnPaint(object sender, PaintEventArgs e)
        {
            
            //graphics.Render(graphics.Graphics);
        }
        #endregion

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnChange = new System.Windows.Forms.Button();
            this.tmrUpdate = new System.Windows.Forms.Timer(this.components);
            this.label0 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnChange
            // 
            this.btnChange.Location = new System.Drawing.Point(13, 13);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(75, 23);
            this.btnChange.TabIndex = 0;
            this.btnChange.Text = "Drumming";
            this.btnChange.UseVisualStyleBackColor = true;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // tmrUpdate
            // 
            this.tmrUpdate.Enabled = true;
            this.tmrUpdate.Interval = 20;
            this.tmrUpdate.Tick += new System.EventHandler(this.Update_Tick);
            // 
            // label0
            // 
            this.label0.AutoSize = true;
            this.label0.Location = new System.Drawing.Point(727, 13);
            this.label0.Name = "label0";
            this.label0.Size = new System.Drawing.Size(77, 13);
            this.label0.TabIndex = 1;
            this.label0.Text = "X1 Coordinate:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(727, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "X2 Coordinate:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(727, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Y1 Coordinate:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(727, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Y2 Coordinate:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(727, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Y2 Coordinate:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 662);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label0);
            this.Controls.Add(this.btnChange);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Practical3_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnChange;
        private System.Windows.Forms.Timer tmrUpdate;
        private Label label0;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
    }
}


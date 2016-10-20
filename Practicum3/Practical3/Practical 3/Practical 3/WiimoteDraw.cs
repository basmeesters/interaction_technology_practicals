using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Practical_3_Template
{
    public partial class WiimoteDraw : Form
    {
        Boolean painting = false;        

        Pen pen;
        Rectangle squarey;
        Color color = Color.Black;
        int size = 3;
        Point startpoint;
        Point endpoint;
        string drawingtool = "pencil";
        Bitmap currentdrawing;
        Bitmap temp;

        // To handle the threads
        public delegate void UpdatePanelCallback();

        public WiimoteDraw()
        {
            InitializeComponent();
            temp = new Bitmap(drawpanel.ClientRectangle.Width, drawpanel.ClientRectangle.Height);
            this.Cursor = System.Windows.Forms.Cursors.Cross;
            Globals.WiiMote.WiimoteChanged += drawpanel_WiiMove;
        }

        private void drawpanel_Paint(object sender, PaintEventArgs e)
        {
            //Check if the user is trying to draw
            if (painting)
            {
                //Determine which drawing tool was selected
                size = (Globals.WiiMote.WiimoteState.IRState.IRSensors[0].Size + Globals.WiiMote.WiimoteState.IRState.IRSensors[0].Size) * 2;
                switch (drawingtool)
                {
                    //Allows the user to freely draw shapes with a pencil tool
                    case "pencil":
                        if (currentdrawing != null)
                        {
                            //creates new graphics using the stored image
                            Graphics gr = Graphics.FromImage(currentdrawing);
                            pen = new Pen(color, size);
                            gr.DrawLine(pen, new Point(startpoint.X - 150, startpoint.Y - 150), new Point(endpoint.X - 150, endpoint.Y - 150));
                            pen.Dispose();
                            e.Graphics.DrawImageUnscaled(currentdrawing, 0, 0);
                            gr.Dispose();
                            //Sets the end point to the start point to ensure the drawing of a continuus line
                            startpoint.X = endpoint.X;
                            startpoint.Y = endpoint.Y;
                        }
                        break;
                    
                    //Does the same as the pencil, except in white
                    case "eraser":
                        if (currentdrawing != null)
                        {
                            //creates new graphics using the stored image
                            Graphics gr = Graphics.FromImage(currentdrawing);
                            pen = new Pen(Color.White, size);
                            gr.DrawLine(pen, startpoint, endpoint);
                            pen.Dispose();
                            e.Graphics.DrawImageUnscaled(currentdrawing, 0, 0);
                            //Sets the end point to the start point to ensure the drawing of a continuus line
                            startpoint.X = endpoint.X;
                            startpoint.Y = endpoint.Y;
                            gr.Dispose();
                        }
                        break;
                    
                    //Allows the user to draw straight lines in any direction
                    case "line":
                        if (currentdrawing != null)
                        {
                            //Remembers the old image so that the temporary circle does not last
                            currentdrawing = (Bitmap)temp.Clone();
                            Graphics gr = Graphics.FromImage(currentdrawing);
                            pen = new Pen(color, size);
                            gr.DrawLine(pen, startpoint, endpoint);
                            pen.Dispose();
                            e.Graphics.DrawImageUnscaled(currentdrawing, 0, 0);
                            gr.Dispose();                            
                        }
                        break;

                    //Allows the user to draw rectangles in any direction
                    case "square":
                        if (currentdrawing != null)
                        {
                            //Remembers the old image so that the temporary circle does not last
                            currentdrawing = (Bitmap)temp.Clone();
                            Graphics gr = Graphics.FromImage(currentdrawing);
                            pen = new Pen(color, size);
                            squarey = new Rectangle(new Point(Math.Min(endpoint.X, startpoint.X), Math.Min(endpoint.Y, startpoint.Y)),
                                             new Size(Math.Max(endpoint.X, startpoint.X) - squarey.X, Math.Max(endpoint.Y, startpoint.Y) - squarey.Y));
                            gr.DrawRectangle(pen, squarey);
                            pen.Dispose();
                            e.Graphics.DrawImageUnscaled(currentdrawing, 0, 0);
                            gr.Dispose();
                        }
                        break;

                    //Allows the user to draw circles in any direction
                    case "circle":
                        if (currentdrawing != null)
                        {
                            //Remembers the old image so that the temporary circle does not last
                            currentdrawing = (Bitmap)temp.Clone();
                            Graphics gr = Graphics.FromImage(currentdrawing);
                            pen = new Pen(color, size);
                            squarey = new Rectangle(new Point(Math.Min(endpoint.X, startpoint.X), Math.Min(endpoint.Y, startpoint.Y)),
                                             new Size(Math.Max(endpoint.X, startpoint.X) - squarey.X, Math.Max(endpoint.Y, startpoint.Y) - squarey.Y));
                            gr.DrawEllipse(pen, squarey);
                            pen.Dispose();
                            e.Graphics.DrawImageUnscaled(currentdrawing, 0, 0);
                            gr.Dispose();
                        }
                        break;

                    default:
                        break;
                }          
               
                              
            }
        }

        // Techniques needed to simulate a left mouse click
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        public const int LeftMouseDown = 0x02;
        public const int LeftMouseUp = 0x04;

        // This simulates a left mouse click
        public static void LeftMouseClick(int x, int y)
        {
            mouse_event(LeftMouseDown, x, y, 0, 0);
            mouse_event(LeftMouseUp, x, y, 0, 0);
        }
         
        // Method which is called every the wiimote changes state
        private void drawpanel_WiiMove(object sender, EventArgs e)
        {
            // Calculate the position of the wiimote, needs some optimizing though
            PointF p = new PointF();
            p.X = 1.3f * (this.Width - Globals.WiiMote.WiimoteState.IRState.Midpoint.X * this.Width) - 180f;
            p.Y = this.Location.Y + Globals.WiiMote.WiimoteState.IRState.Midpoint.Y * this.Height;
            Cursor.Position = Point.Ceiling(p);

            // If the sensor does not sense the wiimote anymore stop painting
            if (!Globals.WiiMote.WiimoteState.IRState.IRSensors[0].Found || !Globals.WiiMote.WiimoteState.IRState.IRSensors[1].Found)
            {
                painting = false;
                if (currentdrawing != null)
                    temp = (Bitmap)currentdrawing.Clone();
            }

            // Simulate mouseclick
            if (Globals.WiiMote.WiimoteState.ButtonState.Down == true)
                LeftMouseClick(Cursor.Position.X, Cursor.Position.Y);

            // Start drawing
            if (Globals.WiiMote.WiimoteState.ButtonState.A == true && painting == false )
            {
                painting = true;
                startpoint = Point.Ceiling(p);
                currentdrawing = (Bitmap)temp.Clone();
            }

            // Stop drawing
            else if (Globals.WiiMote.WiimoteState.ButtonState.B == true && painting == true)
            {
                painting = false;
                temp = (Bitmap)currentdrawing.Clone();  
            }
            
            // Don't use wiimote anymore
            else if (Globals.WiiMote.WiimoteState.ButtonState.One)
            {
                Globals.WiiMote.WiimoteChanged -= drawpanel_WiiMove;
            }

            // Make sure the threads are handled well
            if (painting)
            {
                endpoint = Point.Ceiling(p);

                //Makes sure the user is kept up to date on what they are drawing
                if (drawpanel.InvokeRequired)
                    drawpanel.Invoke(new UpdatePanelCallback(delegate()
                    {
                        drawpanel.Invalidate();
                        drawpanel.Update();
                    }));
            }            
        }

        //set tool to pencil
        private void pencilbutton_Click(object obj, EventArgs ea)
        {
            drawingtool = "pencil";
        }

        //set tool to eraser
        private void eraserbutton_Click(object obj, EventArgs ea)
        {
            drawingtool = "eraser";
        }

        //set tool to circle
        private void circlebutton_Click(object obj, EventArgs ea)
        {
            drawingtool = "circle";
        }

        //set tool to rectangle
        private void squarebutton_Click(object obj, EventArgs ea)
        {
            drawingtool = "square";
        }

        //set tool to line
        private void linebutton_Click(object sender, EventArgs e)
        {
            drawingtool = "line";
        }

        //allows the user to pick a colour to draw with and sets the button background color to that color
        private void colorbutton_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                color = colorDialog1.Color;
                colorbutton.BackColor = color;
                if (color == Color.Black)
                    colorbutton.ForeColor = Color.White;
                if (color == Color.White)
                    colorbutton.ForeColor = Color.Black;
            }
        }

        //closes the application
        private void exitbutton_Click(object obj, EventArgs ea)
        {
            foreach (Form form in Application.OpenForms)
                form.Close();
            Globals.Form.Close();
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Globals.Form = Globals.Practical3;
            Globals.Form.Show();
        }

        


        

        

        

        

        

    }
}

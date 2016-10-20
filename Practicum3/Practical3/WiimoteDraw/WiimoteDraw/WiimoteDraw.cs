using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
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

        
        

        public WiimoteDraw()
        {
            InitializeComponent();
            temp = new Bitmap(drawpanel.ClientRectangle.Width, drawpanel.ClientRectangle.Height);
            this.Cursor = System.Windows.Forms.Cursors.Cross;
        }

        private void drawpanel_Paint(object sender, PaintEventArgs e)
        {
            //Check if the user is trying to draw
            if (painting)
            {
                //Determine which drawing tool was selected
                switch (drawingtool)
                {
                    //Allows the user to freely draw shapes with a pencil tool
                    case "pencil":
                        if (currentdrawing != null)
                        {      
                            //creates new graphics using the stored image
                            Graphics gr = Graphics.FromImage(currentdrawing);
                            pen = new Pen(color, size);
                            gr.DrawLine(pen, startpoint, endpoint);
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
             

        
        //start painting
        private void drawpanel_MouseDown(object sender, MouseEventArgs e)
        {
            painting = true;
            //Stores coordinates of where the user first clicked
            startpoint.X = e.X;
            startpoint.Y = e.Y;
            currentdrawing = (Bitmap)temp.Clone();
        }

        private void drawpanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (painting)
            {
                endpoint.X = e.X;
                endpoint.Y = e.Y;
                //Makes sure the user is kept up to date on what they are drawing
                drawpanel.Invalidate();
                drawpanel.Update();
            }

                  
        }

        //upon releasing the mouse button, reset parameters
        private void drawpanel_MouseUp(object sender, MouseEventArgs e)
        {
            painting = false;
            temp = (Bitmap)currentdrawing.Clone();  
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
            this.Close();
        }


        

        

        

        

        

    }
}

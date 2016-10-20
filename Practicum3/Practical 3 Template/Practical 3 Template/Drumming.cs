using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;



namespace Practical_3_Template
{
    class Drumming
    {
        // This is just an example, you can empty all the methods

        PointF Position;
        List<Drum> drums;

        public Drumming()
        {
            
        }

        public void CreateExampleDrums()
        {
            drums = new List<Drum>();

            // Create drums
            Drum d = new Drum(20, 400, 300, Color.Red, @"drum1.wav");
            drums.Add(d);

            d = new Drum(380, 400, 230, Color.Orange, @"drum2.wav");
            drums.Add(d);

            d = new Drum(660, 400, 330, Color.Yellow, @"drum3.wav");
            drums.Add(d);

        }

        // Put your update loop here.
        public void Update(float x, float y)
        {
            Position.X = x;
            Position.Y = y;
            
        }

        public void checkPosition()
        {
            float distanceX, distanceY;
            foreach (Drum d in drums)
            {
                distanceX = System.Math.Abs(d.Location.X - Position.X);
                distanceY = System.Math.Abs(d.Location.Y - Position.Y);
                if (distanceX < 50.0 && distanceY < 50.0)
                    d.Sound();
            }
        }

        public void Draw(float dt)
        {
            // Create the graphics object so we can draw
            Graphics g = Globals.Graphics;

            // Clear the screen
            g.Clear(Color.CornflowerBlue);
            g.FillEllipse(Brushes.Black, Position.X, Position.Y, 50, 50);

            // Draw each drum
            foreach (Drum d in drums)
            {
                d.Draw(g);
                //d.Sound();
            }
        }

        public void Drum()
        {
            foreach (Drum d in drums)
            {
                d.Sound();
            }
        }

        
    }
}

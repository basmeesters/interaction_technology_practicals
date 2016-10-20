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
        PointF Position;

        float accelX, accelY;
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

            d = new Drum(380, 400, 230, Color.Orange, @"drum3.wav");
            drums.Add(d);

            d = new Drum(660, 400, 330, Color.Yellow, @"drum2.wav");
            drums.Add(d);

        }

        // Put your update loop here.
        public void Update(float x, float y, float accelx, float accely)
        {
            Position.X = x;
            Position.Y = y;
            accelX = accelx;
            accelY = accely;   
        }

        public void checkPosition()
        {
            foreach (Drum d in drums)
            {
                if (Position.X > d.Location.X && 
                    Position.X < d.Location.X + d.Width && 
                    accelY > 0.2f) // x and y accel are good
                {
                    d.Sound();
                }
            }
        }

        public void Draw(int size)
        {
            // Create the graphics object so we can draw
            Graphics g = Globals.Graphics;

            // Clear the screen
            g.Clear(Color.CornflowerBlue);
            g.FillEllipse(Brushes.Black, Position.X, Position.Y, 20 + size * 10, 20 + size * 10);

            // Draw each drum
            foreach (Drum d in drums)
            {
                d.Draw(g);
            }
        }
        
    }

    public class Drum : UserControl
    {
        int posX, posY, size;
        Color color;
        string soundLocation;
        public Drum(int x, int y, int size, Color c, string s)
        {
            this.Location = new Point(x, y);
            this.Size = new Size(size, 100);
            this.posX = x;
            this.posY = y;
            this.size = size;
            this.color = c;
            this.soundLocation = s;
        }

        public void Draw(Graphics g)
        {
            SolidBrush brush = new SolidBrush(color);
            g.FillRectangle(brush, posX, posY, size, 3);

        }

        public void Sound()
        {
            using (var soundplayer = new System.Media.SoundPlayer(soundLocation)) { soundplayer.Play(); }
        }

    }
}

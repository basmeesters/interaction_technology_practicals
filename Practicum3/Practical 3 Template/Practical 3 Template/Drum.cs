using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Media;

namespace Practical_3_Template
{
    public partial class Drum : UserControl
    {
        int x, y, size;
        Color c;
        SoundPlayer player;
        Button b;
        string sound;
        public Drum(int x, int y, int size, Color c, string s)
        {
            this.Location = new Point(x, y);
            this.Size = new Size(size, 100);
            this.x = x;
            this.y = y;
            this.size = size;
            this.c = c;
            this.sound = s;
            //Graphics g = CreateGraphics();
            //Draw(g, c, size);
        }

        public void Draw(Graphics g)
        {
            SolidBrush brush = new SolidBrush(c);
            g.FillRectangle(brush, x, y, size, 3);

        }

        public void Sound()
        {
            using (var soundplayer = new System.Media.SoundPlayer(sound)) { soundplayer.Play(); }
        }

    }
}

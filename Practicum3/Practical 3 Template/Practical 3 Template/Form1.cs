using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WiimoteLib;

namespace Practical_3_Template
{
    public partial class Form1 : Form
    {
        // Init
        public Form1()
        {
            InitializeComponent();
            Init();
        }

        // Load
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Practical 2 - Drumming and IR Tracking";
            Globals.state = State.Drumming;
            Globals.Drumming.CreateExampleDrums();
            Globals.Form = this;
        }	

        // Change State
        private void btnChange_Click(object sender, EventArgs e)
        {
            if (Globals.state == State.IRTracking)
            {
                btnChange.Text = "Drumming";
                Globals.state = State.Drumming;
            }
            else
            {
                btnChange.Text = "IR Tracking";
                Globals.state = State.IRTracking;
            }
        }

        private void Update_Tick(object sender, EventArgs e)
        {
            // Calculates the time difference between the last frame and the current frame
            CalculateDT(); 
            switch (Globals.state)
            {
                case State.Drumming:
                    //Globals.Drumming.Update(Globals.WiiMote.WiimoteState.AccelState.Values.X, Globals.WiiMote.WiimoteState.AccelState.Values.Y);
                    Globals.Drumming.Update(this.Width - Globals.WiiMote.WiimoteState.IRState.IRSensors[0].Position.X * 1000, 
                                            Globals.WiiMote.WiimoteState.IRState.IRSensors[0].Position.Y * 1000);
                    Globals.Drumming.Draw(dt);
                    Globals.Drumming.checkPosition();
                    //Globals.Drumming.Drum();
                    break;
                case State.IRTracking:
                    Globals.IRTracking.Update(dt);
                    Globals.IRTracking.Draw(dt);
                    break;
            }

            // Renders the screen
            graphics.Render(Graphics.FromHwnd(this.Handle));
        }
   }
}

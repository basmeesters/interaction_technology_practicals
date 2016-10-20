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
    public partial class Practical3 : Form
    {
        //the state of the wiiremote
        WiimoteState moteState = Globals.WiiMote.WiimoteState;

        // Init
        public Practical3()
        {
            InitializeComponent();
            Init();
        }

        // Load
        private void Practical3_Load(object sender, EventArgs e)
        {
            this.Text = "Practical 3 - Drumming and IR Tracking";
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
                    Globals.Drumming.Update(this.Width - moteState.IRState.Midpoint.X * this.Width, moteState.IRState.Midpoint.Y * this.Height,
                                           moteState.AccelState.Values.X,
                                           moteState.AccelState.Values.Y);
                    Globals.Drumming.Draw(2);
                    Globals.Drumming.checkPosition();

                    // write codinates on screen
                    label0.Text = Globals.WiiMote.WiimoteState.IRState.Midpoint.X.ToString();
                    label1.Text = Globals.WiiMote.WiimoteState.IRState.Midpoint.Y.ToString();
                    label2.Text = Globals.WiiMote.WiimoteState.IRState.IRSensors[0].Found + " " +
                            Globals.WiiMote.WiimoteState.IRState.IRSensors[0].Position.Y.ToString();
                    label3.Text = Globals.WiiMote.WiimoteState.IRState.IRSensors[1].Found + " " +
                            Globals.WiiMote.WiimoteState.IRState.IRSensors[1].Position.Y.ToString();
                    label4.Text = Globals.WiiMote.WiimoteState.Battery.ToString();
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

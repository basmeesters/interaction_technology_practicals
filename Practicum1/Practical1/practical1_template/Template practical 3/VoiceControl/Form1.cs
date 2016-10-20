using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using System.Speech.Synthesis;
using System.Speech.Recognition;

namespace VoiceControl
{
    public partial class VoiceRecognition : Form
    {
        SpeechRecognitionEngine recognitionEngine = new SpeechRecognitionEngine(); // init speechrecognition Engine
        SpeechSynthesizer speakerEngine = new SpeechSynthesizer(); // init speechsynthesizer Engine

        public VoiceRecognition()
        { 
            InitializeComponent();

            // Speech Recognition grammer
            Choices movements = new Choices();
            movements.Add(new string[] { "left", "right", "top", "bottom" });

            GrammarBuilder builder = new GrammarBuilder();
            builder.Append(movements);

            Grammar grammer = new Grammar(builder);

            recognitionEngine.RequestRecognizerUpdate();
            recognitionEngine.LoadGrammar(grammer);
            recognitionEngine.SetInputToDefaultAudioDevice();
            recognitionEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(RecognizeSpeech);
            recognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
            //recognitionEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(speechRecognizer_SpeechRecognized);
            //recognitionEngine.SetInputToDefaultAudioDevice();
            //recognitionEngine.RecognizeAsync();
        }

        private void MouseMovement(int x, int y)
        {
            this.Cursor = new Cursor(Cursor.Current.Handle);
            Cursor.Position = new Point(x, y);
            Cursor.Clip = new Rectangle(this.Location, this.Size);
        }

        public void speakButton_Click(object sender, EventArgs e)
        {
            speakerEngine.Speak(hearBox.Text);
        }

        public void RecognizeSpeech(object sender, SpeechRecognizedEventArgs e)
        {
            Thread MoveMouse = new Thread(delegate() { });
            string line = "";
            foreach (RecognizedWordUnit word in e.Result.Words)
            {
                if (word.Confidence > 0.5f)
                    line += word.Text + " ";
            }
            string command = line.Trim();
            switch (command)
            {
                case "left":
                    MouseMovement(Cursor.Position.X - 1, Cursor.Position.Y);
                    break;
                case "right":
                    MouseMovement(Cursor.Position.X + 1, Cursor.Position.Y);
                    break;
                case "top":
                    MouseMovement(Cursor.Position.X, Cursor.Position.Y - 1);
                    break;
                case "bottom":
                    MouseMovement(Cursor.Position.X, Cursor.Position.Y + 1);
                    break;
            }
        }

        public void hearButton_Click(object sender, EventArgs e)
        {
            genericFunction();
        }

        //public void speechRecognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        //{
        //    MessageBox.Show(e.Result.Text);
        //}

        // generic function which uses methods of SpeechRecognitionEngine objects
        private void genericFunction()
        {
            // start new thread to control for XP - SAPI bug
            Thread t1 = new Thread(delegate()
            {
                
                // call methods of SpeechRecognitionEngine object here
                // ...

                // use delegate to access GUI element not created in this thread
                // for example update hearbox textbox on form with the string 'herkend!'
                String word = "herkend!";  
                hearBox.Invoke(new UpdateTextCallback(this.UpdatehearBox), word);         
            });

            // set right state for thread and start
            t1.SetApartmentState(ApartmentState.MTA);
            t1.Start();
        }

        // update function
        private void UpdatehearBox(string word)
        { 
            // function to update hearBox
        }

        private void VoiceRecognition_Load(object sender, EventArgs e)
        {

        }
    }

    // delegate needed for communication between threads
    public delegate void UpdateTextCallback(string word);
}

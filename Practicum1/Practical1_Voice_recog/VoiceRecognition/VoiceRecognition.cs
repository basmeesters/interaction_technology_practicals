using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

// needed for circumventing XP - SAPI interaction bug 
using System.Threading;

// additionally needed libraries
using System.Speech.Synthesis;
using System.Speech.Recognition;


namespace WindowsFormsApplication1
{
    public partial class VoiceRecognition : Form
    {
        /*******
         Globals
         *******/

        //Config Globals
        bool moveMouse = true; 

        // Declare recognition grammar objects
        Grammar grammar;
        GrammarBuilder gramBuild;

        // init speechrecognition Engine
        SpeechRecognitionEngine recognitionEngine = new SpeechRecognitionEngine();

        // initiate speechsynthesizer Engine
        SpeechSynthesizer speakerEngine = new SpeechSynthesizer();

        // move control variables
        bool move = false;
        bool repeat = false;

        /*******
         Methods
        *******/

        // Constructor
        public VoiceRecognition()
        {   
            // init GUI
            InitializeComponent();

            //MoveMouse or recognizeSpeech
            if (moveMouse)
                voicetoMouse();
            else
                recognizeSpeech();
        }

        private void repeatVoicetoMouse()
        {
            recognitionEngine.UnloadAllGrammars();
            recognitionEngine.LoadGrammar(grammar);
                recognitionEngine.SetInputToDefaultAudioDevice();
                
                RecognitionResult result = recognitionEngine.Recognize(new TimeSpan(0, 0, 5));

                if (result != null)
                {
                    Thread t2 = null;

                    foreach (RecognizedWordUnit word in result.Words)
                    {
                        if (word.Confidence > 0.8f)
                        {
                            if (word.Text == "escape")
                            {
                                repeat = false;
                                return;
                            }
                            move = true;
                            t2 = new Thread(() => moveDir(word.Text));
                            t2.Start();
                            audioStop();
                        }
                    }
                }


        }

        //method that recognizes speech and tries to control the mouse with it
        private void voicetoMouse()
        {
            //Define words to be recognized
            Choices actions = new Choices("left", "right", "top", "bottom", "escape");

            //Init GrammarBuilder
            gramBuild = new GrammarBuilder(actions);

            //Init Grammar with Builder
            grammar = new Grammar(gramBuild);

            Thread t1 = new Thread(delegate()
            {
                repeat = true;
                while (repeat)
                    repeatVoicetoMouse();
                    
            });

            t1.SetApartmentState(ApartmentState.MTA);
            t1.Start();
        }

        private void audioStop()
        {
            Choices stop = new Choices("halt", "hold", "stop");
            gramBuild = new GrammarBuilder(stop);
            grammar = new Grammar(gramBuild);

            recognitionEngine.UnloadAllGrammars();
            recognitionEngine.LoadGrammar(grammar);

            RecognitionResult result = null;

            while (result == null)
            {
                result = recognitionEngine.Recognize(new TimeSpan(0, 0, 5));
            }

            move = false;
        }

        private void moveDir(string s)
        {
            Point p;

            while (move)
            {
                switch (s)
                {
                    case "left":
                        p = new Point(Cursor.Position.X - 1, Cursor.Position.Y); break;
                    case "right":
                        p = new Point(Cursor.Position.X + 1, Cursor.Position.Y); break;
                    case "bottom":
                        p = new Point(Cursor.Position.X, Cursor.Position.Y + 1); break;
                    default: //top
                        p = new Point(Cursor.Position.X, Cursor.Position.Y - 1); break;
                }
                Cursor.Position = p;
                Thread.Sleep(50);
            }

        }

        private void repeatSpeachToText()
        {
            //Define words to be recognized
            Choices actions = new Choices("left", "right", "top", "bottom","escape");

            //Init GrammarBuilder
            gramBuild = new GrammarBuilder(actions);

            //Init Grammar with Builder
            grammar = new Grammar(gramBuild);
            
            recognitionEngine.LoadGrammar(grammar);
            recognitionEngine.SetInputToDefaultAudioDevice();

            RecognitionResult result = recognitionEngine.Recognize(new TimeSpan(0, 0, 5));
            string value = null;

            if (result == null)
                value = "nothing!";
            else
                foreach (RecognizedWordUnit word in result.Words)
                {
                    if (word.Text == "escape")
                    {
                        move = false;
                        return;   
                    }

                    if (word.Confidence > 0.8f)
                        value = "certainly " + word.Text + " (Confidence:" + word.Confidence + ")";
                    else if (word.Confidence > 0.4f)
                        value = "possibly " + word.Text + " (Confidence:" + word.Confidence + ")";
                }

            //update hearbox textbox on form with the word that was recognized and the certainty
            String total = "Recognized: " + value;
            hearBox.Invoke(new UpdateTextCallback(this.UpdatehearBox), total);      
        }

        //methods that recognizes speech and outputs the text and certainty
        private void recognizeSpeech()
        {
            // start new thread to control for XP - SAPI bug
            Thread t1 = new Thread(delegate()
            {
                repeat = true;
                while (repeat)
                    repeatSpeachToText();

            });

            // set right state for thread and start
            t1.SetApartmentState(ApartmentState.MTA);
            t1.IsBackground = true;
            t1.Start();
        }

        // update function
        private void UpdatehearBox(string word)
        {
            hearBox.Text += word + '\r' + '\n';
        }
    }

    // delegate needed for communication between threads
    public delegate void UpdateTextCallback(string word);
}

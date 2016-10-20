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


namespace VoiceControl
{
    public partial class VoiceRecognition : Form
    {
        /*******
         Globals
         *******/

        //Config Globals
        bool moveMouse = true; // false if you want speech to text, true if you want to control your mouse with speech

        // Declare grammar objects
        GrammarBuilder gramBuild;
        Grammar grammar;

        // init speechrecognition Engine
        SpeechRecognitionEngine recognitionEngine = new SpeechRecognitionEngine();

        // initiate speechsynthesizer Engine
        SpeechSynthesizer speakerEngine = new SpeechSynthesizer();

        // move control variables, don't change these
        bool move = false; 
        bool repeat = false;

        // Mouse speed integer
        int speed = 2;

        // Constructor
        public VoiceRecognition()
        {   
            // init GUI
            InitializeComponent();

            //Set the default audio device as input
            recognitionEngine.SetInputToDefaultAudioDevice();

            //Use speech to control the mouse, or to convert speech to text
            if (moveMouse)
                voicetoMouse();
            else
                recognizeSpeech();
        }

        /*******************************************
         * Use speechcontrol to control the mouse  *
         *******************************************/
        private void voicetoMouse()
        {
            //Define words to be recognized
            Choices actions = new Choices("left", "right", "top", "bottom", "escape");

            //Init GrammarBuilder and Grammar
            gramBuild = new GrammarBuilder(actions);
            grammar = new Grammar(gramBuild);

            //Create a thread to keep running the mouse control
            Thread t1 = new Thread(delegate()
            {
                repeat = true;
                while (repeat)
                    repeatVoicetoMouse(); //keep listening to commands      
            });

            t1.SetApartmentState(ApartmentState.MTA); //This is the default ApartmentState, but better safe, than sorry
            t1.IsBackground = true;
            t1.Start(); 
        }

        private void repeatVoicetoMouse()
        {
            //Reload Grammar
            recognitionEngine.UnloadAllGrammars();
            recognitionEngine.LoadGrammar(grammar);

            //Try to recognize a word
            RecognitionResult result = recognitionEngine.Recognize(new TimeSpan(0, 0, 5));

            if (result != null) // i.e. a word has been recognized
            {
                Thread t2 = null;

                foreach (RecognizedWordUnit word in result.Words)
                {
                    if (word.Confidence > 0.7f) //Only do this if the word was clear
                    {
                        if (word.Text == "escape") //stop if the user says to 
                        {
                            repeat = false; //stop repeating the voice recognition
                            return;
                        }
                        move = true; //give the mouse permission to move
                        t2 = new Thread(() => moveDir(word.Text)); //start a thread to make the mouse move into the desired direction
                        t2.Start();
                        audioStop(); //use this thread to listen when to stop moving the mouse
                    }
                }
            }
        }

        private void audioStop()
        {
            //rebuild the grammar with different words
            Choices stop = new Choices("halt", "hold", "stop");
            gramBuild = new GrammarBuilder(stop);
            grammar = new Grammar(gramBuild);

            //reload the grammar into the recognitionEngine
            recognitionEngine.UnloadAllGrammars();
            recognitionEngine.LoadGrammar(grammar);

            RecognitionResult result = null;

            //keep listening until a word is recognized
            while (result == null)
                result = recognitionEngine.Recognize(new TimeSpan(0, 0, 5));

            //stop the mouse from moving
            move = false;
        }

        private void moveDir(string s)
        {
            while (move) //while a "stop" word has not been recognized, keep moving the mouse
            {
                switch (s)
                {
                    case "left":
                        Cursor.Position = new Point(Cursor.Position.X - speed, Cursor.Position.Y); break;
                    case "right":
                        Cursor.Position = new Point(Cursor.Position.X + speed, Cursor.Position.Y); break;
                    case "bottom":
                        Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y + speed); break;
                    default: //top
                        Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y - speed); break;
                }
                Thread.Sleep(50); //let the thread sleep to make the moving smooth
            }
        }

        /***************************************************
         * Use Speechrecognition to convert speech to text *
         ***************************************************/
        private void recognizeSpeech()
        {
            // start new thread to control for XP - SAPI bug
            Thread t1 = new Thread(delegate()
            {
                repeat = true;
                while (repeat)
                    repeatSpeachToText(); //keep listening for words
            });

            // set right state for thread and start
            t1.SetApartmentState(ApartmentState.MTA);
            t1.IsBackground = true;
            t1.Start();
        }

        private void repeatSpeachToText()
        {
            //Define words to be recognized
            Choices words = new Choices("left", "right", "top", "bottom","escape");

            //Init GrammarBuilder and Grammar
            gramBuild = new GrammarBuilder(words);
            grammar = new Grammar(gramBuild);

            //reload Grammar
            recognitionEngine.UnloadAllGrammars();
            recognitionEngine.LoadGrammar(grammar);


            RecognitionResult result = recognitionEngine.Recognize(new TimeSpan(0, 0, 10)); //will timeout if there is an initial 10 seconds of silence. 
            string value = null;

            if (result == null)
                value = "nothing!"; //if nothing was recognized, output "nothing"
            else
                foreach (RecognizedWordUnit word in result.Words)
                {
                    if (word.Confidence > 0.7f)
                    {
                        if (word.Text == "escape") //if escape is recognized, stop the mouse from moving. 
                        {
                            move = false;
                            return;
                        }
                        value = "certainly " + word.Text + " (Confidence:" + word.Confidence + ")"; //otherwise output the recognized word and the confidence value with which it was recognized.
                    }
                    else if (word.Confidence > 0.4f)
                        value = "possibly " + word.Text + " (Confidence:" + word.Confidence + ")"; //if confidence is between 0.4 and 0.8, append the word possibly (because of the low confidence)
                }
                //if the confidence is lower than 0.4 output nothing

            //update hearbox textbox withoutput value

            String total = "Recognized: " + value;
            hearBox.Invoke(new UpdateTextCallback(this.UpdatehearBox), total);      
        }

        // update function
        private void UpdatehearBox(string word)
        {
            hearBox.Text += word + '\r' + '\n';
        }

        private void SwitchButton_Click(object sender, EventArgs e)
        {
            if (moveMouse == true)
            {
                repeat = false;
                moveMouse = false;
                SwitchButton.Text = "Speech Recognition";
            }
            else
            {
                repeat = false;
                moveMouse = true;
                SwitchButton.Text = "Mouse Movement";
            }
        }

        private void SpeakerButton_Click(object sender, EventArgs e)
        {
            speakerEngine.Rate = Convert.ToInt32(TextSpeed.Value);
            speakerEngine.Volume = Convert.ToInt32(VolumeSwitch.Value);
            speakerEngine.SpeakAsync(hearBox.Text);
        }

        private void MouseSpeed_ValueChanged(object sender, EventArgs e)
        {
            speed = Convert.ToInt32(MouseSpeed.Value);
        }
    }

    // delegate needed for communication between threads
    public delegate void UpdateTextCallback(string word);
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using Emgu.CV.GPU;
using Emgu.CV.Util;
using Emgu.CV.CvEnum;
using System.Diagnostics;

namespace Practical2
{
    public partial class WebcamFeatures : Form
    {
        // Save and open videos objects
        OpenFileDialog open = new OpenFileDialog();
        SaveFileDialog save = new SaveFileDialog();

        // Variables needed for recording
        VideoWriter writer;
        Thread isRecording;
        Stopwatch stopwatch = new Stopwatch();
        bool recordstate = false;
        delegate void SetTextCallback(string text); // Used for showing the recordtime

        // Objects needed for initiating the camera
        Image<Bgr, byte> frame;
        Capture capture = null;
        string last; // To replay the last record

        // Markerdetection variables
        bool markerDetectionMode = false;
        Bitmap detectTrainingFrame = new Bitmap("detectionTraining.bmp");
        Coord trainingmarkerCenter = new Coord(387,224);
        int trainingmarkerRadius = 23;
        bool initialDetection = false;
        Tuple<Coord, int, int, int, int> trackresult;
       
        //Marker histograms, these are set in the trainingstage
        float[] markerHistogramHue;
        float[] markerHistogramSat;

        //Marker overlay
        Image<Bgr, byte> subframe;
        Color trackercolor = Color.Black;

        enum View { Video, FaceDetection, MarkerDetection };
        View current = View.Video;

        public WebcamFeatures()
        {
            InitializeComponent();
            // Setup camera
            try
            {
                    capture = new Capture();
                    capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_WIDTH, 640);
                    capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_HEIGHT, 480);
                    capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FPS, 30); 
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return;
            }

            //set the haar cascades
            face = new HaarCascade("haarcascade_frontalface_default.xml");
            eye = new HaarCascade("haarcascade_eye.xml");
            

            //set the detectTrainingFrame
            Image<Bgr, byte> detectTrainingFrameBgr = new Image<Bgr, byte>(detectTrainingFrame);

            createMarkerHistogram(detectTrainingFrameBgr, trainingmarkerCenter, trainingmarkerRadius);

            
            ResetButton.Visible = false;
            Application.Idle += Process;
        }

        // Method used for showing the camera results on the screen
        private void Process(object sender, EventArgs e)
        {
                frame = capture.QueryFrame();
                if(markerDetectionMode)
                    frame = colourMarker(frame);
                box.Image = frame;
        }
        
        /// <summary>
        /// This methods gets a frame, and tries to colour the marker
        /// </summary>
        /// <param name="frame">the frame in which the marker resides</param>
        /// <returns>the frame with the location of the marker coloured</returns>
        private Image<Bgr, byte> colourMarker(Image<Bgr, byte> frame)
        {
            
            if (initialDetection == false)
                {
                    initialDetection = true;
                    trackresult = HistogramInitialmarkerDetection(frame);
                }

            int markerradius = trackresult.Item3 - trackresult.Item2;
            int trackingarea = 100;
            Coord markercenter = new Coord(trackresult.Item1.x, trackresult.Item1.y); 
            Rectangle subframearea = new Rectangle(markercenter.x - markerradius, markercenter.y - markerradius, trackingarea, trackingarea);
            subframe = frame.GetSubRect(subframearea);
            
            
            //Make histograms of the HSV-converted subframe and calculate the markercenter
            trackresult = HistogramInitialmarkerDetection(subframe);

            //find the radius of the marker
            markerradius = trackresult.Item3 - trackresult.Item2;
            Coord markercenter2 = new Coord(trackresult.Item1.x, trackresult.Item1.y);

            //Calculate the location of the marker on the initial frame
            markercenter = new Coord(markercenter2.x + (markercenter.x - (trackingarea/2)), markercenter2.y + (markercenter.y - (trackingarea/2)));

            //Draw a circle of the selected colour (default black) on the original frame where the marker is located
            PointF mcenter = new PointF(markercenter.x, markercenter.y);
            CircleF ccenter = new CircleF(mcenter, markerradius);
            Bgr markercolor = new Bgr(trackercolor);
            frame.Draw(ccenter, markercolor, -1);

            return frame;
        }

        /// <summary>
        /// this method creates and globally sets the histograms of the marker
        /// </summary>
        /// <param name="frame">frame in which the marker recides</param>
        /// <param name="center">the center of the marker</param>
        /// <param name="radius">the radius of the marker</param>
        private void createMarkerHistogram(Image<Bgr, byte> frame, Coord center, int radius)
        {
            //Calculate rough area of the marker, taking edges into account
            int leftX, rightX, upY, downY;

            if (center.x < radius) leftX = 0;
            else leftX = center.x - radius;

            if (center.x + radius > frame.Cols) rightX = frame.Cols;
            else rightX = center.x + radius;

            if (center.y < radius) upY = 0;
            else  upY = center.y - radius;
            
            if (center.y + radius > frame.Rows) downY = frame.Rows;
            else downY = center.y + radius;

            //Calculate the histogram for the hue and saturation values
            markerHistogramHue = new float[241];
            markerHistogramSat = new float[241]; 
            
            //Transform the input frame into HSV
            Image<Hsv, byte> hsvFrame = frame.Convert<Hsv, byte>(); //convert the frame to Hue Saturation Values
            
            //The data of the image are stored in the Data property (3d array) of the frame. 
            byte[, ,] data = hsvFrame.Data; //referencing it here, instead of in the loop, is more efficient.

            for (int x = leftX; x < rightX; x++)
                for (int y = upY; y < downY; y++)
                {
                    markerHistogramHue[data[y, x, 0]]++; //Expl: histogram[data[y,x,Hue]] + 1; //EMGU Documentation fail: inconsistence. 
                    markerHistogramSat[data[y, x, 1]]++; //Expl: histogram[data[y,x,Saturation]] + 1; 
                }

            //normalize histograms: divide the value by the amount of pixels in the area
            float numPixels = (rightX - leftX) * (downY - upY);

            for (int i = 0; i < 241; i++)
            {
                markerHistogramHue[i] = markerHistogramHue[i] / numPixels;
                markerHistogramSat[i] = markerHistogramSat[i] / numPixels;
            }

            //All done!
        }

        /// <summary>
        /// this method tries to detect the marker, without an initial location (not tracking)
        /// </summary>
        /// <param name="frame">the frame in which the marker needs to be detected</param>
        /// <returns>A tuple with MarkerCenter, leftX, rightX, upY, downY </returns>
        private Tuple<Coord, int,int,int,int> HistogramInitialmarkerDetection(Image<Bgr, byte> frame)
        {
            Image<Hsv, byte> hsFrame = frame.Convert<Hsv, byte>();

            float[,] imgT = new float[frame.Cols, frame.Rows];
            byte[, ,] data = hsFrame.Data;

            float[] xHistoIMGT = new float[frame.Cols];
            float[] yHistoIMGT = new float[frame.Rows];

            for (int x = 0; x < frame.Cols; x++)
                for (int y = 0; y < frame.Rows; y++)
                {
                    if (markerHistogramHue[data[y, x, 0]] > 0.1f && markerHistogramSat[data[y,x,0]] > 0.1f)
                    {
                        xHistoIMGT[x] += markerHistogramHue[x];
                        yHistoIMGT[y] += markerHistogramSat[y];
                    }
                }

            //Markercenter 
            Coord markercenter = new Coord(
                xHistoIMGT.ToList().IndexOf(xHistoIMGT.Max()), //get the max (peak) of the histogram (peak)
                yHistoIMGT.ToList().IndexOf(yHistoIMGT.Max()) //get the max of (peak) the histogram (peak)
                );

            //Area of marker = width of histogram
            int leftX = -1, rightX = -1, upY = -1, downY = -1;

            //find x area
            for (int i = 0; i < xHistoIMGT.Length; i++)
            {
                if (xHistoIMGT[i] > 0 && leftX == -1)
                    leftX = i;
                if (xHistoIMGT[i] > 0)
                    rightX = i;
            }
            //find Y area
            for (int i = 0; i < yHistoIMGT.Length; i++)
            {
                if (yHistoIMGT[i] > 0 && upY == -1)
                    upY = i;
                if (yHistoIMGT[i] > 0)
                    downY = i;
            }

            return new Tuple<Coord,int,int,int,int>(markercenter, leftX, rightX ,upY, downY);

        }

        //this method tracks the marker, by looking in it's surroundings
        private Image<Bgr, byte> HistogramMarkerTrack(Image<Bgr, byte> newFrame, Coord center, int radius)
        {
            //Make histograms of the HSV-converted subframe and calculate the markercenter
            Tuple<Coord, int, int, int, int> trackresult = HistogramInitialmarkerDetection(newFrame);

            //Draw a circle of the selected colour (default black) on the subframe where the marker is located
            
            PointF mcenter = new PointF(center.x, center.y);
            CircleF ccenter = new CircleF(mcenter, radius);
            Bgr markercolor = new Bgr(trackercolor);
            newFrame.Draw(ccenter, markercolor, -1);
            
            return newFrame;
        }

        // Capture Training
        private void TrainingButton_Click(object sender, EventArgs e)
        {
            
        }
            
        // Record and replay method
        private void RecordButton_Click(object sender, EventArgs e)
        {
            // First save the file to be written
            if (isRecording == null && recordstate == false)
            {
                save.Filter =  "Video Files (.avi)|*.avi|All Files (*.*)|*.*";
                if (save.ShowDialog() == DialogResult.OK)
                {
                    RecordButton.Text = "Stop";
                    writer = new VideoWriter(save.FileName, 30, 640, 480, true);
                    isRecording = new Thread(this.Record);
                    isRecording.Start();
                }
            }

            // Replay last record
            else if (recordstate == true)
            {
                RecordButton.Text = "Replay";
                if (capture != null)
                    capture.Dispose();
                capture = new Capture(last);
            }
            
            // Stop the recording
            else
            {
                RecordButton.Text = "Record";
                isRecording = null;
                ResetButton.Visible = true;
            }
        }

        // Thread for writing to a file and showing the stopwatch
        private void Record()
        {
            stopwatch.Reset();
            stopwatch.Start();
            while (isRecording != null)
            {
                writer.WriteFrame(frame);
                SetText("Recordtime: " + stopwatch.Elapsed);
                Thread.Sleep(35);
            }
            stopwatch.Stop();
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            // Open only avi files
            open.Filter = "Video Files (.avi)|*.avi|All Files (*.*)|*.*";
            open.FilterIndex = 1;
            if (open.ShowDialog() == DialogResult.OK)
            {
                recordstate = true;
                if (capture != null)
                    capture.Dispose();
                capture = new Capture(open.FileName);
                last = open.FileName; // Remember the last record for replay
                RecordButton.Text = "Replay";
                ResetButton.Visible = true;
            }
        }

        // When application is closed make sure the camera is disposed
        private void ReleaseData()
        {
            if (capture != null)
                capture.Dispose();
        }

        // Stopwatch text
        private void SetText(string text)
        {
            if (this.lbl_RCTime.InvokeRequired)
            {
                SetTextCallback callback = new SetTextCallback(SetText);
                this.Invoke(callback, new object[] { text });
            }
            else
                this.lbl_RCTime.Text = text;
        }

        // Reset camera and timer
        private void ResetButton_Click(object sender, EventArgs e)
        {
            capture = new Capture();
            RecordButton.Text = "Record";
            recordstate = false;
            ResetButton.Visible = false;
        }

        private void ViewButton_Click(object sender, EventArgs e)
        {
            // Switch to facedetection view
            if (current == View.Video)
            {
                current = View.FaceDetection;
                lbl_RCTime.Visible = false;
                RecordButton.Visible = false;
                OpenButton.Visible = false;
                ResetButton.Visible = false;
                WindowsSwitch.Visible = true;
                EyesSwitch.Visible = true;
                DetectionboxSwitch.Visible = true;
                ViewButton.Text = "Switch to Marker Tracking";
                this.Text = "Face Detection Mode";
                Application.Idle -= Process;
                Application.Idle += FaceDetection;
            }
            else if (current == View.FaceDetection)
            {
                current = View.MarkerDetection;
                WindowsSwitch.Visible = false;
                EyesSwitch.Visible = false;
                DetectionboxSwitch.Visible = false;
                ColorButton.Visible = true;
                initialDetection = false;
                ViewButton.Text = "Switch to Video mode";
                this.Text = "Marker Detection Mode";
                Application.Idle -= FaceDetection;
                markerDetectionMode = true;
                Application.Idle += Process;
            }
            else
            {
                current = View.Video;
                lbl_RCTime.Visible = true;
                ColorButton.Visible = false;
                RecordButton.Visible = true;
                OpenButton.Visible = true;
                ResetButton.Visible = true;
                ViewButton.Text = "Switch to Face Detection";
                this.Text = " Video Mode";
                Application.Idle += Process;
            }
        }

        private void ColorButton_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                trackercolor = colorDialog1.Color;
            }

        }
        
    }
}

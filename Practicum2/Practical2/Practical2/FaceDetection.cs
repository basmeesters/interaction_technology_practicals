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
    partial class WebcamFeatures
    {
        // Face detection variables
        HaarCascade face, eye;
        bool windows = false;
        bool vieweyes = false;
        bool detectionbox = false;

        private void FaceDetection(object sender, EventArgs e)
        {
            // Get the frame captured by the camera
            Image<Bgr, Byte> frame = capture.QueryFrame();

            if (frame != null)   // confirm that image is valid
            {
                // Grayframe needed for face detection
                Image<Gray, byte> grayframe = frame.Convert<Gray, byte>();
                frame = capture.QueryFrame();

                // Detect faces from the grayframe with heads at least 20 x 20, the other parameters are optimized by trying different things
                MCvAvgComp[] faces = face.Detect(grayframe, 1.2, 3, HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(20, 20));
                MCvAvgComp[] eyes = eye.Detect(grayframe, 1.4, 6, HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(20, 20));

                // Variables needed for calculating the middle of the face and coloring the rectangle
                Rectangle rectangle;
                Point center;

                // Call properties outside of loop for performance boost
                int frameWidth = frame.Width;
                int frameHeight = frame.Height;

                // Check for each face detected in which area it is detected
                foreach (var f in faces)
                {
                    if (windows == true)
                    {
                        center = new Point(f.rect.Left + f.rect.Width / 2, f.rect.Top + f.rect.Height / 2);
                        if (center.X > frameWidth / 2 && center.Y > frameHeight / 2)
                            rectangle = new Rectangle(frameWidth / 2, frameHeight / 2, frameWidth / 2, frameHeight / 2);
                        else if (center.X < frameWidth / 2 && center.Y > frameHeight / 2)
                            rectangle = new Rectangle(1, frameHeight / 2, frameWidth / 2 - 1, frameHeight / 2 - 1);
                        else if (center.X > frameWidth / 2 && center.Y < frameHeight / 2)
                            rectangle = new Rectangle(frameWidth / 2, 1, frameWidth / 2 - 1, frameHeight / 2 - 1);
                        else // center.X < frameWidth / 2 && center.Y < ImageFrame.Heigt / 2
                            rectangle = new Rectangle(1, 1, frameWidth / 2 - 1, frameHeight / 2 - 1);
                        frame.Draw(rectangle, new Bgr(Color.Red), 3);
                    }
                    if (detectionbox == true)
                        frame.Draw(f.rect, new Bgr(Color.Green), 2);
                }
                // Draw lines
                if (windows == true)
                {
                    LineSegment2D line = new LineSegment2D(new Point(frame.Width / 2, 0), new Point(frame.Width / 2, frame.Height));
                    frame.Draw(line, new Bgr(Color.Blue), 1);

                    LineSegment2D line2 = new LineSegment2D(new Point(0, frame.Height / 2), new Point(frame.Width, frame.Height / 2));
                    frame.Draw(line2, new Bgr(Color.Blue), 1);
                }
                if (vieweyes == true)
                    foreach (var eyeee in eyes)
                        frame.Draw(eyeee.rect, new Bgr(Color.Blue), 2);
            }

            // Draw in imagebox
            box.Image = frame;   
        }

        // Turn certain techniques on or off
        private void WindowsSwitch_CheckedChanged(object sender, EventArgs e)
        {
            windows = !windows;
        }

        private void EyesSwitch_CheckedChanged(object sender, EventArgs e)
        {
            vieweyes = !vieweyes;
        }

        private void DetectionboxSwitch_CheckedChanged(object sender, EventArgs e)
        {
            detectionbox = !detectionbox;
        }
        
    }
}

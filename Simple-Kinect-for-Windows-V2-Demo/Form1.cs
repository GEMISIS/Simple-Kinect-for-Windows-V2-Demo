using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Kinect;
using System.Drawing.Imaging;

namespace Simple_Kinect_for_Windows_V2_Demo
{
    /// <summary>
    /// An enumeration to decide what type of image to show the user.
    /// </summary>
    public enum ImageType
    {
        Color = 0,
        Depth = 1,
        IR = 2
    }
    public partial class Form1 : Form
    {
        /// <summary>
        /// The Kinect sensor to obtain our data from.
        /// </summary>
        private KinectSensor sensor = null;

        /// <summary>
        /// A reader for getting frame data.  This particular reader can be used to read from
        /// multiple sources (in this case, color, depth, and infrared).
        /// </summary>
        private MultiSourceFrameReader frameReader = null;

        /// <summary>
        /// The raw pixel data recieved for the depth image from the Kinect sensor.
        /// </summary>
        private ushort[] rawDepthPixelData = null;
        /// <summary>
        /// The raw pixel data recieved for the infrared image from the Kinect sensor.
        /// </summary>
        private ushort[] rawIRPixelData = null;

        /// <summary>
        /// The type of image to display in the form.
        /// </summary>
        ImageType imageType = ImageType.Color;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Check whether there are Kinect sensors available and select the default one.
            if(KinectSensor.KinectSensors.Count > 0)
            {
                this.sensor = KinectSensor.Default;

                // Check that the connect was properly retrieved and is connected.
                if(this.sensor != null)
                {
                    if (this.sensor.Status == KinectStatus.Connected)
                    {
                        // Open the sensor for use.
                        this.sensor.Open();

                        // Next open the multi-source frame reader.
                        this.frameReader = this.sensor.OpenMultiSourceFrameReader(FrameSourceTypes.Color | FrameSourceTypes.Depth | FrameSourceTypes.Infrared);

                        // Retrieve the frame descriptions for each frame source.
                        FrameDescription colorFrameDescription = this.sensor.ColorFrameSource.FrameDescription;
                        FrameDescription depthFrameDescription = this.sensor.DepthFrameSource.FrameDescription;
                        FrameDescription irFrameDescription = this.sensor.InfraredFrameSource.FrameDescription;
                        
                        // Afterwards, setup the data using the frame descriptions.
                        // Depth and infrared have just one component per pixel (depth value or infrared value).
                        this.rawDepthPixelData = new ushort[depthFrameDescription.Width * depthFrameDescription.Height * 1];
                        this.rawIRPixelData = new ushort[irFrameDescription.Width * irFrameDescription.Height * 1];

                        // Finally, set the method for handling each multi-source frame that is captured.
                        this.frameReader.MultiSourceFrameArrived += frameReader_MultiSourceFrameArrived;
                    }
                }
            }
        }

        void frameReader_MultiSourceFrameArrived(object sender, MultiSourceFrameArrivedEventArgs e)
        {
            // Try to get the frame from its reference.
            try
            {
                MultiSourceFrame frame = e.FrameReference.AcquireFrame();

                if (frame != null)
                {
                    // The frame is disposable, so make sure we state that we are using it.
                    using (frame)
                    {
                        try
                        {
                            // Then switch between the possible types of images to show, get its frame reference, then use it
                            // with the appropriate image.
                            switch (this.imageType)
                            {
                                case ImageType.Color:
                                    ColorFrameReference colorFrameReference = frame.ColorFrameReference;
                                    useRGBAImage(colorFrameReference);
                                    break;
                                case ImageType.Depth:
                                    DepthFrameReference depthFrameReference = frame.DepthFrameReference;
                                    useDepthImage(depthFrameReference);
                                    break;
                                case ImageType.IR:
                                    InfraredFrameReference irFrameReference = frame.InfraredFrameReference;
                                    useIRImage(irFrameReference);
                                    break;
                            }
                        }
                        catch (Exception)
                        {
                            // Don't worry about exceptions for this demonstration.
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Don't worry about exceptions for this demonstration.
            }
        }

        /// <summary>
        /// Draws color image data from the specified frame.
        /// </summary>
        /// <param name="frameReference">The reference to the color frame that should be used.</param>
        private void useRGBAImage(ColorFrameReference frameReference)
        {
            // Actually aquire the frame here and check that it was properly aquired, and use it again since it too is disposable.
            ColorFrame frame = frameReference.AcquireFrame();

            if (frame != null)
            {
                using (frame)
                {
                    // Next get the frame's description and create an output bitmap image.
                    FrameDescription description = frame.FrameDescription;
                    Bitmap outputImage = new Bitmap(description.Width, description.Height, PixelFormat.Format32bppArgb);

                    // Next, we create the raw data pointer for the bitmap, as well as the size of the image's data.
                    System.Drawing.Imaging.BitmapData imageData = outputImage.LockBits(new Rectangle(0, 0, outputImage.Width, outputImage.Height),
                        ImageLockMode.WriteOnly, outputImage.PixelFormat);
                    IntPtr imageDataPtr = imageData.Scan0;
                    int size = imageData.Stride * outputImage.Height;

                    // After this, we copy the image data directly to the buffer.  Note that while this is in BGRA format, it will be flipped due
                    // to the endianness of the data.
                    if (frame.RawColorImageFormat == ColorImageFormat.Bgra)
                    {
                        frame.CopyRawFrameDataToBuffer((uint)size, imageDataPtr);
                    }
                    else
                    {
                        frame.CopyConvertedFrameDataToBuffer((uint)size, imageDataPtr, ColorImageFormat.Bgra);
                    }

                    // Finally, unlock the output image's raw data again and create a new bitmap for the preview picture box.
                    outputImage.UnlockBits(imageData);
                    this.previewPictureBox.Image = outputImage;
                }
            }
        }

        /// <summary>
        /// Draws depth image data from the specified frame.
        /// </summary>
        /// <param name="frameReference">The reference to the depth frame that should be used.</param>
        private void useDepthImage(DepthFrameReference frameReference)
        {
            // Actually aquire the frame here and check that it was properly aquired, and use it again since it too is disposable.
            DepthFrame frame = frameReference.AcquireFrame();

            if (frame != null)
            {
                FrameDescription description = null;
                using (frame)
                {
                    // Next get the frame's description and create an output bitmap image.
                    description = frame.FrameDescription;
                    Bitmap outputImage = new Bitmap(description.Width, description.Height, PixelFormat.Format32bppArgb);

                    // Next, we create the raw data pointer for the bitmap, as well as the size of the image's data.
                    System.Drawing.Imaging.BitmapData imageData = outputImage.LockBits(new Rectangle(0, 0, outputImage.Width, outputImage.Height),
                        ImageLockMode.WriteOnly, outputImage.PixelFormat);
                    IntPtr imageDataPtr = imageData.Scan0;
                    int size = imageData.Stride * outputImage.Height;

                    // After this, we copy the image data into its array.  We then go through each pixel and shift the data down for the
                    // RGB values, as their normal values are too large.
                    frame.CopyFrameDataToArray(this.rawDepthPixelData);
                    byte[] rawData = new byte[description.Width * description.Height * 4];
                    int i = 0;
                    foreach (ushort point in this.rawDepthPixelData)
                    {
                        rawData[i++] = (byte)(point >> 6);
                        rawData[i++] = (byte)(point >> 4);
                        rawData[i++] = (byte)(point >> 2);
                        rawData[i++] = 255;
                    }
                    // Next, the new raw data is copied to the bitmap's data pointer, and the image is unlocked using its data.
                    System.Runtime.InteropServices.Marshal.Copy(rawData, 0, imageDataPtr, size);
                    outputImage.UnlockBits(imageData);

                    // Finally, the image is set for the preview picture box.
                    this.previewPictureBox.Image = outputImage;
                }
            }
        }

        /// <summary>
        /// Draws infrared image data from the specified frame.
        /// </summary>
        /// <param name="frameReference">The reference to the infrared frame that should be used.</param>
        private void useIRImage(InfraredFrameReference frameReference)
        {
            // Actually aquire the frame here and check that it was properly aquired, and use it again since it too is disposable.
            InfraredFrame frame = frameReference.AcquireFrame();

            if (frame != null)
            {
                FrameDescription description = null;
                using (frame)
                {
                    // Next get the frame's description and create an output bitmap image.
                    description = frame.FrameDescription;
                    Bitmap outputImage = new Bitmap(description.Width, description.Height, PixelFormat.Format32bppArgb);

                    // Next, we create the raw data pointer for the bitmap, as well as the size of the image's data.
                    System.Drawing.Imaging.BitmapData imageData = outputImage.LockBits(new Rectangle(0, 0, outputImage.Width, outputImage.Height),
                        ImageLockMode.WriteOnly, outputImage.PixelFormat);
                    IntPtr imageDataPtr = imageData.Scan0;
                    int size = imageData.Stride * outputImage.Height;

                    // After this, we copy the image data into its array.  We then go through each pixel and shift the data down for the
                    // RGB values, and set each one to the same value, resulting in a grayscale image, as their normal values are too large.
                    frame.CopyFrameDataToArray(this.rawIRPixelData);
                    byte[] rawData = new byte[description.Width * description.Height * 4];
                    int i = 0;
                    foreach (ushort point in this.rawIRPixelData)
                    {
                        byte value = (byte)(128 - (point >> 8));
                        rawData[i++] = value;
                        rawData[i++] = value;
                        rawData[i++] = value;
                        rawData[i++] = 255;
                    }
                    // Next, the new raw data is copied to the bitmap's data pointer, and the image is unlocked using its data.
                    System.Runtime.InteropServices.Marshal.Copy(rawData, 0, imageDataPtr, size);
                    outputImage.UnlockBits(imageData);

                    // Finally, the image is set for the preview picture box.
                    this.previewPictureBox.Image = outputImage;
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.frameReader != null)
            {
                this.frameReader.Dispose();
            }
            if (this.sensor != null)
            {
                this.sensor.Dispose();
            }
        }

        private void colorImageButton_Click(object sender, EventArgs e)
        {
            this.imageType = ImageType.Color;
        }

        private void depthImageButton_Click(object sender, EventArgs e)
        {
            this.imageType = ImageType.Depth;
        }

        private void irImageButton_Click(object sender, EventArgs e)
        {
            this.imageType = ImageType.IR;
        }

        private void snapshotButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "png files (*.png)|*.png";
            dialog.FilterIndex = 1;
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.previewPictureBox.Image.Save(dialog.FileName);
            }
        }
    }
}

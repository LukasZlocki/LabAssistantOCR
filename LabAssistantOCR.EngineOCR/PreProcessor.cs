using Tesseract;
using System.Drawing;

namespace LabAssistantOCR.EngineOCR
{
    internal class PreProcessor
    {
        public Pix ConvertImageToGrey(Pix img)
        {
            using (var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default))
            {
                // Convert to grayscale
                using (var grayImg = img.ConvertTo8(1)) // ConvertRGBToGray(0.8f, 0.8f, 0.8f))
                {
                    // rotate image 360 dagrees to avoid not handled error during saving the non rotated image 
                    //Pix rotateGrayImg = grayImg.Rotate((float)4.7124);
                    Pix finalRotateGrayImg = grayImg.Rotate((float)1.5708);
                    return finalRotateGrayImg;
                }
            }
        }

        public Pix ConvertImageToGrayAndSaveImage(Pix img)
        {
            Pix convertedToGray = ConvertImageToGrey(img);
            // Save the grayscale image
            convertedToGray.Save("C:\\VirtualServer\\reuslts_meas\\PreProcessed\\gray_image.png", ImageFormat.Png);
            return convertedToGray;

        }

        /// <summary>
        /// Saving pix image as png
        /// </summary>
        /// <param name="img">Tesseract Pix format image</param>
        /// <param name="fileName">Output filename</param>
        public void SaveThisImg(Pix img, string fileName)
        {
            img.Save("C:\\VirtualServer\\reuslts_meas\\PreProcessed\\" + fileName, ImageFormat.Png);
        }

        public Pix ConvertImageToBinary(Pix img)
        {
            using (var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default))
            {
                // Convert to binary
                using (var binaryImg = img.BinarizeOtsuAdaptiveThreshold(1000, 1000, 4000, 4000, (float)0.1))
                {
                    // rotate image 360 dagrees to avoid not handled error during saving the non rotated binary image 
                    Pix rotateBinaryImg = binaryImg.Rotate((float)4.7124);
                    Pix finalRotateBinaryImg = rotateBinaryImg.Rotate((float)1.5708);
                    return finalRotateBinaryImg;
                }
            }
        }

        public Pix ConvertImageToBinaryAndSaveImage(Pix img)
        {
            Pix convertedToBinary = ConvertImageToBinary(img);
            // Save the grayscale image
            string savePath = @"C:\VirtualServer\reuslts_meas\PreProcessed\binary_image.jpg";
            convertedToBinary.Save(savePath, ImageFormat.Jp2);
            return convertedToBinary;
        }

        public void CheckImageDimension(Pix img)
        {
            int width = img.Width;
            int height = img.Height;

            Console.WriteLine($"Width {width}");
            Console.WriteLine($"Height {height}");
        }

        /// <summary>
        /// Rescale Pix image to given width
        /// </summary>
        /// <param name="img">image in Tesseract Pix format</param>
        /// <param name="userWidth">Future width of given image</param>
        /// <returns>Rescaled image in Tesseract Pix format</returns>
        public Pix RescaleImageToUserWidth(Pix img, int userWidth)
        {
            int newHeight = (img.Height * userWidth) / img.Width;

            using (var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default))
            {
                using (var resizedImg = img.Scale(userWidth, newHeight))
                {
                    return resizedImg;
                }

            }
        }

        /// <summary>
        /// rescale jpg image
        /// </summary>
        /// <param name="img">Image as System.Drawing.Image object</param>
        /// <param name="userWidth">Future width of given image</param>
        /// <returns>Rescaled image as System.Drawing.Image object</returns>
        public Image RescaleJpgImage(Image img, int userWidth)
        {
            int newHeight = (img.Height * userWidth) / img.Width;
            Bitmap resizedImg = new Bitmap(userWidth, newHeight);

            using (Graphics graphics = Graphics.FromImage(resizedImg))
            {
                graphics.DrawImage(img, 0, 0, userWidth, newHeight);
            }
            //resizedImg.RotateFlip(RotateFlipType.Rotate90FlipNone);
            return resizedImg;
        }

        /// <summary>
        /// Load jpg file from given path
        /// </summary>
        /// <param name="path">full path to image with folder name and file name</param>
        /// <returns>System.Drawing.Image object</returns>
        public Image LoadJpgImage(string path)
        {
            try
            {
                Image img = Image.FromFile(path);
                return img;
            }
            catch (Exception ex) 
            { 
                Console.WriteLine("Error during loading the image: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Save image to specific path
        /// </summary>
        /// <param name="img">Image as System.Drawing.Image object</param>
        /// <param name="savePath">full save path with folder name and file name</param>
        public void SaveJpgImage(Image img, string savePath)
        {
            try
            {
                img.Save(savePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during saving the image: " + ex.Message);
            }
        }





    }
}
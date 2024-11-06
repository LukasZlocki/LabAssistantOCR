using Tesseract;

namespace LabAssistantOCR.EngineOCR
{
    public class PreProcessor
    {
        public Pix ConvertImageToGrey(Pix img)
        {
            using (var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default))
            {
                // Convert to grayscale
                using (var grayImg = img.ConvertRGBToGray(0.8f, 0.8f, 0.8f))
                {
                    // rotate mage 90 dagrees
                    Pix rotateGrayImg = grayImg.Rotate((float)1.5708);
                    return rotateGrayImg;
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

        public Pix ConvertImageToBinary(Pix img)
        {
            Pix imgGray = ConvertImageToGrey(img);

            using (var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default))
            {
                // Convert to binary
                using (var binaryImg = imgGray.BinarizeOtsuAdaptiveThreshold(1000, 1000, 4000, 4000, (float)0.1))
                {
                    return binaryImg;
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





    }
}
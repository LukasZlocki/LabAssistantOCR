using Tesseract;
using ImageFormat = Tesseract.ImageFormat;

namespace LabAssistantOCR.EngineOCR
{
    public class PreProcessor
    {
        public Pix ConvertImageToGrey(Pix img)
        {
            using (var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default))
            {
                // Convert to grayscale
                using (var grayImg = img.ConvertRGBToGray(1, 1, 1))
                {
                    // rotate mage 90 dagrees
                    Pix rotateGrayImg = grayImg.Rotate((float)1.5708);
                    // Save the grayscale image
                    rotateGrayImg.Save("C:\\VirtualServer\\reuslts_meas\\PreProcessed\\gray_image.jpg", ImageFormat.Bmp);
                    return rotateGrayImg;
                }
            }
        }

        public Pix ConvertImageToBinary(Pix img)
        {
            using (var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default))
            {
                // Convert to binary
                using (var binaryImg = img.BinarizeOtsuAdaptiveThreshold(1000, 1000, 4000, 4000, (float)0.1))
                {
                    // Save the grayscale imageSystem.ArgumentException: 'The sx parameter must be greater than or equal to 16 (Parameter 'sx')'
                    binaryImg.Save("C:\\VirtualServer\\reuslts_meas\\PreProcessed\\binary_image.jpg", ImageFormat.Bmp);
                    return binaryImg;
                }
            }
        }

    }
}
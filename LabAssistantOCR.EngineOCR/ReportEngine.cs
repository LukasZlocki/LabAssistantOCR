using Tesseract;

namespace LabAssistantOCR.EngineOCR
{
    public class ReportEngine
    {
        PreProcessor preProcessor = new PreProcessor();
        DataExtractor dataExtractor = new DataExtractor();
        TextExtractor textExtractor = new TextExtractor();

        public void ReadMachineReportImage(string pathToImage, string fileName)
        {
            // ToDo : Code loading img for further processing
            Console.WriteLine("Loading image...");
            Pix img = LoadImage(pathToImage, fileName);
            Console.WriteLine("Image loaded.");

            // ToDo : Code to pre process img (gray, binary)
            Console.WriteLine("Preprocessing...");
            Console.WriteLine("Preprocessing...8bit Gray");
            
            Pix imgGray = preProcessor.ConvertImageToGrey(img);
            Console.WriteLine("Preprocessing...binary image");
            Pix imgBinary = preProcessor.ConvertImageToBinary(imgGray);
            Console.WriteLine("Preprocessing finished.");

            // ToDo : Extract text from image
            Console.WriteLine("Extracting raw text data...");
            Console.WriteLine("Extracting raw text from gray img...");
            string extractedTextGrayImg = textExtractor.ExtractTextFromImg(imgGray);
            Console.WriteLine("Extracting raw text from binary img...");
            string extractedTextBinaryImg = textExtractor.ExtractTextFromImg(imgBinary);

            

            // TODO : Clean data 
            // ToDo : Store data in final report

        }

        public void GetReport()
        {
            // ToDo: Code retriving final report
        }

        private Pix LoadImage(string path, string filename)
        {
            try
            {
                Pix img = Pix.LoadFromFile(path + @"\" + filename);
                return img;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Not able to load image " + ex.Message);
                return null;
            }
        }



    }
}

using Tesseract;

namespace LabAssistantOCR.EngineOCR
{
    public class ReportEngine
    {
        private readonly PreProcessor _preProcessor;
        private readonly DataExtractor _dataExtractor;
        private readonly TextExtractor _textExtractor;
        private readonly DataCleaner _dataCleaner;

        public ReportEngine()
        {
            _preProcessor = new();
            _dataExtractor = new();
            _textExtractor = new();
            _textExtractor = new();
        }


        public void ReadMachineReportImage(string pathToImage, string fileName)
        {
            // ToDo : Code loading img for further processing
            Console.WriteLine("Loading image...");
            Pix img = LoadImage(pathToImage, fileName);
            Console.WriteLine("Image loaded.");

            // ToDo : Code to pre process img (gray, binary)
            Console.WriteLine("Preprocessing...");
            Console.WriteLine("Preprocessing...8bit Gray");
            
            Pix imgGray = _preProcessor.ConvertImageToGrey(img);
            Console.WriteLine("Preprocessing...binary image");
            Pix imgBinary = _preProcessor.ConvertImageToBinary(imgGray);
            Console.WriteLine("Preprocessing finished.");

            // ToDo : Extract text from image
            Console.WriteLine("Extracting raw text data...");
            Console.WriteLine("Extracting raw text from gray img...");
            string extractedTextGrayImg = _textExtractor.ExtractTextFromImg(imgGray);
            Console.WriteLine("Extracting raw text from binary img...");
            string extractedTextBinaryImg = _textExtractor.ExtractTextFromImg(imgBinary);
            Console.WriteLine("Extracting raw text finished.");

            // Todo : Data extraction from readed string
            Console.WriteLine("Extracting report text data from raw text...");
            Console.WriteLine("...extracting from Gray img...");
            DataSample datasample1 = _dataExtractor.DataExtractionToReport(extractedTextGrayImg);
            Console.WriteLine("...extracting from binary img...");
            DataSample datasample2 = _dataExtractor.DataExtractionToReport(extractedTextBinaryImg);
            Console.WriteLine("...extracting from binary img...");
            Console.WriteLine("Extraction finisahed.");

            Console.WriteLine("Showing data - gray img:");
            datasample1.ShowDataSample();

            Console.WriteLine("Showing data - binary img:");
            datasample2.ShowDataSample();


            // TODO : Clean data
            Console.WriteLine("Cleaning data...");
            _dataCleaner.AddDatasample(datasample1);
            _dataCleaner.AddDatasample(datasample2);
            _dataCleaner.CleanDatasamples();
            Console.WriteLine("Data cleaned.");

            Console.WriteLine("Showing cleaned data:");
            _dataCleaner.ShowCleanedDatasamples();

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

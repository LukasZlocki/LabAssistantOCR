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
            _dataCleaner = new();
        }
        // this is alternative method
        /// <summary>
        /// read and create report base on only binary image
        /// </summary>
        /// <param name="pathToImage"></param>
        /// <param name="fileName"></param>
        public void ReadMachineReportImage_BinaryOnly(string pathToImage, string fileName)
        {
            /*
            // Loading img for further processing
            Console.WriteLine("Loading image...");
            Pix img = LoadImage(pathToImage, fileName);
            Console.WriteLine("Image loaded.");
            */

        }


        /// <summary>
        /// Read and create report base on gray and binary images
        /// </summary>
        /// <param name="pathToImage"></param>
        /// <param name="fileName"></param>
        public void ReadMachineReportImage(string pathToImage, string fileName)
        {
            // Loading img for further processing
            Console.WriteLine("Loading image...");
            Pix img = LoadImage(pathToImage, fileName);
            Console.WriteLine("Image loaded.");

            // Pre processing img (gray, binary)
            Console.WriteLine("Preprocessing...");
            Console.WriteLine("Preprocessing...8bit Gray");
            Pix imgGray = LoadImage(@"C:\VirtualServer\reuslts_meas\PreProcessed\", "gray_image.png");
            //Pix imgGray = _preProcessor.ConvertImageToGrayAndSaveImage(img);

            Console.WriteLine("Preprocessing...binary image");
            Pix imgBinary = _preProcessor.ConvertImageToBinaryAndSaveImage(imgGray);
            Console.WriteLine("Preprocessing finished.");

            // Extract text from image
            Console.WriteLine("Extracting raw text data...");
            Console.WriteLine("Extracting raw text from gray img...");
            Pix imgGrayTxtExtract = LoadImage(@"C:\VirtualServer\reuslts_meas\PreProcessed\", "gray_image.png");
            string extractedTextGrayImg = _textExtractor.ExtractTextFromImg(imgGrayTxtExtract);
            
            Console.WriteLine("Extracting raw text from binary img...");
            string imagePathBin = @"C:\VirtualServer\reuslts_meas\PreProcessed\binary_image.jpg";
            Pix imgBinaryTxtExtract = LoadImage(@"C:\VirtualServer\reuslts_meas\PreProcessed\", "binary_image.png");
            string extractedTextBinaryImg = _textExtractor.ExtractTextFromImg(imgBinaryTxtExtract);

            //string extractedTextBinaryImg = _textExtractor.ExtractTextFromImg(imgBinary);
            Console.WriteLine("Extracting raw text finished.");

            // Data extraction from readed string
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

            // Clean data
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

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

        /// <summary>
        /// Extract report from given image without any pre processing.
        /// </summary>
        /// <param name="pathToImage">Path to image</param>
        /// <returns>Extracted string from given image</returns>
        public string GetTextFromImage_NoPreprocessing(string pathToImage)
        {
            return  _textExtractor.ExtractTextFromLoadedImage(pathToImage);
        }

        /// <summary>
        /// Retrieve report from given image without image pre processingS
        /// </summary>
        /// <param name="pathToImage">Path to image</param>
        /// <returns>DataSample object with cleaned report data</returns>
        public DataSample GetReportFromImage_NoPreProcessing(string pathToImage)
        {
            string extractedText = _textExtractor.ExtractTextFromLoadedImage(pathToImage);
            DataSample rawExtractedDataSample = new DataSample();
            rawExtractedDataSample = _dataExtractor.DataExtractionToReport(extractedText);
            // Add datasample set to data cleaner
            _dataCleaner.AddDatasample(rawExtractedDataSample);
            // Cleaning data samples
            _dataCleaner.CleanDatasamples();
            return _dataCleaner.GetCleanedReport();
        }

        public void ReadMachineReportImage_GrayOnly(string pathToImage, string fileName)
        {
            // Loading img for further processing
            Console.WriteLine("Loading image...");
            Pix img = LoadImage(pathToImage, fileName);
            Console.WriteLine("Image loaded.");

            // Pre processing img - binary only
            Console.WriteLine("Preprocessing...");
            Console.WriteLine("Preprocessing...8bit Gray");
            Pix imgGray = _preProcessor.ConvertImageToGrayAndSaveImage(img);

            // Extract text from image
            Console.WriteLine("Extracting raw text data...");
            Console.WriteLine("Extracting raw text from gray img...");
            string extractedTextGrayImg = _textExtractor.ExtractTextFromImg(imgGray);
            Console.WriteLine("Extracted Text: {0}", extractedTextGrayImg);
            Console.WriteLine("Extracting raw text finished.");

            // Data extraction from readed string
            Console.WriteLine("Extracting report text data from raw text...");
            Console.WriteLine("...extracting from Gray img...");
            DataSample datasampleGray = _dataExtractor.DataExtractionToReport(extractedTextGrayImg);
            Console.WriteLine("Extraction finisahed.");

            Console.WriteLine("Showing data - gray img:");
            datasampleGray.ShowDataSample();

            // Clean data
            Console.WriteLine("Cleaning data...");
            _dataCleaner.AddDatasample(datasampleGray);
            _dataCleaner.CleanDatasamples();
            Console.WriteLine("Data cleaned.");

            Console.WriteLine("Showing cleaned data:");
            _dataCleaner.ShowCleanedDatasamples();
        }


        /// <summary>
        /// read and create report base on only binary image
        /// </summary>
        /// <param name="pathToImage"></param>
        /// <param name="fileName"></param>
        public void ReadMachineReportImage_BinaryOnly(string pathToImage, string fileName)
        {
            // Loading img for further processing
            Console.WriteLine("Loading image...");
            Pix img = LoadImage(pathToImage, fileName);
            Console.WriteLine("Image loaded.");

            // Pre processing img - binary only
            Console.WriteLine("Preprocessing...");
            Console.WriteLine("Preprocessing...binary image");
            Pix imgBinary = _preProcessor.ConvertImageToBinary(img);
            Console.WriteLine("Preprocessing finished.");

            // Extract text from image
            Console.WriteLine("Extracting raw text data...");
            Console.WriteLine("Extracting raw text from binary img...");
            //string extractedTextBinaryImg = _textExtractor.ExtractTextFromImg(imgBinary);
            /// --- >>>>   string extractedTextBinaryImg = _textExtractor.ExtrA
            ///Console.WriteLine("Extracted Text: {0}", extractedTextBinaryImg);
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

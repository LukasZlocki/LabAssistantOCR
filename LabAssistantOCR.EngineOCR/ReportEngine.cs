using System.Drawing;
using System.Xml.Linq;
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
            string extractedText = "";
            try 
            { 
                extractedText = _textExtractor.ExtractTextFromLoadedImage(pathToImage);
            }
            catch (ErrorHandler ex)
            {
                new DataSample
                {
                    Date = Convert.ToString(DateTime.UtcNow),
                    um4 = "N/A",
                    um6 = "N/A",
                    um14 = "N/A"
                };
            }
            catch (Exception ex)
            {
                new DataSample 
                { 
                    Date = Convert.ToString(DateTime.UtcNow),
                    um4 = "N/A",
                    um6 = "N/A",
                    um14 = "N/A"
                };
            }
            DataSample rawExtractedDataSample = new DataSample();
            rawExtractedDataSample = _dataExtractor.DataExtractionToReport(extractedText);
            // Add datasample set to data cleaner
            _dataCleaner.AddDatasample(rawExtractedDataSample);
            // Cleaning data samples
            _dataCleaner.CleanDatasamples();
            return _dataCleaner.GetCleanedReport();
        }


        /// <summary>
        /// Preprocess given image for batter OCR accuracy along with reading and creating final report base on preprocessed image
        /// </summary>
        /// <param name="path">Path to image</param>
        /// <returns>Final report as DataSample object</returns>
        public DataSample GetReportFromImage_ImageWillBePreProcessed(string path)
        {       
            // Step 1: Load and convert orginal image to gray
            Pix imageToGray = LoadImage(path);
            Pix grayImg = _preProcessor.ConvertImageToGrey(imageToGray);
            grayImg.Save("C:\\VirtualServer\\reuslts_meas\\PreProcessedTesseract\\1grayImage.tif", ImageFormat.Tiff);

            // Step 2: Convert gray image to binary image
            Pix binaryImg = _preProcessor.ConvertImageToBinary(grayImg);
            binaryImg.Save("C:\\VirtualServer\\reuslts_meas\\PreProcessedTesseract\\2binaryImage.tif", ImageFormat.Tiff);

            // Step 3: Rescale gray image
            Image grayImgToRescale = _preProcessor.LoadJpgImage("C:\\VirtualServer\\reuslts_meas\\PreProcessedTesseract\\1grayImage.tif");
            Image rescaledGrayImg = _preProcessor.RescaleJpgImage(grayImgToRescale, 1000);
            _preProcessor.SaveJpgImage(rescaledGrayImg, "C:\\VirtualServer\\reuslts_meas\\PreProcessedTesseract\\3rescaledGrayImg.jpg");

            // Step 4: Rescale binary image
            Image binaryImgToRescale = _preProcessor.LoadJpgImage("C:\\VirtualServer\\reuslts_meas\\PreProcessedTesseract\\2binaryImage.tif");
            Image rescaledBinaryImg = _preProcessor.RescaleJpgImage(binaryImgToRescale, 1000);
            _preProcessor.SaveJpgImage(rescaledBinaryImg, "C:\\VirtualServer\\reuslts_meas\\PreProcessedTesseract\\4rescaledBinaryImg.jpg");

            // Step 4: Extract text data from preprocessed and rescaled binary image
            string extractedText = "";
            try
            {
                extractedText = _textExtractor.ExtractTextFromLoadedImage("C:\\VirtualServer\\reuslts_meas\\PreProcessedTesseract\\4rescaledBinaryImg.jpg");
            }
            catch (ErrorHandler ex)
            {
                new DataSample
                {
                    Date = Convert.ToString(DateTime.UtcNow),
                    um4 = "N/A",
                    um6 = "N/A",
                    um14 = "N/A"
                };
            }
            catch (Exception ex)
            {
                new DataSample
                {
                    Date = Convert.ToString(DateTime.UtcNow),
                    um4 = "N/A",
                    um6 = "N/A",
                    um14 = "N/A"
                };
            }

            // Step 5: Clean data
            DataSample rawExtractedDataSample = new DataSample();
            rawExtractedDataSample = _dataExtractor.DataExtractionToReport(extractedText);
            // Add datasample set to data cleaner
            _dataCleaner.AddDatasample(rawExtractedDataSample);
            // Cleaning data samples
            _dataCleaner.CleanDatasamples();

            // Step 6: Return report
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

        private Pix LoadImage(string path)
        {
            try
            {
                Pix img = Pix.LoadFromFile(path);
                return img;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Not able to load image " + ex.Message);
                return null;
            }
        }

        private void SavePixImage(Pix pixImg, string path)
        {
            pixImg.Save(path);
        }



    }
}

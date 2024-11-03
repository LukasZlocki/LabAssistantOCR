using LabAssistant;
using Tesseract;

Console.WriteLine("Laboratory Assistant");

Console.WriteLine("");
Console.WriteLine("**************** USING PREPROCESSING***************");

string imagePath = @"C:\VirtualServer\reuslts_meas\15 smarfone\sample13.jpg";
Pix img = Pix.LoadFromFile(imagePath);

// Preprocessing - converting to grey scale
PreProcessor preProcessor = new PreProcessor();

// Text extractor
TextExtractor textExtractor = new TextExtractor();


Console.WriteLine("**** Data extraction from processed to gray img");
Pix imgGrey = preProcessor.ConvertImageToGrey(img);
string ExtractedTextProcessedGrey = textExtractor.ExtractTextFromImg(imgGrey);
Console.WriteLine("Extracted Text: {0}", ExtractedTextProcessedGrey);

Console.WriteLine("**** Data extraction from processed to binary image");
Pix imgBinary = preProcessor.ConvertImageToBinary(imgGrey);
string imagePathBin = @"C:\VirtualServer\reuslts_meas\PreProcessed\binary_image.jpg";
Pix imgBin = Pix.LoadFromFile(imagePathBin);
string ExtractedTextProcessedBinary = textExtractor.ExtractTextFromImg(imgBin);
Console.WriteLine("Extracted Text: {0}", ExtractedTextProcessedBinary);

// ***************** extracting potential report
Console.WriteLine("");
Console.WriteLine("");
Console.WriteLine("***************** extracting potential report");
DataExtractor dataExtractor = new DataExtractor();
var datasample1 = dataExtractor.DataExtractionToReport(ExtractedTextProcessedBinary);
var datasample2 = dataExtractor.DataExtractionToReport(ExtractedTextProcessedGrey);

Console.WriteLine("");
Console.WriteLine("Report : from binary");
datasample1.ShowDataSample();

Console.WriteLine("");
Console.WriteLine("Report : from grey");
datasample2.ShowDataSample();

// Cleaning data samples
DataCleaner cleaner = new DataCleaner();
cleaner.AddDatasample(datasample1);
cleaner.AddDatasample(datasample2);
cleaner.CleanDatasamples();
cleaner.ShowCleanedDatasamples();



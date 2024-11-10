using LabAssistantOCR.EngineOCR;
using Tesseract;

string imagePath = @"C:\VirtualServer\reuslts_meas\15 smarfone\";
string fileName = "sample_00.jpg";

Console.WriteLine("Laboratory Assistant");

/*
// This works!
#region Report base on GRAY img only
ReportEngine reGray = new();
reGray.ReadMachineReportImage_GrayOnly(imagePath, fileName);
#endregion
*/

#region Report base on BINARY img only

ReportEngine reBinary = new();
reBinary.ReadMachineReportImage_BinaryOnly(imagePath, fileName);

#endregion

#region Report base on gray and binary img
/*
string imagePath = @"C:\VirtualServer\reuslts_meas\15 smarfone\";
string fileName = "sample13.jpg";
ReportEngine re = new();
re.ReadMachineReportImage(imagePath, fileName);
*/
#endregion

#region Main Program
/*
Console.WriteLine("");
Console.WriteLine("**************** USING PREPROCESSING***************");

string imagePath = @"C:\VirtualServer\reuslts_meas\15 smarfone\sample1.jpg";
Pix img = Pix.LoadFromFile(imagePath);

// Preprocessing - converting to grey scale
PreProcessor preProcessor = new PreProcessor();

// Text extractor
TextExtractor textExtractor = new TextExtractor();


Console.WriteLine("**** Data extraction from processed to gray img");
//Pix imgGrey = preProcessor.ConvertImageToGrey(img);
Pix imgGrey = preProcessor.ConvertImageToGrayAndSaveImage(img);
string ExtractedTextProcessedGrey = textExtractor.ExtractTextFromImg(imgGrey);
Console.WriteLine("Extracted Text: {0}", ExtractedTextProcessedGrey);


Console.WriteLine("**** Data extraction from processed to binary image");
//Pix imgBinary = preProcessor.ConvertImageToBinary(imgGrey);
Pix imgBinary = preProcessor.ConvertImageToBinaryAndSaveImage(imgGrey);
string imagePathBin = @"C:\VirtualServer\reuslts_meas\PreProcessed\binary_image.jpg";
Pix imgBin = Pix.LoadFromFile(imagePathBin);
string ExtractedTextProcessedBinary = textExtractor.ExtractTextFromImg(imgBin);
Console.WriteLine("Extracted Text: {0}", ExtractedTextProcessedBinary);


// ***************** extracting potential report
Console.WriteLine("");
Console.WriteLine("");
Console.WriteLine("***************** extracting potential report");
DataExtractor dataExtractor = new DataExtractor();
//var datasample1 = dataExtractor.DataExtractionToReport(ExtractedTextProcessedBinary);
var datasample2 = dataExtractor.DataExtractionToReport(ExtractedTextProcessedGrey);

Console.WriteLine("");
Console.WriteLine("Report : from binary");
//datasample1.ShowDataSample();

Console.WriteLine("");
Console.WriteLine("Report : from grey");
datasample2.ShowDataSample();

// Cleaning data samples
DataCleaner cleaner = new DataCleaner();
//cleaner.AddDatasample(datasample1);
cleaner.AddDatasample(datasample2);
cleaner.CleanDatasamples();
cleaner.ShowCleanedDatasamples();
*/
#endregion





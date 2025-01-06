using LabAssistantOCR.EngineOCR;
using System.Windows;

namespace LabAssistantOCR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ReportEngine _reportEngine;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void FileDropStackPanel_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                string filename = System.IO.Path.GetFileName(files[0]);
                string path = System.IO.Path.GetFullPath(files[0]);
                string path2 = System.IO.Path.GetDirectoryName(files[0]);

                ExtractReportAndShowOnScreen(path);
            }
        }

        private void ExtractReportAndShowOnScreen(string path)
        {
            DataSample dataSample = new DataSample();

            // Get report from preprocessed image
            _reportEngine = new();
            dataSample = _reportEngine.GetReportFromImage_ImageWillBePreProcessed(path);

            // Gettting raw data from pre processed image.
            string rawdata = _reportEngine.GetExtractedStringFromPreProcessedImage();
            
            // Show raw data on screen
            ShowDataOnScreen(rawdata);

            // Show extracted report on screen
            ShowReportOnScreen(dataSample);
        }

        private void ShowDataOnScreen(string data)
        {
            txtScreen.Text = string.Empty;
            txtScreen.Text = "   " + data;
        }

        private void ShowReportOnScreen(DataSample dataSample)
        {
            txtReportScreen.Text = string.Empty;
            string report = "FINAL REPORT: " + "\n"
                + "Report date: " + dataSample.Date + "\n"
                + " 4um  : " + dataSample.um4 + "\n"
                + " 6um  : " + dataSample.um6 + "\n"
                + " 14um : " + dataSample.um14;
            txtReportScreen.Text = report;
        }


    }
}
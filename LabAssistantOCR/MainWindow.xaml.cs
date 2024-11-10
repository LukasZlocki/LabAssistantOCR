using LabAssistantOCR.EngineOCR;
using System.Windows;

namespace LabAssistantOCR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ReportEngine _reportEngine = new ReportEngine();

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

                Show(path);
            }
        }

        private void Show(string path)
        {
            // Gettting raw data from image without preprocessing
            string rawdata = GetRawDataFromImage(path);
            ShowDataOnScreen(rawdata);

            // Get report from image without preprocessing
            DataSample dataSample = new DataSample();
            dataSample = GetDatasampleFromImage(path);
            ShowReportOnScreen(dataSample);

        }

        private string GetRawDataFromImage(string path)
        {
            return _reportEngine.GetTextFromImage_NoPreprocessing(path);
        }

        private DataSample GetDatasampleFromImage(string path)
        { 
            return _reportEngine.GetReportFromImage_NoPreProcessing(path);
        }

        private void ShowDataOnScreen(string data)
        {
            txtScreen.Text = string.Empty;
            txtScreen.Text = data;
        }

        private void ShowReportOnScreen(DataSample dataSample)
        {
            string report = "Report date: " + dataSample.Date + "\n"
                + " 4um  : " + dataSample.um4 + "\n"
                + " 6um  : " + dataSample.um6 + "\n"
                + " 14um : " + dataSample.um14;
            txtReportScreen.Text = report;
        }

    }
}
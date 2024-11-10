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
            string rawdata = GetRawDataFromImage(path);
            ShowDataOnScreen(rawdata);
        }

        private string GetRawDataFromImage(string path)
        {
            return _reportEngine.GetTextFromImage_NoPreprocessing(path);
        }

        private void ShowDataOnScreen(string data)
        {
            txtScreen.Text = string.Empty;
            txtScreen.Text = data;
        }

    }
}
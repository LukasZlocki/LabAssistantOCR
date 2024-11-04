using Tesseract;

namespace LabAssistantOCR.EngineOCR
{
    public class ReportEngine
    {
        public void ReadMachineReportImage(string pathToImage, string fileName)
        {
            // ToDo : Code loading img for further processing
            var img = LoadImage(pathToImage, fileName);

            // ToDo : Code to pre process img

            // ToDo : Extract text from image
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

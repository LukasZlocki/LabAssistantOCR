using System.Reflection.Metadata;
using System.Text.RegularExpressions;

namespace LabAssistantOCR.EngineOCR
{
    internal class DataCleaner
    {
        private List<DataSample> RawDataSamples;
        private List<DataSample> CleanedDatasamples;

        DataSample cleanReport;

        // Constructor
        public DataCleaner()
        {
            RawDataSamples = new List<DataSample>();
            CleanedDatasamples = new List<DataSample>();
            cleanReport = new DataSample();
        }

        /// <summary>
        /// Clean list with datasamples by extracting dates and values 
        /// </summary>
        public void CleanDatasamples()
        {
            foreach (DataSample sample in RawDataSamples)
            {
                DataSample dataSampleCleaned = new();
                dataSampleCleaned.Date = extractDate(sample.Date);
                dataSampleCleaned.um4 = extractValue(sample.um4);
                dataSampleCleaned.um6 = extractValue(sample.um6);
                dataSampleCleaned.um14 = extractValue(sample.um14);

                CleanedDatasamples.Add(dataSampleCleaned);
            }
        }

        /// <summary>
        /// Extract date from given string
        /// </summary>
        /// <param name="dateString">string with date</param>
        /// <returns>date as string</returns>
        private string extractDate(string dateString)
        {
            if (dateString != null)
            {
                string date;
                string pattern = @"\d{4}-\d{2}-\d{2}";
                Match match = Regex.Match(dateString, pattern);

                if (match.Success)
                {
                    return match.Value;
                }
                else
                {
                    return "0000-00-00";
                }
            }
            else 
            {
                return "0000-00-00";
            }
        }

        /// <summary>
        /// Extract first value from given string
        /// </summary>
        /// <param name="value">string with value to extract</param>
        /// <returns>value as string</returns>
        private string extractValue(string value)
        {
            string pattern = @"\b\d+\b";
            Match match = Regex.Match(value, pattern);

            if (match.Success)
            {
                return match.Value;
            }
            else
            {
                return "N/A";
            }
        }

        /// <summary>
        /// Add datasample to list of datasamples
        /// </summary>
        /// <param name="dataSample"></param>
        public void AddDatasample(DataSample dataSample)
        {
            RawDataSamples.Add(dataSample);
        }

        /// <summary>
        /// Show available raw datasamples
        /// </summary>
        public void ShowRawDatasamples()
        {
            foreach (DataSample dataSample in RawDataSamples)
            {
                Console.WriteLine("");
                Console.WriteLine("Raw datasample report:");
                dataSample.ShowDataSample();
            }
        }

        /// <summary>
        /// Show available cleaned datasamples
        /// </summary>
        public void ShowCleanedDatasamples()
        {
            foreach (DataSample dataSample in CleanedDatasamples)
            {
                Console.WriteLine("");
                Console.WriteLine("Cleaned datasample report:");
                dataSample.ShowDataSample();
            }
        }

        public DataSample GetCleanedReport()
        {
            this.cleanReport = CleanedDatasamples.First();
            return this.cleanReport;
        }

    }
}

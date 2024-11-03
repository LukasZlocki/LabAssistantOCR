namespace LabAssistantOCR.EngineOCR
{
    public class DataExtractor
    {
        DataSample dataSample = new DataSample();

        // List to establish point 0 for list of data
        List<string> zeroPointCheckList = new List<string> {
            "Sample",
            "Meas."
        };

        // data sample positions
        int dataSamplePosition1_date = 1;
        int dataSamplePosition2_4um = 3;
        int dataSamplePosition3_6um = 4;
        int dataSamplePosition4_14um = 6;

        public DataSample DataExtractionToReport(string reportString)
        {
            string[] linesToAnalysys = CutStringToLines(reportString);
            int zeroPosition = 0;
            bool isZeroPositionEstablished = false;
            foreach (string line in linesToAnalysys)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                if (line.Contains(zeroPointCheckList[0]) || line.Contains(zeroPointCheckList[1]))
                {
                    zeroPosition = 0;
                    isZeroPositionEstablished |= true;
                    zeroPosition++;
                    continue;
                }
                if (zeroPosition == dataSamplePosition1_date && isZeroPositionEstablished == true)
                {
                    dataSample.Date = line;
                    zeroPosition++;
                    continue;
                }
                if (zeroPosition == dataSamplePosition2_4um && isZeroPositionEstablished == true)
                {
                    dataSample.um4 = line;
                    zeroPosition++;
                    continue;
                }
                if (zeroPosition == dataSamplePosition3_6um && isZeroPositionEstablished == true)
                {
                    dataSample.um6 = line;
                    zeroPosition++;
                    continue;
                }
                if (zeroPosition == dataSamplePosition4_14um && isZeroPositionEstablished == true)
                {
                    dataSample.um14 = line;
                    zeroPosition++;
                    return dataSample;
                }
                zeroPosition++;
            }
            return dataSample;
        }

        private string[] CutStringToLines(string reportString)
        {
            string[] lines = reportString.Split(new[] { '\n' }, StringSplitOptions.None);
            return lines;
        }
    }
}

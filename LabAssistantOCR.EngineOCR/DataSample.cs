namespace LabAssistantOCR.EngineOCR
{
    public class DataSample
    {
        public string Date { get; set; }
        public string um4 { get; set; }
        public string um6 { get; set; }
        public string um14 { get; set; }

        public void ShowDataSample()
        {
            Console.WriteLine("");
            Console.WriteLine("Report extracted sample:");
            Console.WriteLine("Date : {0}", Date);
            Console.WriteLine("4um  : {0}", um4);
            Console.WriteLine("6um  : {0}", um6);
            Console.WriteLine("14um : {0}", um14);
        }
    }
}

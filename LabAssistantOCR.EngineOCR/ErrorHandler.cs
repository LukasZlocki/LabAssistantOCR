namespace LabAssistantOCR.EngineOCR
{
    internal class ErrorHandler : Exception
    {
        internal ErrorHandler(string exceptionMessage) : base(exceptionMessage) { }
    }
}

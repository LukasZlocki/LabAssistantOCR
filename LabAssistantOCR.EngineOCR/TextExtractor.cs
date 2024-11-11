using Patagames.Ocr.Enums;
using Patagames.Ocr;
using Tesseract;

namespace LabAssistantOCR.EngineOCR
{
    internal class TextExtractor
    {
        public string ExtractTextFromImg(Pix img)
        {
            using (var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default))
            {
                using (var page = engine.Process(img))
                {
                    return page.GetText();
                }
            }
        }

        internal string ExtractTextFromLoadedImage(string imagePath)
        {
            using (var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default))
            {
                try
                {
                    using (var img = Pix.LoadFromFile(imagePath))
                    {
                        using (var page = engine.Process(img))
                        {
                            return page.GetText();
                        }
                    }
                }
                catch (Exception ex) 
                {
                    return "Error during image text recognition. System error description: " + ex.Message;
                }
            }
        }

        internal void CreatePdf()
        {
            using (var api = OcrApi.Create())
            {
                api.Init(Languages.English);
                //Create the renderer to PDF file output. The extension will be added automatically
                using (var renderer = OcrPdfRenderer.Create("multipage_pdf_file"))
                {
                    renderer.BeginDocument("Title");
                    api.ProcessPages(@"c:\multipage.tif", renderer);
                    renderer.EndDocument();
                }
            }
        }
    }
}

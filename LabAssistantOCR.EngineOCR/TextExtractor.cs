using Patagames.Ocr.Enums;
using Patagames.Ocr;
using Tesseract;

namespace LabAssistant
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

        public static string ExtractTextFromImage(string imagePath)
        {
            using (var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default))
            {
                using (var img = Pix.LoadFromFile(imagePath))
                {
                    using (var page = engine.Process(img))
                    {
                        return page.GetText();
                    }
                }
            }
        }

        public void CreatePdf()
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

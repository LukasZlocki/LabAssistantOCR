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

        /// <summary>
        /// Extract text from given path with image
        /// </summary>
        /// <param name="imagePath">Path to image </param>
        /// <returns>String of signs red by OCR from given image</returns>
        /// <exception cref="ErrorHandler"></exception>
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
                    throw new ErrorHandler("Error during image text recognition. System error description: " + ex.Message);
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

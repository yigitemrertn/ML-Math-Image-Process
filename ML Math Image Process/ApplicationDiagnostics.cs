namespace ML_Math_Image_Process
{
    /// <summary>
    /// Uygulama baþlatma kontrolü
    /// </summary>
    public static class ApplicationDiagnostics
    {
        /// <summary>
        /// Tüm bileþenlerin düzgün baþlatýldýðýný kontrol et
        /// </summary>
        public static void CheckInitialization()
        {
            try
            {
                // ImageProcessor kontrol
                var processor = new ImageProcessor();
                if (!processor.IsImageLoaded)
                {
                    System.Diagnostics.Debug.WriteLine("? ImageProcessor baþlatýldý");
                }

                // Filter kontrol
                var grayscale = new GrayscaleFilter();
                System.Diagnostics.Debug.WriteLine($"? GrayscaleFilter: {grayscale.FilterName}");

                var blur = new BlurFilter();
                System.Diagnostics.Debug.WriteLine($"? BlurFilter: {blur.FilterName}");

                var edge = new EdgeDetectionFilter();
                System.Diagnostics.Debug.WriteLine($"? EdgeDetectionFilter: {edge.FilterName}");

                System.Diagnostics.Debug.WriteLine("? TÜM BÝLEÞENLER BAÞARILIYDI");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"? HATA: {ex.Message}");
                throw;
            }
        }
    }
}

using System.Drawing;

namespace ML_Math_Image_Process
{
    /// <summary>
    /// Görüntü iþleme operasyonlarýný yönetir.
    /// Filtre seçimi ve uygulamasý için merkezi nokta.
    /// OOP Ýlkeleri: Encapsulation (kapsülleme) - filtreleme iþlemlerini içinde tutar.
    /// </summary>
    public class ImageProcessor
    {
        /// <summary>
        /// Mevcut çalýþma görüntüsü
        /// </summary>
        private Bitmap? _currentImage;

        /// <summary>
        /// Orijinal görüntü (sýfýrlama için)
        /// </summary>
        private Bitmap? _originalImage;

        /// <summary>
        /// Mevcut görüntüyü döndür
        /// </summary>
        public Bitmap? CurrentImage => _currentImage;

        /// <summary>
        /// Mevcut görüntü yüklendi mi?
        /// </summary>
        public bool IsImageLoaded => _currentImage != null;

        /// <summary>
        /// Görüntü yükle
        /// </summary>
        public void LoadImage(string imagePath)
        {
            try
            {
                // Eski görüntüyü temizle
                _currentImage?.Dispose();
                _originalImage?.Dispose();

                // Yeni görüntüyü yükle
                _currentImage = new Bitmap(imagePath);
                _originalImage = new Bitmap(_currentImage);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Görüntü yüklenirken hata oluþtu: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Bitmap ile doðrudan yükle
        /// </summary>
        public void LoadImage(Bitmap bitmap)
        {
            _currentImage?.Dispose();
            _originalImage?.Dispose();

            _currentImage = new Bitmap(bitmap);
            _originalImage = new Bitmap(bitmap);
        }

        /// <summary>
        /// Seçilen filtreyi uygulamak
        /// </summary>
        public void ApplyFilter(ImageFilter filter)
        {
            if (!IsImageLoaded)
                throw new InvalidOperationException("Lütfen önce bir görüntü yükleyiniz");

            try
            {
                // Filtre uygula
                Bitmap filteredImage = filter.Apply(_currentImage!);

                // Eski görüntüyü temizle
                _currentImage.Dispose();

                // Yeni görüntüyü ayarla
                _currentImage = filteredImage;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"{filter.FilterName} filtresini uygularken hata oluþtu: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Görüntüyü orijinal haline sýfýrla
        /// </summary>
        public void ResetToOriginal()
        {
            if (_originalImage == null)
                throw new InvalidOperationException("Orijinal görüntü bulunamadý");

            _currentImage?.Dispose();
            _currentImage = new Bitmap(_originalImage);
        }

        /// <summary>
        /// Görüntüyü dosyaya kaydet
        /// </summary>
        public void SaveImage(string filePath)
        {
            if (!IsImageLoaded)
                throw new InvalidOperationException("Lütfen önce bir görüntü yükleyiniz");

            try
            {
                _currentImage!.Save(filePath);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Görüntü kaydedilirken hata oluþtu: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Kaynaklarý temizle
        /// </summary>
        public void Dispose()
        {
            _currentImage?.Dispose();
            _originalImage?.Dispose();
        }
    }
}

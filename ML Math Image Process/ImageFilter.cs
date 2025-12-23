using System.Drawing;
using System.Drawing.Imaging;

namespace ML_Math_Image_Process
{
    /// <summary>
    /// Görüntü filtreleri için soyut taban sýnýf.
    /// Tüm filtreler bu sýnýftan miras almak zorundadýr.
    /// OOP Ýlkeleri: Polimorfizm ve Kalýtým kullanýr.
    /// </summary>
    public abstract class ImageFilter
    {
        /// <summary>
        /// Filtre adý (örn: "Kenar Bulma", "Gri Tonlama", vb.)
        /// </summary>
        public abstract string FilterName { get; }

        /// <summary>
        /// Filtre açýklamasý
        /// </summary>
        public abstract string FilterDescription { get; }

        /// <summary>
        /// Bitmap üzerine filtreyi uygula (soyut metot - her alt sýnýf kendi uygulamasýný saðlamalý)
        /// </summary>
        /// <param name="bitmap">Ýþlenecek görüntü</param>
        /// <returns>Filtrelenen görüntü</returns>
        public abstract Bitmap Apply(Bitmap bitmap);

        /// <summary>
        /// LockBits ile piksel verilerine doðrudan eriþim saðlar (yüksek performans).
        /// Bu yöntem tüm alt sýnýflar tarafýndan kullanýlabilir.
        /// </summary>
        /// <param name="bitmap">Ýþlenecek görüntü</param>
        /// <param name="callback">Piksel verilerine eriþim için callback fonksiyonu</param>
        protected void ProcessBitmapWithLockBits(Bitmap bitmap, Action<BitmapData, int> callback)
        {
            // Bitmap'i kilitleyerek doðrudan bellek eriþimi saðla
            BitmapData bitmapData = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadWrite,
                bitmap.PixelFormat);

            try
            {
                // Piksel formatýna göre byte sayýsý
                int bytesPerPixel = Image.GetPixelFormatSize(bitmap.PixelFormat) / 8;
                
                // Callback'ý çaðýr (alt sýnýf iþleme yapar)
                callback(bitmapData, bytesPerPixel);
            }
            finally
            {
                // Bitmap'i kilidi aç
                bitmap.UnlockBits(bitmapData);
            }
        }

        /// <summary>
        /// Bitmap'in kopyasýný oluþtur
        /// </summary>
        protected Bitmap CloneBitmap(Bitmap source)
        {
            return new Bitmap(source);
        }
    }
}

using System.Drawing;
using System.Drawing.Imaging;

namespace ML_Math_Image_Process
{
    /// <summary>
    /// Grayscale (Gri Tonlama) Filtresi
    /// Renkli bir görüntüyü gri tonlamaya dönüþtürür.
    /// Formül: Gray = 0.299*R + 0.587*G + 0.114*B (standart lüminans formülü)
    /// </summary>
    public class GrayscaleFilter : ImageFilter
    {
        public override string FilterName => "Gri Tonlama (Grayscale)";
        public override string FilterDescription => "Renkli görüntüyü gri tonlamaya dönüþtürür";

        /// <summary>
        /// Grayscale filtresini uyguluyor.
        /// LockBits kullanarak yüksek performans saðlar.
        /// </summary>
        public override Bitmap Apply(Bitmap bitmap)
        {
            // Orijinal bitmap'in kopyasýný oluþtur
            Bitmap result = CloneBitmap(bitmap);

            // LockBits ile piksel manipülasyonunu baþlat
            ProcessBitmapWithLockBits(result, (bitmapData, bytesPerPixel) =>
            {
                int height = bitmapData.Height;
                int width = bitmapData.Width;
                int stride = bitmapData.Stride;

                unsafe
                {
                    // Piksel verilerine doðrudan iþaretçi eriþimi
                    byte* ptr = (byte*)bitmapData.Scan0;

                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            // Piksel konumunu hesapla
                            int offset = y * stride + x * bytesPerPixel;

                            // BGR formatýnda piksel kanallarýný oku (Windows GDI+ BGR kullanýr)
                            byte b = ptr[offset];
                            byte g = ptr[offset + 1];
                            byte r = ptr[offset + 2];

                            // Lüminans formülünü kullanarak gri deðeri hesapla
                            byte grayValue = (byte)(0.299 * r + 0.587 * g + 0.114 * b);

                            // Tüm kanallarý gri deðere ayarla
                            ptr[offset] = grayValue;      // B
                            ptr[offset + 1] = grayValue;  // G
                            ptr[offset + 2] = grayValue;  // R
                        }
                    }
                }
            });

            return result;
        }
    }
}

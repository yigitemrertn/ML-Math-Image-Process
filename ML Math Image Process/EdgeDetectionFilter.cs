using System.Drawing;
using System.Drawing.Imaging;

namespace ML_Math_Image_Process
{
    /// <summary>
    /// Edge Detection (Kenar Bulma) Filtresi
    /// Sobel operatörünü kullanarak görüntüdeki kenarlarý tespit eder.
    /// Sobel X kernel (dikey kenarlarý tespit eder):
    /// [-1  0  1 ]
    /// [-2  0  2 ]
    /// [-1  0  1 ]
    /// Sobel Y kernel (yatay kenarlarý tespit eder):
    /// [-1 -2 -1 ]
    /// [ 0  0  0 ]
    /// [ 1  2  1 ]
    /// </summary>
    public class EdgeDetectionFilter : ImageFilter
    {
        public override string FilterName => "Kenar Bulma (Edge Detection)";
        public override string FilterDescription => "Sobel operatorunu kullanarak goruntudeki kenarlari tespit eder";

        // Sobel X kernel (dikey kenarlar)
        private readonly int[] _sobelX = new int[]
        {
            -1, 0, 1,
            -2, 0, 2,
            -1, 0, 1
        };

        // Sobel Y kernel (yatay kenarlar)
        private readonly int[] _sobelY = new int[]
        {
            -1, -2, -1,
             0,  0,  0,
             1,  2,  1
        };

        /// <summary>
        /// Edge Detection filtresini uyguluyor.
        /// Sobel operatörü kullanarak gradyan hesaplar.
        /// </summary>
        public override Bitmap Apply(Bitmap bitmap)
        {
            // Ýlk olarak grayscale'e dönüþtür
            GrayscaleFilter grayscale = new GrayscaleFilter();
            Bitmap grayBitmap = grayscale.Apply(bitmap);

            // Sonuç bitmap'i oluþtur
            Bitmap result = new Bitmap(grayBitmap.Width, grayBitmap.Height, PixelFormat.Format32bppArgb);

            // Gri bitmap'in piksel verilerine eriþim saðla
            BitmapData sourceBitmapData = grayBitmap.LockBits(
                new Rectangle(0, 0, grayBitmap.Width, grayBitmap.Height),
                ImageLockMode.ReadOnly,
                grayBitmap.PixelFormat);

            BitmapData resultBitmapData = result.LockBits(
                new Rectangle(0, 0, result.Width, result.Height),
                ImageLockMode.WriteOnly,
                result.PixelFormat);

            try
            {
                int bytesPerPixel = Image.GetPixelFormatSize(grayBitmap.PixelFormat) / 8;
                int resultBytesPerPixel = 4; // ARGB
                int height = grayBitmap.Height;
                int width = grayBitmap.Width;
                int stride = sourceBitmapData.Stride;
                int resultStride = resultBitmapData.Stride;

                unsafe
                {
                    byte* srcPtr = (byte*)sourceBitmapData.Scan0;
                    byte* resultPtr = (byte*)resultBitmapData.Scan0;

                    // Tüm pikselleri iþle (border dahil)
                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            // Sobel operatörünü uygula
                            int sobelXValue = 0;
                            int sobelYValue = 0;

                            // Sobel kernel matrisini uygula
                            for (int ky = -1; ky <= 1; ky++)
                            {
                                for (int kx = -1; kx <= 1; kx++)
                                {
                                    int pixelX = x + kx;
                                    int pixelY = y + ky;

                                    // Sýnýr kontrolü
                                    if (pixelX < 0 || pixelX >= width || pixelY < 0 || pixelY >= height)
                                    {
                                        // Sýnýr dýþýnda ise 0 kabul et (padding)
                                        continue;
                                    }

                                    // Gri görüntüde ilk kanal piksel deðeri
                                    int pixelOffset = pixelY * stride + pixelX * bytesPerPixel;
                                    byte pixelValue = srcPtr[pixelOffset];

                                    int kernelX = _sobelX[(ky + 1) * 3 + (kx + 1)];
                                    int kernelY = _sobelY[(ky + 1) * 3 + (kx + 1)];

                                    sobelXValue += pixelValue * kernelX;
                                    sobelYValue += pixelValue * kernelY;
                                }
                            }

                            // Gradyan büyüklüðünü hesapla (Euclidean norm)
                            // Bazý kaynaklara göre Manhattan distance kullanabilirsiniz:
                            // int magnitude = System.Math.Abs(sobelXValue) + System.Math.Abs(sobelYValue);
                            int magnitude = (int)System.Math.Sqrt(sobelXValue * sobelXValue + sobelYValue * sobelYValue);
                            byte edgeValue = ClampValue(magnitude);

                            // Sonuç bitmap'e yaz (ARGB format)
                            int resultOffset = y * resultStride + x * resultBytesPerPixel;
                            resultPtr[resultOffset] = edgeValue;      // B
                            resultPtr[resultOffset + 1] = edgeValue;  // G
                            resultPtr[resultOffset + 2] = edgeValue;  // R
                            resultPtr[resultOffset + 3] = 255;        // A (tam opak)
                        }
                    }
                }
            }
            finally
            {
                grayBitmap.UnlockBits(sourceBitmapData);
                result.UnlockBits(resultBitmapData);
                grayBitmap.Dispose();
            }

            return result;
        }

        /// <summary>
        /// Piksel deðerini 0-255 aralýðýnda sýnýrlandýrýr
        /// </summary>
        private byte ClampValue(int value)
        {
            if (value < 0) return 0;
            if (value > 255) return 255;
            return (byte)value;
        }
    }
}

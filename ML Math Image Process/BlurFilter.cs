using System.Drawing;
using System.Drawing.Imaging;

namespace ML_Math_Image_Process
{
    /// <summary>
    /// Blur (Bulanýklaþtýrma) Filtresi
    /// Convolution operatörü kullanarak görüntüyü yumuþatýr.
    /// 3x3 Gaussian Blur kerneli uygular.
    /// Kernel:
    /// [ 1  2  1 ]
    /// [ 2  4  2 ] / 16
    /// [ 1  2  1 ]
    /// </summary>
    public class BlurFilter : ImageFilter
    {
        public override string FilterName => "Bulaniklastirma (Blur)";
        public override string FilterDescription => "Goruntu 3x3 Gaussian blur kerneli ile yumusatir";

        // Blur kernel (3x3)
        private readonly float[] _kernel = new float[]
        {
            1, 2, 1,
            2, 4, 2,
            1, 2, 1
        };

        private const float _kernelDivisor = 16f;

        /// <summary>
        /// Blur filtresini uyguluyor.
        /// Convolution operatörü kullanýr.
        /// </summary>
        public override Bitmap Apply(Bitmap bitmap)
        {
            // Orijinal bitmap'i kopyalayarak baþla (border pikselleri korumak için)
            Bitmap result = CloneBitmap(bitmap);

            // Sonuç bitmap'ini LockBits ile iþle
            BitmapData resultBitmapData = result.LockBits(
                new Rectangle(0, 0, result.Width, result.Height),
                ImageLockMode.ReadWrite,
                result.PixelFormat);

            // Kaynak bitmap'i de kilitlemek için
            BitmapData sourceBitmapData = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly,
                bitmap.PixelFormat);

            try
            {
                int bytesPerPixel = Image.GetPixelFormatSize(bitmap.PixelFormat) / 8;
                int height = bitmap.Height;
                int width = bitmap.Width;
                int stride = sourceBitmapData.Stride;

                unsafe
                {
                    byte* srcPtr = (byte*)sourceBitmapData.Scan0;
                    byte* resultPtr = (byte*)resultBitmapData.Scan0;

                    // Tüm pikselleri iþle (border dahil)
                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            // Convolution operatörünü 3x3 kernel ile uygula
                            float blueSum = 0, greenSum = 0, redSum = 0;
                            float kernelWeightSum = 0;

                            // 3x3 kernel matrisini uygula
                            for (int ky = -1; ky <= 1; ky++)
                            {
                                for (int kx = -1; kx <= 1; kx++)
                                {
                                    int pixelX = x + kx;
                                    int pixelY = y + ky;

                                    // Sýnýr kontrolü - görüntü dýþýnda gitmeme
                                    if (pixelX < 0 || pixelX >= width || pixelY < 0 || pixelY >= height)
                                    {
                                        continue; // Sýnýr dýþý pikselleri atla
                                    }

                                    int pixelOffset = pixelY * stride + pixelX * bytesPerPixel;
                                    float kernelValue = _kernel[(ky + 1) * 3 + (kx + 1)];

                                    blueSum += srcPtr[pixelOffset] * kernelValue;
                                    greenSum += srcPtr[pixelOffset + 1] * kernelValue;
                                    redSum += srcPtr[pixelOffset + 2] * kernelValue;
                                    kernelWeightSum += kernelValue;
                                }
                            }

                            // Kernel deðerleri ile böl
                            // Border pikselleri daha az weight ile iþlenecek
                            int resultOffset = y * stride + x * bytesPerPixel;
                            if (kernelWeightSum > 0)
                            {
                                resultPtr[resultOffset] = ClampValue((int)(blueSum / kernelWeightSum));
                                resultPtr[resultOffset + 1] = ClampValue((int)(greenSum / kernelWeightSum));
                                resultPtr[resultOffset + 2] = ClampValue((int)(redSum / kernelWeightSum));

                                // Alpha kanali varsa koru
                                if (bytesPerPixel == 4)
                                {
                                    resultPtr[resultOffset + 3] = 255; // Full opacity
                                }
                            }
                        }
                    }
                }
            }
            finally
            {
                bitmap.UnlockBits(sourceBitmapData);
                result.UnlockBits(resultBitmapData);
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

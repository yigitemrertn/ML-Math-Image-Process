using System.Drawing;

namespace ML_Math_Image_Process
{
    /// <summary>
    /// Filtrelerin düzgün çalýþýp çalýþmadýðýný test eder
    /// </summary>
    public static class FilterTestHelper
    {
        /// <summary>
        /// Test bitmap oluþtur (basit pattern)
        /// </summary>
        public static Bitmap CreateTestBitmap(int width = 100, int height = 100)
        {
            Bitmap bitmap = new Bitmap(width, height);

            // Basit bir gradient pattern oluþtur
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int r = (x * 255) / width;
                    int g = (y * 255) / height;
                    int b = 128;
                    bitmap.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }

            return bitmap;
        }

        /// <summary>
        /// Bitmap'in siyah olup olmadýðýný kontrol et
        /// </summary>
        public static bool IsAllBlack(Bitmap bitmap)
        {
            for (int y = 0; y < Math.Min(bitmap.Height, 10); y++)
            {
                for (int x = 0; x < Math.Min(bitmap.Width, 10); x++)
                {
                    Color pixel = bitmap.GetPixel(x, y);
                    // Eðer herhangi bir piksel siyah deðilse, görüntü siyah deðil
                    if (pixel.R > 10 || pixel.G > 10 || pixel.B > 10)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Bitmap'in boþ olup olmadýðýný kontrol et
        /// </summary>
        public static bool HasContent(Bitmap bitmap)
        {
            if (bitmap == null || bitmap.Width < 1 || bitmap.Height < 1)
                return false;

            // Rastgele pikselleri kontrol et
            Random random = new Random(42);
            for (int i = 0; i < 20; i++)
            {
                int x = random.Next(bitmap.Width);
                int y = random.Next(bitmap.Height);
                Color pixel = bitmap.GetPixel(x, y);

                // En az bir piksel siyah olmayan ise içerik var
                if (pixel.R > 0 || pixel.G > 0 || pixel.B > 0)
                    return true;
            }

            return false;
        }
    }
}

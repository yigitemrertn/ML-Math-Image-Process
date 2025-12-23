using BenchmarkDotNet.Attributes;
using System;
using System.Drawing;
using Microsoft.VSDiagnostics;

namespace ML_Math_Image_Process
{
    [CPUUsageDiagnoser]
    public class PixelManipulationBenchmark
    {
        private Bitmap? _testBitmap;

        [GlobalSetup]
        public void Setup()
        {
            // 800x600 test görüntüsü oluþtur
            _testBitmap = new Bitmap(800, 600);

            // Test görüntüsünü rastgele piksellerle doldur
            Random random = new Random(42);
            for (int y = 0; y < 600; y++)
            {
                for (int x = 0; x < 800; x++)
                {
                    int r = random.Next(256);
                    int g = random.Next(256);
                    int b = random.Next(256);
                    _testBitmap.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            _testBitmap?.Dispose();
        }

        /// <summary>
        /// SetPixel yöntemiyle piksel manipülasyonunun performansýný ölçer.
        /// Bu yöntem yavaþ olmasýyla bilinir çünkü her piksel için GDI+ çaðrýsý yapar.
        /// </summary>
        [Benchmark(Description = "SetPixel method - slow pixel manipulation")]
        public void SetPixelMethod()
        {
            if (_testBitmap == null)
                return;

            for (int y = 0; y < _testBitmap.Height; y++)
            {
                for (int x = 0; x < _testBitmap.Width; x++)
                {
                    Color pixel = _testBitmap.GetPixel(x, y);
                    // Piksel deðerini açýk kýrmýzýya çevir (test amaçlý)
                    _testBitmap.SetPixel(x, y, Color.FromArgb(255, pixel.G, pixel.B));
                }
            }
        }

        /// <summary>
        /// LockBits yöntemiyle piksel manipülasyonunun performansýný ölçer.
        /// Bu yöntem hýzlýdýr çünkü bellek sabitlemesi ve doðrudan iþaretçi eriþimi kullanýr.
        /// </summary>
        [Benchmark(Description = "LockBits method - fast pixel manipulation")]
        public void LockBitsMethod()
        {
            if (_testBitmap == null)
                return;

            // Bitmap'i kilitleyerek doðrudan bellek eriþimi saðla
            System.Drawing.Imaging.BitmapData bitmapData = _testBitmap.LockBits(
                new Rectangle(0, 0, _testBitmap.Width, _testBitmap.Height),
                System.Drawing.Imaging.ImageLockMode.ReadWrite,
                _testBitmap.PixelFormat);

            try
            {
                int bytesPerPixel = System.Drawing.Image.GetPixelFormatSize(bitmapData.PixelFormat) / 8;
                int height = bitmapData.Height;
                int width = bitmapData.Width;
                int stride = bitmapData.Stride;

                unsafe
                {
                    byte* ptr = (byte*)bitmapData.Scan0;

                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            int offset = y * stride + x * bytesPerPixel;
                            // Piksel deðerlerini güncelle (test amaçlý)
                            ptr[offset + 2] = 255; // B
                            ptr[offset + 1] = ptr[offset + 1]; // G
                            ptr[offset] = ptr[offset]; // R
                        }
                    }
                }
            }
            finally
            {
                // Bitmap'i kilidi aç
                _testBitmap.UnlockBits(bitmapData);
            }
        }
    }
}

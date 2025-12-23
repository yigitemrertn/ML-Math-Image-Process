namespace ML_Math_Image_Process
{
    public partial class Form1 : Form
    {
        private ImageProcessor _imageProcessor = new();
        private string _currentFilterName = "Orijinal";

        public Form1()
        {
            InitializeComponent();
            btnLoadImage.Click += BtnLoadImage_Click;
            btnGrayscale.Click += BtnGrayscale_Click;
            btnBlur.Click += BtnBlur_Click;
            btnEdgeDetection.Click += BtnEdgeDetection_Click;
            btnReset.Click += BtnReset_Click;
            btnSaveImage.Click += BtnSaveImage_Click;
            Load += (s, e) => UpdateStatusLabel();
        }

        private void BtnLoadImage_Click(object? sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new())
            {
                openFileDialog.Filter = "Resim Dosyalarý|*.jpg;*.jpeg;*.png;*.bmp;*.gif|Tüm Dosyalar|*.*";
                openFileDialog.Title = "Bir Resim Seçiniz";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _imageProcessor.LoadImage(openFileDialog.FileName);
                        _currentFilterName = "Orijinal";
                        pictureBoxImage.Image = _imageProcessor.CurrentImage;
                        UpdateStatusLabel();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnGrayscale_Click(object? sender, EventArgs e)
        {
            if (!_imageProcessor.IsImageLoaded)
            {
                MessageBox.Show("Lütfen önce bir resim yükleyiniz!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                GrayscaleFilter grayscaleFilter = new();
                _imageProcessor.ApplyFilter(grayscaleFilter);
                _currentFilterName = grayscaleFilter.FilterName;
                pictureBoxImage.Image = _imageProcessor.CurrentImage;
                UpdateStatusLabel();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnBlur_Click(object? sender, EventArgs e)
        {
            if (!_imageProcessor.IsImageLoaded)
            {
                MessageBox.Show("Lütfen önce bir resim yükleyiniz!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                BlurFilter blurFilter = new();
                _imageProcessor.ApplyFilter(blurFilter);
                _currentFilterName = blurFilter.FilterName;
                pictureBoxImage.Image = _imageProcessor.CurrentImage;
                UpdateStatusLabel();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEdgeDetection_Click(object? sender, EventArgs e)
        {
            if (!_imageProcessor.IsImageLoaded)
            {
                MessageBox.Show("Lütfen önce bir resim yükleyiniz!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                EdgeDetectionFilter edgeDetectionFilter = new();
                _imageProcessor.ApplyFilter(edgeDetectionFilter);
                _currentFilterName = edgeDetectionFilter.FilterName;
                pictureBoxImage.Image = _imageProcessor.CurrentImage;
                UpdateStatusLabel();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnReset_Click(object? sender, EventArgs e)
        {
            if (!_imageProcessor.IsImageLoaded)
            {
                MessageBox.Show("Lütfen önce bir resim yükleyiniz!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _imageProcessor.ResetToOriginal();
                _currentFilterName = "Orijinal";
                pictureBoxImage.Image = _imageProcessor.CurrentImage;
                UpdateStatusLabel();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSaveImage_Click(object? sender, EventArgs e)
        {
            if (!_imageProcessor.IsImageLoaded)
            {
                MessageBox.Show("Lütfen önce bir resim yükleyiniz!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog saveFileDialog = new())
            {
                saveFileDialog.Filter = "PNG Dosyasý|*.png|JPG Dosyasý|*.jpg|BMP Dosyasý|*.bmp";
                saveFileDialog.Title = "Resmi Kaydet";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _imageProcessor.SaveImage(saveFileDialog.FileName);
                        MessageBox.Show("Resim baþarýyla kaydedildi!", "Baþarýlý", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void UpdateStatusLabel()
        {
            if (_imageProcessor.IsImageLoaded)
            {
                var img = _imageProcessor.CurrentImage;
                labelStatus.Text = $"Durum: Yüklü\n\nBoy: {img?.Width}x{img?.Height}\n\nFiltre: {_currentFilterName}";
            }
            else
            {
                labelStatus.Text = "Durum: Bekleniyor\n\nResim Yükle";
            }
        }
    }
}

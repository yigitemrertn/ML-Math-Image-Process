namespace ML_Math_Image_Process
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            panelControls = new Panel();
            btnLoadImage = new Button();
            btnGrayscale = new Button();
            btnBlur = new Button();
            btnEdgeDetection = new Button();
            btnReset = new Button();
            btnSaveImage = new Button();
            labelStatus = new Label();
            panelImageDisplay = new Panel();
            pictureBoxImage = new PictureBox();

            panelControls.SuspendLayout();
            panelImageDisplay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxImage).BeginInit();
            SuspendLayout();

            // panelControls
            panelControls.BackColor = Color.LightGray;
            panelControls.BorderStyle = BorderStyle.Fixed3D;
            panelControls.Controls.Add(btnLoadImage);
            panelControls.Controls.Add(btnGrayscale);
            panelControls.Controls.Add(btnBlur);
            panelControls.Controls.Add(btnEdgeDetection);
            panelControls.Controls.Add(btnReset);
            panelControls.Controls.Add(btnSaveImage);
            panelControls.Controls.Add(labelStatus);
            panelControls.Dock = DockStyle.Right;
            panelControls.Location = new Point(650, 0);
            panelControls.Name = "panelControls";
            panelControls.Size = new Size(150, 550);
            panelControls.TabIndex = 0;

            // btnLoadImage
            btnLoadImage.Location = new Point(10, 10);
            btnLoadImage.Name = "btnLoadImage";
            btnLoadImage.Size = new Size(130, 40);
            btnLoadImage.TabIndex = 0;
            btnLoadImage.Text = "Resim Yukle";
            btnLoadImage.UseVisualStyleBackColor = true;

            // btnGrayscale
            btnGrayscale.Location = new Point(10, 60);
            btnGrayscale.Name = "btnGrayscale";
            btnGrayscale.Size = new Size(130, 40);
            btnGrayscale.TabIndex = 1;
            btnGrayscale.Text = "Gri Ton";
            btnGrayscale.UseVisualStyleBackColor = true;

            // btnBlur
            btnBlur.Location = new Point(10, 110);
            btnBlur.Name = "btnBlur";
            btnBlur.Size = new Size(130, 40);
            btnBlur.TabIndex = 2;
            btnBlur.Text = "Bulanik";
            btnBlur.UseVisualStyleBackColor = true;

            // btnEdgeDetection
            btnEdgeDetection.Location = new Point(10, 160);
            btnEdgeDetection.Name = "btnEdgeDetection";
            btnEdgeDetection.Size = new Size(130, 40);
            btnEdgeDetection.TabIndex = 3;
            btnEdgeDetection.Text = "Kenar";
            btnEdgeDetection.UseVisualStyleBackColor = true;

            // btnReset
            btnReset.Location = new Point(10, 210);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(130, 40);
            btnReset.TabIndex = 4;
            btnReset.Text = "Sifirla";
            btnReset.UseVisualStyleBackColor = true;

            // btnSaveImage
            btnSaveImage.Location = new Point(10, 260);
            btnSaveImage.Name = "btnSaveImage";
            btnSaveImage.Size = new Size(130, 40);
            btnSaveImage.TabIndex = 5;
            btnSaveImage.Text = "Kaydet";
            btnSaveImage.UseVisualStyleBackColor = true;

            // labelStatus
            labelStatus.AutoSize = true;
            labelStatus.Location = new Point(10, 320);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(120, 120);
            labelStatus.TabIndex = 6;
            labelStatus.Text = "Durum: Bekleniyor";

            // panelImageDisplay
            panelImageDisplay.BackColor = Color.Black;
            panelImageDisplay.BorderStyle = BorderStyle.Fixed3D;
            panelImageDisplay.Controls.Add(pictureBoxImage);
            panelImageDisplay.Dock = DockStyle.Fill;
            panelImageDisplay.Location = new Point(0, 0);
            panelImageDisplay.Name = "panelImageDisplay";
            panelImageDisplay.Size = new Size(650, 550);
            panelImageDisplay.TabIndex = 1;

            // pictureBoxImage
            pictureBoxImage.Dock = DockStyle.Fill;
            pictureBoxImage.Location = new Point(0, 0);
            pictureBoxImage.Name = "pictureBoxImage";
            pictureBoxImage.Size = new Size(650, 550);
            pictureBoxImage.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxImage.TabIndex = 0;
            pictureBoxImage.TabStop = false;

            // Form1
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 550);
            Controls.Add(panelImageDisplay);
            Controls.Add(panelControls);
            Name = "Form1";
            Text = "Goruntu Isleme Uygulamasi";
            panelControls.ResumeLayout(false);
            panelControls.PerformLayout();
            panelImageDisplay.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxImage).EndInit();
            ResumeLayout(false);
        }

        private Panel panelControls;
        private Button btnLoadImage;
        private Button btnGrayscale;
        private Button btnBlur;
        private Button btnEdgeDetection;
        private Button btnReset;
        private Button btnSaveImage;
        private Label labelStatus;
        private Panel panelImageDisplay;
        private PictureBox pictureBoxImage;
    }
}

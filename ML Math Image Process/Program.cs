namespace ML_Math_Image_Process
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Bileþenlerin düzgün baþlatýldýðýný kontrol et
            try
            {
                ApplicationDiagnostics.CheckInitialization();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Baslat error: {ex.Message}\n\n{ex.StackTrace}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}
        
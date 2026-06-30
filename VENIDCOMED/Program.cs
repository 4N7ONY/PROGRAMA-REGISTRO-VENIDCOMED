namespace VENIDCOMED
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Form1 main = new Form1();
            // Mostrar login antes de arrancar la aplicación principal
            bool ok = false;
            try
            {
                ok = main.Invoke((Func<bool>)(() => main.Visible ? true : true));
            }
            catch { }

            // Usar método MostrarLogin si existe
            bool loginOk = false;
            try
            {
                var metodo = main.GetType().GetMethod("MostrarLogin", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                if (metodo != null)
                {
                    loginOk = (bool)metodo.Invoke(main, null);
                }
            }
            catch { }

            if (loginOk)
                Application.Run(main);
            else
                MessageBox.Show("Acceso denegado. Saliendo...");
        }
    }
}
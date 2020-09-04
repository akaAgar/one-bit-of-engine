using System;
using System.Windows.Forms;

namespace OneBitOfEngine.Tools
{
    public static class OneBitOfDevTools
    {
        /// <summary>
        /// Main application entry point.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Forms.MainMenuForm());
        }
    }
}

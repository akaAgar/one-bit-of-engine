using System;
using System.Windows.Forms;

namespace AsterionEngine.Tools
{
    public static class AsterionTools
    {
        /// <summary>
        /// Main application entry point.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Forms.MainForm());
        }
    }
}

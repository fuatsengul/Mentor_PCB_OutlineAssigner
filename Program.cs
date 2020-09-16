using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace xPCB_OutlineAssigner
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main( string[] args )
        {
            string _appGUID = null;
            if ( args.Length == 2 )
            {
                if ( args[0] == "-guid" && args[1] != null )
                {
                    _appGUID = args[1];
                }

            }
            DarkUI.Config.Colors.LightMode = false;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new OutlineAssigner(_appGUID));
        }
    }
}

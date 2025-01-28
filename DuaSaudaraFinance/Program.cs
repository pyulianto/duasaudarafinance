using DuaSaudaraFinance.Transaksi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DuaSaudaraFinance.GlobalVariable;
using static DuaSaudaraFinance.LibSQLServer;

namespace DuaSaudaraFinance
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            if (System.Net.Dns.GetHostName() == "PrimaLegion")
            {
                ConnString = "Server=192.168.1.120; User Id=sa; password=ilc960x6; Database=DuaSaudaraFinance; MultipleActiveResultSets=True";
                IsOwner = 1;
                UserName = "Administrator";


                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // Show the Login form first
                LoginForm loginForm = new LoginForm();
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    Application.Run(new frmMainWindow());
                }



            }
            else
            {
                Application.Run(new frmMainWindow());
            }

            
            


        }
    }
}

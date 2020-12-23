using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp.UpgradeDemo
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            FrmUpdateStart frmStart = new FrmUpdateStart();
            DialogResult result = frmStart.ShowDialog();

            if (result == DialogResult.OK)
                Application.Run(new FrmUpdate());
            else
                Application.Exit();
        }
    }
}

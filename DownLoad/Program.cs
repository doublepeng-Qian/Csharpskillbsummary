using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DownLoad
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string [] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string tmp = args[0]; // 只取第一个参数
            //string tmp = "COM1"; // 只取第一个参数
            if (tmp.Length == 0)
            {
                Application.Run(new DownLoadFile());
            }
            else
            {
                Application.Run(new DownLoadFile(tmp));
            }
        }
    }
}

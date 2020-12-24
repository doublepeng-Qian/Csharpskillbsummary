using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Threading;
//using MetroFramework.Forms;

namespace DownLoad
{
    /// <summary>
    /// 
    /// </summary>
    public partial class DownLoadFile : Form
    {
        private DownLoadManager objUpdateManager = new DownLoadManager();
        public SerialPort port = new SerialPort();
        public DOWNLOADFILE_PROCESS cmd;
        public DOWNLOADFILE_PROCESS current_cmd;
        private bool brushing_flag = true;
        private int line_num = 0;
        private int downloaded_lines = 0;

        MillisecondTimer _sysTimer;
        public UInt32 counter = 0;

        private Thread th1; //声明全局线程
        private byte handle_flag = 0;

        // 是否正在关闭串口，执行Application.DoEvents，并阻止再次Invoke
        private bool closing = false;

        // 是否没有执行完Invoke相关操作，如果不加，在关闭串口时程序会卡死
        private bool listening = false;

        /// <summary>
        /// 文件下载过程
        /// </summary>
        public enum DOWNLOADFILE_PROCESS : byte
        {
            /// <summary>
            /// 
            /// </summary>
            NONE = 0,
            /// <summary>
            /// 
            /// </summary>
            GOTOBRUSH = 1,
            /// <summary>
            /// 
            /// </summary>
            CHECKSUM = 2,
            /// <summary>
            /// 
            /// </summary>
            MAXBIN = 3,
            /// <summary>
            /// 
            /// </summary>
            WDATE = 4,
            /// <summary>
            /// 
            /// </summary>
            BRUSHEND = 5,
        }

        /// <summary>
        /// 
        /// </summary>
        public DownLoadFile()
        {
            // InitializeComponent();
            // Init();
            MessageBox.Show("请先选择串口号，并打开，再进行烧录操作！", "提示：");
        }

        string uart_port_nume = null;
        public DownLoadFile(string args)
        {
            InitializeComponent();
            uart_port_nume = args;
            Init();   
        }

        /// <summary>
        /// 初始化方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Init()
        {
            // 将同步显示进度的方法和委托变量关联
            this.objUpdateManager.ShowProgressDelegate = this.ShowUpdateProgress;
            // 初始化【注意，如果不初始化，程序不知道这个数据类型的大小，无法分配存储空间】
            this.objUpdateManager.downLoadInfo.FileContent = new byte[0x40000];
            this.objUpdateManager.downLoadInfo.FileSize = 0;
            this.objUpdateManager.downLoadInfo.FileCheckSum = 0;
            this.objUpdateManager.downLoadInfo.FileLine = new List<byte[]>();
            // 将显示数据帧的方法和委托变量关联
            this.objUpdateManager.ShowRTFramesDelegate = this.ShowRTFrames;

            try
            {
                port.PortName = uart_port_nume;
                port.BaudRate = int.Parse("115200");
                port.DataBits = 8;
                port.NewLine = "\r";
                uart_connect();
            }
            catch
            {
                MessageBox.Show("未发现串口设备，请检查！","提示：");
                Application.Exit();
            }
        }

        /// <summary>
        /// 加载文件触发方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            btnLoadFile.Enabled = false;

            // 现将
            this.objUpdateManager.downLoadInfo.FileLine.Clear();
            this.objUpdateManager.downLoadInfo.FileCheckSum = 0;
            this.objUpdateManager.downLoadInfo.FileLineNum = 0;
            this.objUpdateManager.downLoadInfo.FileSize = 0;

            // 实例化选择文件对话框
            OpenFileDialog fileDialog = new OpenFileDialog();

            /* 不允许同时选定多个文件 */
            fileDialog.Multiselect = false;

            fileDialog.Filter = "所有文件(*.*)|*.bin*";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                //将文件信息赋给Info
                this.objUpdateManager.downLoadInfo.UpdateFilePth = fileDialog.FileName;

                tbFilePath.Text = fileDialog.FileName;

                // 调用文件加载
                if (this.objUpdateManager.LoadFileProcess())
                {
                    // 
                    btnStart.Enabled = true;
                }
                else
                {
                    btnLoadFile.Enabled = true;
                }
            }
        }


        /// <summary>
        /// 烧录触发方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            btnReference.Enabled = true;
            cmd = DOWNLOADFILE_PROCESS.GOTOBRUSH;

            th1 = new Thread(DownLoadFileProcess);
            //if (th1 != null)
            //    th1.Start();
            th1.IsBackground = true; //定义为后台线程
            th1.Start();
        }

        private void DownLoadFileProcess()
        {
            // 初始化ms定时器
            MSTimerStart();

            do
            {
                Application.DoEvents();

                try
                {
                    switch (cmd)
                    {
                        case DOWNLOADFILE_PROCESS.NONE:
                            break;
                        case DOWNLOADFILE_PROCESS.GOTOBRUSH:
                            {
                                Thread.Sleep(50);
                                cmd = DOWNLOADFILE_PROCESS.NONE;
                                port.Write("cmd gotobrush" + "\r\n");
                                current_cmd = DOWNLOADFILE_PROCESS.GOTOBRUSH;
                                _sysTimer.Start();
                            }
                            break;
                        case DOWNLOADFILE_PROCESS.CHECKSUM:
                            {
                                Thread.Sleep(50);
                                cmd = DOWNLOADFILE_PROCESS.NONE;
                                port.Write("brush checksum " + "0x" +
                                        Convert.ToString(this.objUpdateManager.downLoadInfo.FileCheckSum, 16) + "\r\nOK\r\n");
                                current_cmd = DOWNLOADFILE_PROCESS.CHECKSUM;
                                _sysTimer.Start();
                            }
                            break;
                        case DOWNLOADFILE_PROCESS.MAXBIN:
                            {
                                Thread.Sleep(50);
                                cmd = DOWNLOADFILE_PROCESS.NONE;
                                port.Write("brush maxbin " + "0x" +
                                        Convert.ToString(this.objUpdateManager.downLoadInfo.FileSize, 16) + "\r\nOK\r\n");
                                current_cmd = DOWNLOADFILE_PROCESS.MAXBIN;
                                _sysTimer.Start();
                            }
                            break;
                        case DOWNLOADFILE_PROCESS.WDATE:
                            {
                                Thread.Sleep(80);
                                cmd = DOWNLOADFILE_PROCESS.NONE;
                                port.Write(this.objUpdateManager.downLoadInfo.FileLine[line_num],
                                                    0,
                                                    this.objUpdateManager.downLoadInfo.FileLine[line_num].Length);
                                line_num++;
                                current_cmd = DOWNLOADFILE_PROCESS.WDATE;
                                _sysTimer.Start();
                            }
                            break;
                        case DOWNLOADFILE_PROCESS.BRUSHEND:
                            {
                                Thread.Sleep(50);
                                cmd = DOWNLOADFILE_PROCESS.NONE;
                                port.Write("cmd BrushEnd" + "\r\nOK\r\n");
                                current_cmd = DOWNLOADFILE_PROCESS.BRUSHEND;
                                _sysTimer.Start();
                            }
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            } while (brushing_flag);
        }

        /// <summary>
        /// 根据委托定义一个同步下载进度显示
        /// </summary>
        /// <param name="fileIndex">文件顺序</param>
        /// <param name="finishedPercent">已经完成的百分比</param>
        private void ShowUpdateProgress(int finishedPercent)
        {
            //在列表框对应的文件信息最后显示下载百分比
            //this.lvUpdateList.Items[fileIndex].SubItems[3].Text = finishedPercent + "%";
            //进度条显示
            this.pbDownLoad.Maximum = 100;
            this.pbDownLoad.Value = finishedPercent;
            lblPercentage.Text = finishedPercent.ToString() + "%";
        }

        private void ShowRTFrames(string str)
        {
            string currenttime = DateTime.Now.ToString();//获取系统当前时间
            this.Invoke((EventHandler)(delegate
            {
                lbProcess.Items.Add(currenttime + " : " + str);
            }));
        }

        /// <summary>
        /// 
        /// </summary>
        public void UpgradLogic_Init()
        {
            // 添加事件注册
            port.DataReceived += port_DataReceived;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (closing) return;

            try
            {
                string str = "";
                listening = true;
                str = port.ReadLine();

                //string currenttime = DateTime.Now.ToString();//获取系统当前时间
                //this.Invoke((EventHandler)(delegate
                //{
                //    lbProcess.Items.Add(currenttime + " : " + str);
                //}));
                ShowRTFrames(str);
                DataReceived_Handle(str);
            }
            finally
            {
                listening = false;//我用完了，ui可以关闭串口了。   
            }
        }

        /// <summary>
        /// 接收处理函数
        /// </summary>
        /// <param name="str"></param>
        private void DataReceived_Handle(string str)
        {
            // 记录接收
            //RecordData_testlog(str);

            // 开始刷写响应
            if ((Char_Match("Brush GotoBrush", str, (decimal)0.7)) && (handle_flag == 0))
            {
                counter = 0;
                cmd = DOWNLOADFILE_PROCESS.CHECKSUM;
                handle_flag = 0x01;
            }
            // 文件校验响应
            //if (Char_Match("Brush CheckSum "+ "0x" +
            //        Convert.ToString(this.objUpdateManager.downLoadInfo.FileCheckSum, 16), str))
            if ((Char_Match("Brush CheckSum 0x" +
                Convert.ToString(this.objUpdateManager.downLoadInfo.FileCheckSum, 16), 
                str, (decimal)0.6)) && (handle_flag == 0x01))
            {
                counter = 0;
                cmd = DOWNLOADFILE_PROCESS.MAXBIN;
                handle_flag = 0x02;
            }
            // 总的字节数响应
            //if (Char_Match("Brush MaxBin " + "0x" +
            //        Convert.ToString(this.objUpdateManager.downLoadInfo.FileSize, 16), str))
            if ((Char_Match("Brush MaxBin 0x" +
                Convert.ToString(this.objUpdateManager.downLoadInfo.FileSize, 16), 
                str, (decimal)0.6)) && (handle_flag == 0x02))
            {
                counter = 0;
                cmd = DOWNLOADFILE_PROCESS.WDATE;
                handle_flag = 0x03;
            }
            // 行刷写完成
            //if ("?rush ?Data ?ass" == str)
            //{
            //    Char_Match("Brush WData Pass", str, (decimal)0.5);
            //}
            if ((Char_Match("Brush WData Pass", str, (decimal)0.5)) && (handle_flag == 0x03))
            {
                counter = 0;
                // 如果是最后一行，则进入结束模式
                if (downloaded_lines < (this.objUpdateManager.downLoadInfo.FileLine.Count - 1))
                {
                    cmd = DOWNLOADFILE_PROCESS.WDATE;

                    // 同步更新进度百分比（整数）
                    float total = (float)this.objUpdateManager.downLoadInfo.FileLine.Count - 1; //总行数
                    int percent = Convert.ToInt32(((float)downloaded_lines++ / total) * 100);

                    this.Invoke((EventHandler)(delegate
                    {
                        lblPercentage.Text = percent.ToString() + "%";
                        pbDownLoad.Maximum = 100;
                        pbDownLoad.Value = percent;
                    }));
                    // TODO: 进度显示委托
                    //ShowUpdateProgress(percent);
                }
                else
                {
                    cmd = DOWNLOADFILE_PROCESS.BRUSHEND;
                    handle_flag = 0x04;
                }
                
                // 显示行号
                //this.Invoke((EventHandler)(delegate
                //{
                //    lbProcess.Items.Add(downloaded_lines.ToString() + "\r\n");
                //}));
            }
            // 刷写完成 TODO：接收到这帧退出或超时了之后退出
            if ((Char_Match("Brush BrushPass", str, (decimal)0.6)) 
                && (handle_flag == 0x04))
            {
                counter = 0;
                cmd = DOWNLOADFILE_PROCESS.NONE;
                
                brushing_flag = false;
                this.Invoke((EventHandler)(delegate
                {
                    btnStart.Enabled = true;
                    _sysTimer.Stop();
                    handle_flag = 0xFF;
                }));
                MessageBox.Show("刷写完成！", "提示");

                return; // 跳出接收函数
            }
            // 行错误 TODO:直接返回，重新刷写
            if (Char_Match("Brush WData Error", str, (decimal)0.6))
            {
                counter = 0;
                this.Invoke((EventHandler)(delegate
                {
                    lbProcess.Items.Add("Brush WData Error");
                }));

                // 刷写失败后， 将自动重新复位所有参数
                Reference();
                MessageBox.Show("刷写失败！需要重新刷写！请点击复位并将设备重新上电后再点击开始刷写！", "提示");
            }
            // 刷写失败
            if (Char_Match("Brush BrushFail", str, (decimal)0.6))
            {
                counter = 0;
                // 刷写失败后， 将自动重新复位所有参数
                Reference();
                MessageBox.Show("刷写失败！需要重新刷写！请点击复位并将设备重新上电后再点击开始刷写！", "提示");
            }

            // 接收软件版本
            if (Char_Match("YNRCT Ver: ", str))
            {
                this.Invoke((EventHandler)(delegate
                {
                    lblCurrentVer.Text = "当前软件版本：" + str.Substring((str.Length - 10), 10);
                }));
            }

            // 对行数进行判断，防止，没有接收到行反馈导致的多发现象发生

        }

        /// <summary>
        /// 字符匹配方法
        /// </summary>
        /// <param name="input"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        private bool Char_Match(string input, string source)
        {
            bool result = false;

            Regex r = new Regex(input);
            Match m = r.Match(source);

            if (m.Success)
                result = true;

            return result;
        }

        /// <summary>
        /// 字符串模糊匹配算法
        /// </summary>
        /// <param name="input">标准字符串</param>
        /// <param name="source">要匹配的字符串</param>
        /// <param name="level">相似度等级</param>
        /// <returns></returns>
        private bool Char_Match(string input, string source, decimal level)
        {
            bool result = false;

            decimal Kq = 2; //浮点数
            decimal Kr = 1;
            decimal Ks = 1;
            char[] ss = input.ToCharArray();
            char[] st = source.ToCharArray();
            
            //获取交集数量
            int q = ss.Intersect(st).Count();
            int s = ss.Length - q;
            int r = st.Length - q;
            decimal iit = Kq * q / (Kq * q + Kr * r + Ks * s);
            //tb3.Text = iit.ToString();
            if (iit >= level)
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// 串口连接方法
        /// </summary>
        private void uart_connect()
        {
            try
            {
                port.Open();
            }
            catch (Exception ex)
            {
                port = new SerialPort();
                //显示异常信息给客户。
                MessageBox.Show(ex.Message);
            }

            if (port.IsOpen)
            {
                UpgradLogic_Init();
            }
            else
            {
                MessageBox.Show("请检查串口设备是否连接正确！");
            }
        }

        /// <summary>
        /// 刷写复位
        /// 清空，刷写过程中间变量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReference_Click(object sender, EventArgs e)
        {
            Reference();
        }

        /// <summary>
        /// 刷新方法
        /// </summary>
        private void Reference()
        {
            // 关闭开启的线程，释放资源
            if (th1.IsAlive)
            {
                th1.Abort();
            }
            _sysTimer.Stop();
            brushing_flag = true;
            line_num = 0;
            handle_flag = 0;
            downloaded_lines = 0;
            lbProcess.Items.Clear();
            pbDownLoad.Value = 0;
            lblPercentage.Text = "0%";
            btnLoadFile.Enabled = true;
            btnStart.Enabled = true;
        }

        #region 定时器
        public void MSTimerStart()
        {
            _sysTimer = new MillisecondTimer();
            _sysTimer.Tick += sysTimer_Tick;
            _sysTimer.Interval = 1000;
            //_sysTimer.Start();
        }

        /* 定时触发发送，间隔1s,在串口接收中确认。 */
        private void sysTimer_Tick(object sender, EventArgs e)
        {
            counter++;
            if (counter > 3)
            {
                counter = 0;
                //_sysTimer.Stop();
                ResponseTimeout_Tick();
                //_sysTimer.Start();
            }
        }
        #endregion
        
        /// <summary>
        /// 如何3s没有收到响应，则进行下一步骤
        /// </summary>
        private void ResponseTimeout_Tick()
        {
            this.Invoke((EventHandler)(delegate
            {
                lbProcess.Items.Add("--------响应超时--------");
            }));
            // 当前状态下，超时，则将自切换，不管回复
            switch (current_cmd)
            {
                case DOWNLOADFILE_PROCESS.GOTOBRUSH:
                    {
                        cmd = DOWNLOADFILE_PROCESS.CHECKSUM;
                        handle_flag = 0x01;
                    }
                    break;
                case DOWNLOADFILE_PROCESS.CHECKSUM:
                    {
                        cmd = DOWNLOADFILE_PROCESS.MAXBIN;
                        handle_flag = 0x02;
                    }
                    break;
                case DOWNLOADFILE_PROCESS.MAXBIN:
                    {
                        cmd = DOWNLOADFILE_PROCESS.WDATE;
                        handle_flag = 0x03;
                    }
                    break;
                case DOWNLOADFILE_PROCESS.WDATE:
                    {
                        // TODO:刷写数据超时，直接重新刷，不再进行下一步
                        //// 判断是否是最后一行，如果是则进行下一步，否则还是当前状态
                        //if (line_num == this.objUpdateManager.downLoadInfo.FileLine.Count)
                        //{
                        //    cmd = DOWNLOADFILE_PROCESS.BRUSHEND;
                        //}
                        //else
                        //{
                        //    cmd = DOWNLOADFILE_PROCESS.WDATE;
                        //}
                        //downloaded_lines += 1;

                        // 中断刷写
                        _sysTimer.Stop();
                        this.Invoke((EventHandler)(delegate
                        {
                            lbProcess.Items.Add("--------响应超时异常--------");
                        }));
                        MessageBox.Show("刷写失败！需要重新刷写！请点击复位并将设备重新上电后再点击开始刷写！", "提示");
                    }
                    break;
                case DOWNLOADFILE_PROCESS.BRUSHEND:
                    {
                        //cmd = DOWNLOADFILE_PROCESS.BRUSHEND;
                        counter = 0;
                        cmd = DOWNLOADFILE_PROCESS.NONE;

                        brushing_flag = false;
                        this.Invoke((EventHandler)(delegate
                        {
                            btnStart.Enabled = true;
                            _sysTimer.Stop();
                            handle_flag = 0xFF;
                        }));
                        MessageBox.Show("刷写完成！", "提示");

                        return; // 跳出接收函数
                    }
                    break;
                default:
                    break;
            }
        }

        private void DownLoadFile_Load(object sender, EventArgs e)
        {

        }

        private void DownLoadFile_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 关闭开启的线程，释放资源
            //if (th1.IsAlive)
            //{
            //    th1.Abort();
            //}
            
            closing = true;
            // 把串口释放掉
            while (listening) Application.DoEvents(); // 此处的侦听，是什么用意？如何实现？
            port.Close();
            closing = false;
            // 关闭当前的运行程序。
            Application.Exit();
            // 重新打开
            System.Diagnostics.Process.Start("TBoxConfigTool.exe");
        }

        /// <summary>
        /// List面板的右键清除触发方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmClaerAll_Click(object sender, EventArgs e)
        {
            lbProcess.Items.Clear();
        }
        
        #region record
        private static string CURRENT_DATE =
            DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");// 2008_09_04_23_59_59
        private static string dataRec_testlog =
           System.IO.Directory.GetCurrentDirectory().Split(new String[] { @"" },
               StringSplitOptions.None)[0] + @"\BrushLog\brushlog" + CURRENT_DATE + ".txt";
        /// <summary>
        /// 记录测试日志的方法
        /// </summary>
        /// <param name="strMsg">要记录的数据</param>
        public void RecordData_testlog(string strMsg)
        {
            string currenttime2 = DateTime.Now.ToString();
            string result1 = dataRec_testlog;//结果保存再txt中
            FileInfo fileInfo = new FileInfo(result1);
            FileStream fs = new FileStream(result1, FileMode.Append); //保证写入TXT中重新写一行，不覆盖上一次写入的内容；
            StreamWriter wr = null;
            wr = new StreamWriter(fs);//F:\result1.txt是保存目录，可以根据个人设置；
            double FileLength = fileInfo.Length; //读取文件大小
            double FileLenKB = 0.0;

            if (FileLength > 1024)//将文件大小转换为KB
            {
                FileLenKB = FileLength / 1024;
                if (FileLenKB > 500000)
                {
                    wr.Close();
                    fs.Close();
                    fs = new FileStream(result1, FileMode.Truncate, FileAccess.ReadWrite);//清空文件内容
                    fs.Close();
                    fs = new FileStream(result1, FileMode.Append, FileAccess.Write);//重新打开文件
                    wr = new StreamWriter(fs);
                }
            }
            if (strMsg != null)
            {
                wr.WriteLine(currenttime2 + " ： " + strMsg);
            }

            wr.Close();
        }
        #endregion
    }
}

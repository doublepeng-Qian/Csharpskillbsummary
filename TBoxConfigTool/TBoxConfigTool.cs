using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;

namespace TBoxConfigTool
{
    #region Enumerations
    /// <summary>
    /// 串口状态信息序号。
    /// </summary>
    public enum UARTPortStatusType : byte
    {
        /// <summary>
        /// UART Port was Opened.
        /// </summary>
        UART_PORT_STATUS_TYPE_OPEN = 0x00,

        /// <summary>
        /// UART Port was Opened.
        /// </summary>
        UART_PORT_STATUS_TYPE_CLOSE = 0x01,

        /// <summary>
        /// UART Port is Sending.
        /// </summary>
        UART_PORT_STATUS_TYPE_SENDING = 0x02,

        /// <summary>
        /// UART Port status is NULL.
        /// </summary>
        UART_PORT_STATUS_TYPE_NULL = 0xFF,
    }

    /// <summary>
    /// 串口信息结构体
    /// </summary>
    public struct PORT_INFM
    {
        public string[] port;
        public UInt16 baudrate;
        public UARTPortStatusType portstatus;
    }

    /// <summary>
    /// 配置状态枚举
    /// </summary>
    public enum ConfigOperationStatus : int
    { 
        normal     = 0,
        busiing    = 1,
        completed  = 2,
    }
    #endregion

    public partial class MainForm : Form
    {
        PortComm objPort = null; // 串口通讯类

        /***********************************************************************
         * 声明
         ***********************************************************************/
        public SerialPort port = null;

        public byte[] rec = new byte[1060];

        private StringBuilder Builder = new StringBuilder();
        //避免在事件处理方法中反复的创建，定义到外面。   
        private long received_count = 0;    //接收计数   
        private long send_count = 0;        //发送计数   
        private bool Listening = false;     //是否没有执行完invoke相关操作  
        private bool UART_Closing = false;       //是否正在关闭串口，执行Application.DoEvents，并阻止再次invoke
        private List<byte> buffer = new List<byte>(4096);
        //默认分配1页内存，并始终限制不允许超过   
        PORT_INFM portinformation;
        string Tx_flg = "Tx--> ";
        string Rx_flg = "Rx<-- ";

        string cat_ecucf = "cat ecucf\r\n";


        MillisecondTimer _sysTimer;
        public UInt32 counter = 0;

        bool configcondition_flag = true;//当前配置条件成熟的标志
        bool timingsend_flag      = false;//定时发送标志
        bool configcompleted      = false;

        string oausServerIP = "127.0.0.1";//"192.168.1.128";
        int oausServerPort  = 4540;

        const UInt16 VehicleCfg_CmdMax = 100;
        public UInt16 VehicleCfg_CmdNum = 0;
        public UInt16 VehicleCfg_CmdLines = 0;
        public string[] VehicleCfgInfo = new string[VehicleCfg_CmdMax];
        public UInt16 line_index = 0;
        public bool[] line_cmd_status = new bool[50];
        public bool DataReceived_Handle_SW = false;
        public bool autoconfigbutton_flag = false;
        ConfigOperationStatus m_Cfg_status = 0;
        bool UART_SW = false;
        public MainForm()
        {
            InitializeComponent();

            /* 线程安全访问窗体函数 */
            //Control.CheckForIllegalCrossThreadCalls = false;

            // 串口配置初始化
            objPort = new PortComm();
            // 初始化串口号下拉菜单
            this.cmbPorts.Items.AddRange(objPort.GetPortNames());
            if (this.cmbPorts.Items.Count > 0)
                this.cmbPorts.SelectedIndex = 0;
            // 初始化波特率
            //this.cmbBaudRate.SelectedIndex = 1;
        }

        public void configparameter_init()
        {
            //CP[0] = Settings1;
        }

        #region 定时器
        public void MSTimerStart()
        {
            _sysTimer = new MillisecondTimer();
            _sysTimer.Tick += sysTimer_Tick;
            _sysTimer.Interval = 1000;
            _sysTimer.Start();
        }

        /* 定时触发发送，间隔1s,在串口接收中确认。 */
        private void sysTimer_Tick(object sender, EventArgs e)
        {
            counter++;
            if (counter > 1)
            {
                timingsend_flag = true;
                counter = 0;
            }
        }
        #endregion

        /***********************************************************************
         * 串口驱动
         ***********************************************************************/
        public void UpgradLogic_Init()
        {
            // 添加事件注册
            port.DataReceived += port_DataReceived;
        }

        public void receivedata()
        {
            //port.Read(rec, 0, 1036);
            //UpgradLogic_Init();
        }

        public void open_receive_thread()
        {
            Thread th1 = new Thread(receivedata);

            if (th1 != null)
                th1.Start();
        }

        public void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (UART_Closing) return;//如果正在关闭，忽略操作，直接返回，尽快的完成串口监听线程的一次循环 

            try
            {
                string str = "";

                str = port.ReadLine();
                this.Invoke((EventHandler)(delegate
                {
                    IncludeTextMessage(Rx_flg + str);//显示到Listbox
                }));

                if (DataReceived_Handle_SW)
                {
                    DataReceived_Handle(str);
                }
            }
            finally
            {
                Listening = false;//我用完了，ui可以关闭串口了。   
            }

        }
        public void DataReceived_Handle(string str)
        {
            if (VehicleCfgInfo[line_index * 2 + 1] == null)
            {
                return;
            }
            /* 用ACK和接收进行比较 */
            if (Char_Match(VehicleCfgInfo[line_index*2 + 1], str))
            {
                line_cmd_status[line_index] = true;
            }

        }


        #region 事件函数
        private void btnOpenSerial_Click(object sender, EventArgs e)
        {
            ////如果串口已经是打开状态，则此按钮关闭串口
            //if (port.IsOpen)
            //{
            //    //打开时点击则关闭串口
            //    //port.Close();
            //}
            //else
            //{
            //    uartconnectbutton.Enabled = false;
            //    //判断是否接了串口驱动
            //    if (Port_comboBox.Text == "")
            //    {
            //        MessageBox.Show("请检查串口设备是否连接正确！");

            //        return;
            //    }
            //    else
            //    {
            //        //关闭时点击，则设置好端口，波特率后打开
            //        port.PortName = Port_comboBox.Text;
            //        port.BaudRate = int.Parse("115200");
            //        port.DataBits = 8;

            //        try
            //        {
            //            port.Open();
            //        }
            //        catch (Exception ex)
            //        {
            //            //捕获到异常信息，创建一个新的comm对象，之前的不能用了。
            //            port = new SerialPort();
            //            //显示异常信息给客户。
            //            MessageBox.Show(ex.Message);
            //            return;
            //        }
            //    }
            //}
            //uartconnectbutton.Text = port.IsOpen ? "关闭" : "打开";
            //lbl_StateWord.Text = port.IsOpen ? "已连接" : "----";
            //uartconnectbutton.BackColor = port.IsOpen ? Color.LightGreen : Color.Empty;
            //Port_comboBox.Enabled = port.IsOpen ? false : true;
            //btn_loadfile.Enabled = true;
            //btn_inquiry.Enabled = true;

            ////注册接收事件，只要串口打开接收线程就启动
            //UpgradLogic_Init();

            if (!objPort.IsPortOpen) // 打开串口
            {
                string result = objPort.OpenPort();
                if (result == "success")
                {
                    this.btnOpenSerial.Text = "关闭串口";
                    this.cmbPorts.Enabled = false;
                    //this.cmbBaudRate.Enabled = false;
                    port = objPort.objSerialPort;
                    //port.PortName = objPort.GetPortNames();
                }
                else
                {
                    MessageBox.Show(result);
                }
            }
            else // 关闭串口
            {
                objPort.ClosePort();
                this.btnOpenSerial.Text = "打开串口";
                this.cmbPorts.Enabled = true;
                //this.cmbBaudRate.Enabled = true;
            }

            lbl_StateWord.Text = objPort.IsPortOpen ? "已连接" : "----";
            btnOpenSerial.BackColor = objPort.IsPortOpen ? Color.LightGreen : Color.Empty;
            btn_loadfile.Enabled = true;
            btn_inquiry.Enabled = true;

            //注册接收事件，只要串口打开接收线程就启动
            UpgradLogic_Init();
        }

        public void SendData(string str)
        {
            try
            {
                this.port.Write(str);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void autoconfig()
        {
            DataReceived_Handle_SW = true;
            MSTimerStart();
            btn_Reset.Enabled = true;

            /*  */
            do
            {
                if (line_cmd_status[line_index] == true)
                    line_index++;

                if (timingsend_flag == true)
                {
                    timingsend_flag = false;
                    // TODO:
                    //port.Write(VehicleCfgInfo[line_index * 2] + "\r");//发送的是偶数行，命令
                    SendData(VehicleCfgInfo[line_index * 2] + "\r");
                    IncludeTextMessage(Tx_flg + VehicleCfgInfo[line_index * 2] + "\r");
                    if (VehicleCfgInfo[line_index * 2] == null)
                    {
                        configcompleted = true;

                        _sysTimer.Stop();
                        MessageBox.Show("自动配置完成！");
                        m_Cfg_status = ConfigOperationStatus.completed;
                        btn_inquiry.Enabled = true;
                    }

                }
                
            } while (configcompleted == false);
        }

        /* 自动配置按钮事件 */
        private void autoconfigbutton_Click(object sender, EventArgs e)
        {
            //判断是否已经加载了配合文件
            if (VehicleCfgInfo[0] == null)
            {
                MessageBox.Show("请先加载车型配置文件！");
                return;
            }
            //不允许再进行车型选择
            btn_inquiry.Enabled = false;
            btn_loadfile.Enabled = false;

            if (m_Cfg_status == ConfigOperationStatus.completed)
            {
                if (MessageBox.Show("确定是要再次配置？",
                                    "提示：",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Exclamation,
                                    MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    /* 复位一下数据 */
                    VehicleCfg_CmdNum = 0;
                    VehicleCfg_CmdLines = 0;
                    line_index = 0;
                    Array.Clear(line_cmd_status, 0, 50);
                    //不清除当前的配置数据。

                    m_Cfg_status = ConfigOperationStatus.busiing;

                    configcompleted = false;

                    Thread th1 = new Thread(autoconfig);
                    if (th1 != null)
                        th1.Start();
                }
                else
                {
                    return;
                }

            }
            else if (m_Cfg_status == ConfigOperationStatus.normal)
            {
                m_Cfg_status = ConfigOperationStatus.busiing;

                configcompleted = false;

                Thread th1 = new Thread(autoconfig);
                if (th1 != null)
                    th1.Start();

            }
            else
            {
                IncludeTextMessage("请稍等，当前配置还未完成！");
            }
        }

        private void 操作说明CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String HelpDocument_path = System.Windows.Forms.Application.StartupPath
                + "\\TBoxConfigTool操作说明.pdf";

            System.Diagnostics.Process.Start(HelpDocument_path);
        }

        /* 软件升级功能 */
        private void 软件升级SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //程序自动更新
            //AutoUpgrade();
        }

        /* 作者信息 */
        private void 关于AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAuthor FA = new FormAuthor();
            FA.ShowDialog();
        }
        #endregion



        #region 功能函数
        // 将char[]数组转换为string类型并返回
        private static string CharArrayTosting(char[] cha, int len)
        {
            string str = "";

            for (int i = 0; i < len; i++)
            {
                str += string.Format("{0}", cha[i]);
            }

            return str;
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

        #endregion

        #region 处理函数
        public void IncludeTextMessage(string strMsg)
        {
            string currenttime = DateTime.Now.ToString();//获取系统当前时间

            InformationListBox.Items.Add(currenttime + " : " + strMsg);
            try
            {
                InformationListBox.SelectedIndex = InformationListBox.Items.Count - 1; /* 显示的总行数 */
                // 记录log日志
                RecordData_testlog(currenttime + ":" + strMsg);
            }
            catch
            {
            }

            // RecordData_testlog(currenttime + ":" + strMsg);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            portinformation.portstatus = UARTPortStatusType.UART_PORT_STATUS_TYPE_NULL;

            #region 初始化下拉串口名称列表框
            //获取设备与PC连接的端口号
            //string[] uart_port_num = SerialPort.GetPortNames();
            //Array.Sort(uart_port_num);

            //将其显示到comboPorName控件中去
            //Port_comboBox.Items.AddRange(uart_port_num);
            //Port_comboBox.SelectedIndex = Port_comboBox.Items.Count > 0 ? 0 : -1;

            //波特率默认为9600bps，可以通过下拉选项进行调节
            //BaudRate_comboBox.SelectedIndex = BaudRate_comboBox.Items.IndexOf("115200");

            //uartconnectbutton.Text = "开启串口";

            //port.NewLine = "\r\n";
            //port.RtsEnable = true;
            #endregion

            //lbl_StateWord.Text = "未连接";
            
        }

        public void ReadVehicleConfigFile(string selectfilepath)
        {
            string path = selectfilepath;
            // 读取文件的源路径及其读取流
            StreamReader srReadFile = new StreamReader(path);

            // 读取流直至文件末尾结束
            while (!srReadFile.EndOfStream)
            {
                string strReadLine = srReadFile.ReadLine(); //读取每行数据
                //Console.WriteLine(strReadLine); //屏幕打印每行数据
                AnalyzeVehicleConfigFile(strReadLine);
            }

            // 关闭读取流文件
            srReadFile.Close();
            Console.Read();
        }

        public void AnalyzeVehicleConfigFile(string str)
        {           
            VehicleCfgInfo[VehicleCfg_CmdLines++] = str;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //单向触发
            btn_loadfile.Enabled = false;

            if (m_Cfg_status == ConfigOperationStatus.completed)
            {
                //如果要重新加载配置文件，则清除数据项；否则忽略此次操作
                if (MessageBox.Show("确定是要重新加载车型配置文件？",
                                    "提示：",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Exclamation,
                                    MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    VehicleCfg_CmdNum = 0;
                    VehicleCfg_CmdLines = 0;
                    line_index = 0;
                    Array.Clear(line_cmd_status, 0, 50);
                    Array.Clear(VehicleCfgInfo, 0, VehicleCfg_CmdMax);
                    m_Cfg_status = ConfigOperationStatus.normal;
                }
                else
                {
                    return;
                }
            }

            if (m_Cfg_status == ConfigOperationStatus.normal)
            { 
                OpenFileDialog fileDialog = new OpenFileDialog();

                /* 不允许同时选定多个文件 */
                fileDialog.Multiselect = false;

                fileDialog.Filter = "所有文件(*.*)|*.ivc*";

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    ReadVehicleConfigFile(fileDialog.FileName);
                }

                VehicleCfg_CmdNum = (UInt16)(VehicleCfg_CmdLines / 2);

                textBox1.Text = "车型配置信息路径：\r\n";
                textBox1.AppendText(fileDialog.FileName + "\r\n");
                textBox1.AppendText("--------------------\r\n");
                textBox1.AppendText("车型配置信息:\r\n");
                for (int i = 0; i < VehicleCfg_CmdNum; i++)
                {
                    textBox1.AppendText(VehicleCfgInfo[i * 2] + "\r\n");
                }
                autoconfigbutton.Enabled = true;
            }
        }

        private void btn_inquiry_Click(object sender, EventArgs e)
        {
            if (objPort.IsPortOpen == false)
                return;

            string str = cat_ecucf;
            //发送查询报文
            //port.Write(str);
            SendData(str);

            //调用显示，查询窗口
            IncludeTextMessage(Tx_flg + str);
        }

        private void ClearAll_Click(object sender, EventArgs e)
        {
            InformationListBox.Items.Clear();
        }

        private void 配置toolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Form2 keyinput = new Form2();
            //keyinput.ShowDialog();
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            configcompleted = true;
            _sysTimer.Stop(); //TODO:如果没有开启，则出错。
            InformationListBox.Items.Clear();
            textBox1.Clear();
            btn_inquiry.Enabled = true;
            btn_loadfile.Enabled = true;

            VehicleCfg_CmdNum = 0;
            VehicleCfg_CmdLines = 0;
            line_index = 0;
            Array.Clear(line_cmd_status, 0, 50);
            Array.Clear(VehicleCfgInfo, 0, VehicleCfg_CmdMax);
            m_Cfg_status = ConfigOperationStatus.normal;

            btn_Reset.Enabled = false;
        }
        #endregion

        #region
        //记录测试日志
        private static string CURRENT_DATE = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");// 2008_09_04_23_59_59
        private static string dataRec_testlog =
            System.IO.Directory.GetCurrentDirectory().Split(new String[] { @"" },
                StringSplitOptions.None)[0] + @"\log\configlog" + CURRENT_DATE + ".txt";
        public void RecordData_testlog(string strMsg)
        {
            //string currenttime2 = DateTime.Now.ToString();
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
                if (FileLenKB > 5000)
                {
                    wr.Close();
                    fs.Close();
                    fs = new FileStream(result1, FileMode.Truncate, FileAccess.ReadWrite);//清空文件内容
                    fs.Close();
                    fs = new FileStream(result1, FileMode.Append, FileAccess.Write);//重新打开文件
                    wr = new StreamWriter(fs);
                }
            }
            if (strMsg != "开始记录:")
            {
                wr.WriteLine(strMsg);
            }
            else
            {
                wr.WriteLine("\n" + "\n" + "\n" + "开始记录:");
            }
            wr.Close();
        }
        #endregion

        private void 烧录CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // 释放串口
                //port.Close
                objPort.ClosePort();
                //关闭主程序
                //Application.ExitThread();
                Application.Exit();
                // 启动烧录程序
                //System.Diagnostics.Process.Start("DownLoad.exe");

                // 启动一个应用，并传入参数
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "DownLoad.exe";
                startInfo.Arguments = port.PortName;
                startInfo.WindowStyle = ProcessWindowStyle.Normal;
                Process.Start(startInfo);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Port_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            objPort.objPortParam.Port = this.cmbPorts.Text;
        }
    }
}

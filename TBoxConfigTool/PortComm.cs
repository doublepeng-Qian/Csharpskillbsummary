using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO.Ports;

namespace TBoxConfigTool
{
    class PortComm
    {
        #region 字段、属性
        // 串口通讯
        public SerialPort objSerialPort = null;

        // 串口参数实体类属性
        public PortParameter objPortParam { get; set; } = new PortParameter();

        // 是否没有执行完Invoke相关操作，如果不加，在关闭串口时程序会卡死
        private bool listening = false;

        // 是否正在关闭串口，执行Application.DoEvents，并阻止再次Invoke
        private bool closing = false;

        // 缓冲区数据
        private List<byte> bufferList = new List<byte>();

        public TempCommParam TempData { get; set; } = new TempCommParam();

        // 串口打开标志
        public bool IsPortOpen { get; set; } = false;
        #endregion

        #region 构造方法
        public PortComm()
        {
            objSerialPort = new SerialPort()
            {
                PortName = objPortParam.Port,
                BaudRate = objPortParam.Baud,
                DataBits = objPortParam.DataBit,
                Parity = objPortParam.ObjParity,
                StopBits = objPortParam.ObjStopBits
            };

            objSerialPort.DataReceived += ObjSerialPort_DataReceived;
        }
        #endregion

        #region 串口数据发送
        public void SendData(byte[] byteArry)
        {
            try
            {
                this.objSerialPort.Write(byteArry, 0, byteArry.Length);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 串口数据接收
        private void ObjSerialPort_DataReceived(object sender, SerialDataReceivedEventArgs eventArgs)
        {
            //if (closing) return;
            //try
            //{
            //    #region 数据解析
            //    int count = objSerialPort.BytesToRead;
            //    byte[] buffer = new byte[count];
            //    objSerialPort.Read(buffer, 0, count);
            //    bufferList.AddRange(buffer);

            //    while (bufferList.Count > 0)
            //    {
            //        if (bufferList[0] == 0x55)
            //        {
            //            if (bufferList.Count > 1)
            //            {
            //                // 获得指令总长度
            //                int commandLength = bufferList[1] + 4;

            //                // 接收的数据长度没有达到完整指令时退出，继续等待接收
            //                if (bufferList.Count < commandLength) break;

            //                // 判断帧尾和校验位是否符合要求
            //                if (bufferList[commandLength - 1] == 0xAA && CheckBit(bufferList[bufferList.Count - 2]))
            //                {
            //                    switch (bufferList[2])
            //                    {
            //                        #region 握手返回指令
            //                        case 0x90:
            //                            string handStr = string.Empty;
            //                            for (int i = 0; i < 5; i++)
            //                            {
            //                                handStr += bufferList[i].ToString("X2") + " ";
            //                            }
            //                            handStr = handStr.TrimEnd();
            //                            TempData.CommParam = 0x90;
            //                            TempData.ReturnResult = true;
            //                            TempData.CommStr = handStr;
            //                            break;
            //                        #endregion

            //                        case 0xA3:
            //                            string chipID = string.Empty;
            //                            for (int i = 3; i < 15; i++)
            //                            {
            //                                chipID += bufferList[i].ToString("X2");
            //                            }
            //                            TempData.CommParam = 0xA3;
            //                            TempData.ReturnResult = true;
            //                            TempData.CommStr = chipID;
            //                            break;

            //                    }
            //                }
            //            }
            //            else
            //            {
            //                break;
            //            }
            //        }
            //        bufferList.RemoveAt(0);
            //    }
            //    #endregion
            //}
            //catch (Exception ex)
            //{

            //}
        }
        #endregion

        #region 设置串口参数
        private void SetPortParam()
        {
            objSerialPort.PortName = objPortParam.Port;
            objSerialPort.BaudRate = objPortParam.Baud;
            objSerialPort.DataBits = objPortParam.DataBit;
            objSerialPort.Parity = objPortParam.ObjParity;
            objSerialPort.StopBits = objPortParam.ObjStopBits;
        }
        #endregion

        #region 获取Com口的列表
        /// <summary>
        /// 获取Com口的列表
        /// </summary>
        /// <returns></returns>
        public string[] GetPortNames()
        {
            List<int> portNames = new List<int>();
            foreach (string pname in SerialPort.GetPortNames())
            {
                portNames.Add(Convert.ToInt32(pname.Substring(3)));
            }
            portNames.Sort();
            string[] myStr = new string[portNames.Count];
            int i = 0;
            foreach (int num in portNames)
            {
                myStr[i++] = "COM" + num.ToString();
            }
            return myStr;
        }
        #endregion

        #region 打开串口
        /// <summary>
        /// 打开串口
        /// </summary>
        /// <returns></returns>
        public string OpenPort()
        {
            try
            {
                if (!objSerialPort.IsOpen)
                {
                    SetPortParam();
                    objSerialPort.Open();
                    this.IsPortOpen = true;
                }
                return "success";
            }
            catch (Exception ex)
            {
                return "打开串口出现异常：" + ex.Message;
            }
        }
        #endregion

        #region 关闭串口
        /// <summary>
        /// 关闭串口
        /// </summary>
        public void ClosePort()
        {
            closing = true;
            while (listening) Application.DoEvents();
            objSerialPort.Close();
            this.IsPortOpen = false;
            closing = false;
        }
        #endregion

        /// <summary>
        /// 校验位检测
        /// </summary>
        /// <param name="checkByte"></param>
        /// <returns></returns>
        private bool CheckBit(byte checkByte)
        {
            //byte[] tempArray = new byte[bufferList.Count - 4];
            //bufferList.CopyTo(2, tempArray, 0, bufferList.Count - 4);
            //byte currentByte = new CommandBase().CheskSum(tempArray);
            //if (currentByte == checkByte)
            //    return true;
            //else
                 return false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class TempCommParam
    {
        /// <summary>
        /// 指令参数
        /// </summary>
        public int CommParam { get; set; } = -1;
        /// <summary>
        /// 返回结果
        /// </summary>
        public bool ReturnResult { get; set; } = false;
        /// <summary>
        /// 指令字符串
        /// </summary>
        public string CommStr { get; set; }
    }
}

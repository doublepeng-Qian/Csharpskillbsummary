using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBoxConfigTool
{
    /// <summary>
    /// 串口参数实体类
    /// </summary>
    public class PortParameter
    {
        /// <summary>
        /// 端口号
        /// </summary>
        public string Port { get; set; } = "COM1";

        /// <summary>
        /// 波特率
        /// </summary>
        public int Baud { get; set; } = 115200;

        /// <summary>
        /// 数据位
        /// </summary>
        public int DataBit { get; set; } = 8;

        /// <summary>
        /// 校验位
        /// </summary>
        public Parity ObjParity { get; set; } = Parity.None;

        /// <summary>
        /// 停止位
        /// </summary>
        public StopBits ObjStopBits { get; set; } = StopBits.One;
    }
}

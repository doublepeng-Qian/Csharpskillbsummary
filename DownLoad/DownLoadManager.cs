using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Windows.Forms;

namespace DownLoad
{
    /// <summary>
    /// 烧录管理器核心业务类
    /// </summary>
    public class DownLoadManager
    {
        private byte[] brush_wdata_ = {0x62, 0x72, 0x75, 0x73, 0x68, 0x20,
                                                           0x77, 0x64, 0x61, 0x74, 0x65, 0x20};
        private byte[] brush_checksum_ = {0x62, 0x72, 0x75, 0x73, 0x68, 0x20,
                                                           0x63, 0x68, 0x65, 0x63, 0x6B, 0x73, 0x75, 0x6D, 0x20};

        private byte[] brush_ = {0x62, 0x72, 0x75, 0x73, 0x68, 0x20,
                                                           0x6D, 0x61, 0x78, 0x62, 0x69, 0x6E, 0x20};

        private byte[] brush_wdata_OK_ = {0x0D, 0x0A, 0x4F, 0x4B, 0x0D, 0x0A};
        public DownLoadManager()
        {
            // 初始化对象属性
            this.downLoadInfo = new DownLoadInfo();
        }

        /// <summary>
        /// 
        /// </summary>
        public DownLoadInfo downLoadInfo { get; set; }

        ///<summary>
        /// 声明用于显示更新进度的委托【已下载的百分比】
        ///</summary>
        public delegate void ShowUpdateProgress(int finishedPercent);

        ///<summary>
        /// 创建委托对象【在更新窗体中队友具体方法预支关联】
        ///</summary>
        public ShowUpdateProgress ShowProgressDelegate;

        ///<summary>
        /// 声明用于显示接收报文的委托【已下载的百分比】
        ///</summary>
        public delegate void ShowRTFrames(string str);

        ///<summary>
        /// 创建委托对象【更新信息的函数】
        ///</summary>
        public ShowRTFrames ShowRTFramesDelegate;

        /// <summary>
        /// 加载文件
        /// </summary>
        public bool LoadFileProcess()
        {
            try
            {
                int i = 0;

                this.downLoadInfo.FileLine = new List<byte[]>();

                FileStream fs;
                fs = new FileStream(this.downLoadInfo.UpdateFilePth,
                                                  FileMode.OpenOrCreate,
                                                  FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                while (br.BaseStream.Position < br.BaseStream.Length)
                {
                    this.downLoadInfo.FileContent[i] = br.ReadByte();
                    this.downLoadInfo.FileCheckSum +=
                        this.downLoadInfo.FileContent[i];
                    i++;
                }
                this.downLoadInfo.FileSize = i;

                // 关闭读取流文件
                fs.Close();
                br.Close();

                int a = i / 1024;
                int b = i % 1024;
                byte[] tmp = new byte[1040];
                int linechecksum = 0;
                // 如果是整数行
                if (b == 0)
                {
                    for (int j = 0; j < a; j++)
                    {
                        // 将固定字节先写入
                        for (int k = 0; k < 12; k++)
                        {
                            tmp[k] = brush_wdata_[k];
                        }
                        // 将数据写入，并计算校验值
                        for (int k = 0; k < 1024; k++)
                        {
                            tmp[k + 12] = this.downLoadInfo.FileContent[k + j * 1024];
                            linechecksum += tmp[k + 12];
                        }
                        // 将校验值拆分
                        tmp[1036] = (byte)(linechecksum >> 24);
                        tmp[1037] = (byte)(linechecksum >> 16);
                        tmp[1038] = (byte)(linechecksum >>  8);
                        tmp[1039] = (byte)(linechecksum);
                        byte[] t = new byte[1046];
                        Array.Copy(tmp, t, tmp.Length);
                        for (int h = 0; h < 6; h++)
                        {
                            t[1040 + h] = brush_wdata_OK_[h];
                        }
                        this.downLoadInfo.FileLine.Add(t);
                    }
                }
                else // 如果是非整数行
                {
                    for (int j = 0; j < a; j++)
                    {
                        // 写入固定字节【12】
                        Array.Copy(brush_wdata_, tmp, brush_wdata_.Length);
                        // 写入数据【1024】
                        for (int k = 0; k < 1024; k++)
                        {
                            tmp[k + 12] = this.downLoadInfo.FileContent[k + j * 1024];
                            linechecksum += tmp[k + 12];
                        }
                        // 计算数据的校验值
                        tmp[1036] = (byte)(linechecksum >> 24);
                        tmp[1037] = (byte)(linechecksum >> 16);
                        tmp[1038] = (byte)(linechecksum >> 8);
                        tmp[1039] = (byte)(linechecksum);
                        linechecksum = 0;
                        byte[] c = new byte[1046];
                        Array.Copy(tmp, c, tmp.Length);
                        for (int h = 0; h < 6; h++)
                        {
                            c[1040 + h] = brush_wdata_OK_[h];
                        }
                        this.downLoadInfo.FileLine.Add(c); //整数行的添加
                        Array.Clear(tmp, 0, 1040);
                    }
                    // 整数行+非整数行
                    a += 1;
                    // 处理非满字节行数据[12]
                    Array.Copy(brush_wdata_, tmp, brush_wdata_.Length);
                    for (int k = 0; k < b; k++)// b的长度至少为1
                    {
                        tmp[k + 12] = this.downLoadInfo.FileContent[k + (a - 1)* 1024]; // 
                        linechecksum += tmp[k + 12];
                    }
                    tmp[b + 12] = (byte)(linechecksum >> 24);
                    tmp[b + 13] = (byte)(linechecksum >> 16);
                    tmp[b + 14] = (byte)(linechecksum >> 8);
                    tmp[b + 15] = (byte)(linechecksum);
                    linechecksum = 0;
                    byte[] d = new byte[b+22]; // 固定数据+数据+校验
                    Array.Copy(tmp, d, b+16);
                    for (int h = 0; h < 6; h++)
                    {
                        d[b + 16 + h] = brush_wdata_OK_[h];
                    }
                    this.downLoadInfo.FileLine.Add(d); 
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
       
    }
}

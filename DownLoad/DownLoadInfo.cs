using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownLoad
{
    /// <summary>
    /// 创建一个下载信息类
    /// </summary>
    public class DownLoadInfo
    {
        ///<summary>
        /// 软件版本
        ///</summary>
        public string Version { get; set; }

        ///<summary>
        /// 更新时间
        ///</summary>
        public string UpdateTime { get; set; }

        ///<summary>
        /// 更新文件所在的路径
        ///</summary>
        public string UpdateFilePth { get; set; }

        ///<summary>
        /// 更新文件内容
        ///</summary>
        public byte[] FileContent { get; set; }

        ///<summary>
        /// 更新文件大小
        ///</summary>
        public int FileSize { get; set; }

        ///<summary>
        /// 更新校验值
        ///</summary>
        public int FileCheckSum { get; set; }

        /// <summary>
        /// 文件的每行
        /// </summary>
        public List<byte[]> FileLine { get; set; }

        /// <summary>
        /// 行数
        /// </summary>
        public int FileLineNum { get; set; }
    }
}

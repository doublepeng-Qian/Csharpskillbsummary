using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp12_LSP
{
    /// <summary>
    /// 交通工具类
    /// </summary>
    class Vehicle
    {
        //使用virtual关键字，声明一个虚方法Transport()
        public virtual void Transport()
        {
            Console.WriteLine("交通工具运送乘客出发了……");
        }
    }
}

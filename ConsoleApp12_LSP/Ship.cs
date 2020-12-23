using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp12_LSP
{
    /// <summary>
    /// 轮船类
    /// </summary>
    class Ship : Vehicle    //继承父类Vehicle
    {
        public override void Transport()      //使用override关键字，重写父类的虚方法
        {
            Console.WriteLine("汽车带你路上走");
        }
    }
}

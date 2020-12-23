using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp12_LSP
{
    /// <summary>
    /// 旅行者类
    /// </summary>
    class Traveler
    {
        //旅行
        public void Travel(Vehicle vehicle)     //  该方法需要传入参数，数据类型是Vehicle类型
        {
            Console.WriteLine("世界这么大，我要去看看");
            vehicle.Transport();    //调用对象的Transport()方法
        }
    }
}

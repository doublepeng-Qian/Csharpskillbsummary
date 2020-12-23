using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp12_LSP
{
    class Program
    {
        static void Main(string[] args)
        {
            Traveler traveler = new Traveler();  //new一个旅行者对象
            //乘坐不同的交通工具
            traveler.Travel(new Car());          //调用旅行者对象的Trave方法，并将子类Car对象，作为参数传入
            traveler.Travel(new Plane());
            traveler.Travel(new Ship());
            Console.ReadLine();
        }
    }
}

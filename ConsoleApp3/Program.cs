using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Program
    {
        //声明一个测试类
        class MyObject
        {
            public string Flag { get; set; }   //声明一个属性
        }
        static void Main(string[] args)
        {
            //实例化一个对象
            MyObject obj1 = new MyObject();
            obj1.Flag = "你好";        //给对象的Flag属性赋值

            MyObject obj2 = obj1;       //声明一个对象，并将obj1赋值给它
            Console.WriteLine("修改obj2前，obj1={0},obj2={1}", obj1.Flag, obj2.Flag);

            obj2.Flag = "中国";           //重新赋值
            Console.WriteLine("修改obj2后，obj1={0},obj2={1}", obj1.Flag, obj2.Flag);
            Console.ReadKey();
        }
    }
}

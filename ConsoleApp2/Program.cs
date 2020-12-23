using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            //实例化一个对象，语法：类名 对象名 = new 类名();            
            Clerk zsClerk = new Clerk();
            //给对象相关属性赋值
            zsClerk.Name = "张三";
            //zsClerk.Gender = "女";    //Gender属性由于是只读属性，因此这里如果给它赋值会编译出错
            zsClerk.Age = 0;

            //调用方法，语法：对象名.方法名();
            zsClerk.SayHello();
            Console.ReadLine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //将类实例化，就是将类指定某个对象
            Student zsStudent = new Student();
            zsStudent._name = "张三";
            zsStudent._gender = "男";
            zsStudent._age = 19;
            string mystr = "王五";//声明一个变量，并赋值

            //调用非静态方法
            zsStudent.Write();
            
            //实例化第二个对象
            Student lsStudent = new Student();
            lsStudent._name = "李四";
            lsStudent._gender = "女";
            lsStudent._age = 18;
            mystr = "赵六";//给变量mystr，重新赋值

            //调用非静态方法
            zsStudent.Write();
            Console.WriteLine(mystr);
            Console.WriteLine(mystr);
            Console.ReadLine();
        }
    }
}

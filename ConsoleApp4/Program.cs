using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    //声明一个结构
    public struct Student
    {
        public string name { get; set; }
        public int age { get; set; }
        public string gender { get; set; }

        //声明一个方法
        public void SayHello()
        {
            Console.WriteLine("大家好，我叫{0}，性别：{1}，年龄：{2}。", name, gender, age);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Student zsStudent = new Student();    //创建一个对象
            zsStudent.name = "张三";
            zsStudent.age = 18;
            zsStudent.gender = "男";
            zsStudent.SayHello();    //调用方法
            Console.ReadKey();
        }
    }
}

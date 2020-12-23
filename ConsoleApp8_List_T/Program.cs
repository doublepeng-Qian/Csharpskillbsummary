using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp8_List_T
{
    class Program
    {
        //声明一个学生类Student
        class Student
        {
            public string Name { get; set; }
            public float Score { get; set; }

            //声明一个方法，输出学生的分数
            public void SayHello()
            {
                Console.WriteLine("大家好，我是学生：{0}，我这次考了{1}分。", Name, Score);
            }
        }

        static void Main(string[] args)
        {
            //声明一个List集合
            List<Student> list = new List<Student>();       //  指定list集合中的数据类型是Student类型
            //声明两个Student对象
            Student stu1 = new Student() { Name = "张三", Score = 80 };
            Student stu2 = new Student() { Name = "李四", Score = 90 };
            //将声明的两个Student对象，stu1和stu2，添加到list集合里
            list.Add(stu1);
            list.Add(stu2);
            //访问数据
            list[0].SayHello();         //使用索引访问
            //遍历集合中的元素
            Console.WriteLine("\n遍历list集合中的元素");
            for (int i = 0; i < list.Count; i++)
            {
                list[i].SayHello();
            }
            Console.ReadLine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ConsoleApp6_ArrayList2
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
        //声明一个教师类Teacher
        class Teacher
        {
            public string Name { get; set; }
            public string Subject { get; set; }
            //声明一个方法，输出教师的自我介绍
            public void SayHello()
            {
                Console.WriteLine("大家好，我是{0}老师，我教的科目是：{1}。", Name, Subject);
            }
        }

        static void Main(string[] args)
        {
            ////集合初始化器初始化了arrayList，然后用对象初始化器初始化了1个Teacher对象实例和2个Student对象实例
            //ArrayList arrayList = new ArrayList()
            //{
            //    new Teacher(){ Name="王迪" ,Subject="C#" },
            //    new Student(){ Name="张三" , Score=80.5f },
            //    new Student { Name="李四" , Score=90 }
            //};
            ////访问集合中的元素，并存储在对象中
            //Teacher teacher = (Teacher)arrayList[0];//将集合arrayList中索引0的元素，强制转换为Teacher类型
            //teacher.SayHello();         //调用对象的方法
            //Console.ReadLine();


            /******************************************/
            //使用集合初始化器，添加元素到动态数组中
            ArrayList arrayList = new ArrayList { 11, 0, "张三", 20.3, "张三", 'a', 5, 6, 7 };
            Console.WriteLine("集合中的元素有：");
            foreach (object a in arrayList)
            {
                Console.Write(a + "\t");
            }
            Console.WriteLine();

            //使用Remove方法删除
            arrayList.Remove("张三");
            Console.WriteLine("使用Remove方法删除后，集合中的元素有：");
            foreach (object a in arrayList)
            {
                Console.Write(a + "\t");
            }
            Console.WriteLine();

            //使用RemoveAt方法删除
            arrayList.RemoveAt(0);
            Console.WriteLine("使用RemoveAt方法删除后，集合中的元素有：");
            foreach (object a in arrayList)
            {
                Console.Write(a + "\t");
            }
            Console.WriteLine();

            //使用RemoveRange方法删除
            arrayList.RemoveRange(2, 3);
            Console.WriteLine("使用RemoveRange方法删除后，集合中的元素有：");
            foreach (object a in arrayList)
            {
                Console.Write(a + "\t");
            }
            Console.WriteLine();

            //使用Clear方法删除
            arrayList.Clear();
            Console.WriteLine("使用Clear方法删除后，集合中的元素有{0}个", arrayList.Count);

            Console.ReadLine();

        }
    }
}

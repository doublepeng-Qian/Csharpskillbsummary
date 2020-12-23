using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ConsoleApp6_ArrayList3
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
            //使用集合初始化器，new三个对象初始化以后，添加到动态数组中
            ArrayList arrayList = new ArrayList()
            {
                new Teacher(){ Name="王迪" ,Subject="C#" },
                new Student(){ Name="张三" , Score=80.5f },
                new Student { Name="李四" , Score=90 }
            };
            //遍历输出集合中的元素，第一种方法：foreach方法
            Console.WriteLine("使用foreach方法，遍历输出集合中的元素");
            foreach (object item in arrayList)
            {
                if (item is Student)                //  使用is关键字判断元素类型，这里判断元素是否Student类型，返回 true 或 false
                {
                    Student stu = (Student)item;    //  将元素强制转换为Student类型后，存储在对象stu里
                    stu.SayHello();                 //调用对象的方法
                }
                if (item is Teacher)
                {
                    ((Teacher)item).SayHello();     //将两个步骤合并在一起
                }
            }
            Console.WriteLine();
            Console.WriteLine("使用for方法，遍历输出集合中的元素");
            //遍历输出集合中的元素，第二种方法：for方法
            for (int i = 0; i < arrayList.Count; i++)       //使用Count属性，获取集合的索引长度（区别数组长度用的是Length属性）
            {
                if (arrayList[i] is Student)
                {
                    ((Student)arrayList[i]).SayHello();
                }
                if (arrayList[i] is Teacher)
                {
                    ((Teacher)arrayList[i]).SayHello();
                }
            }
            Console.ReadLine();
        }
    }
}

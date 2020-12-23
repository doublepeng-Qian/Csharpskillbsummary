using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ConsoleApp6_ArrayList1
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
            /******************************************/
            ////new第一个学生对象，并针对属性赋值
            //Student stu1 = new Student();
            //stu1.Name = "张三";
            //stu1.Score = 80.5f;
            ////new第二个学生对象，并针对属性赋值
            //Student stu2 = new Student();
            //stu2.Name = "李四";
            //stu2.Score = 90;
            ////new一个教师对象，并针对属性赋值
            //Teacher teacher = new Teacher();
            //teacher.Name = "王迪";
            //teacher.Subject = "c#";

            ////声明一个动态数组，不指定容量
            //ArrayList arrayList = new ArrayList();
            ////向动态数组arrayList中添加对象
            //arrayList.Add(stu1);
            //arrayList.Add(stu2);
            //arrayList.Insert(0, teacher);       //将对象teacher添加到集合中，索引位置是0

            ////输出动态数组中的元素个数，使用Count属性获取
            //Console.WriteLine("动态数组arrayList中，有{0}个元素", arrayList.Count);
            //Console.ReadLine();

            /******************************************/
            ////new一个学生对象，并针对属性赋值
            //Student stu1 = new Student();
            //stu1.Name = "张三";
            //stu1.Score = 80.5f;

            ////使用对象初始化器，new一个学生对象，并初始化
            //Student stu2 = new Student() { Name = "张三", Score = 80 };//对象初始化器将创建对象和赋值合为一行，其中赋值就是在后面大括号里做的。这里的赋值可以给所有属性赋值，也可以给部分属性赋值。
            //Student stu3 = new Student { Name = "李四", Score = 90.5f };
            //Student stu4 = new Student { };
            ////声明一个动态数组，不指定容量
            //ArrayList arrayList = new ArrayList();
            //arrayList.Add(stu1);
            //arrayList.Add(stu2);
            //arrayList.Add(stu3);
            //arrayList.Add(stu4);
            //Console.WriteLine("动态数组arrayList中，有{0}个元素", arrayList.Count);
            //Console.ReadLine();

            /******************************************/
            ////声明一个动态数组ArrayList，并添加元素
            //ArrayList arrayList = new ArrayList();
            //arrayList.Add(10);
            //arrayList.Add("hello");
            //arrayList.Add(20.3);
            ////使用集合初始化器，简化上面四行代码
            //ArrayList arrayList2 = new ArrayList { 10, "hello", 20.3 };//集合初始化器的原理也很简单，就是它默默的为我们调用了ArrayList的Add方法依次为我们添加了这3个元素。
            //ArrayList arrayList3 = new ArrayList() { 10, "hello", 20.3, 'a' };

            //Console.WriteLine("动态数组arrayList中，有{0}个元素", arrayList.Count);
            //Console.ReadLine();

            /******************************************/
            ////使用集合初始化器，new三个对象初始化以后，添加到动态数组中
            //ArrayList arrayList = new ArrayList()
            //{
            //    new Teacher(){ Name="王迪" , Subject="C#" },
            //    new Student(){ Name="张三" , Score=80.5f },
            //    new Student { Name="李四" , Score=90 }
            //};

            //Console.WriteLine("动态数组arrayList中，有{0}个元素", arrayList.Count);
            //Console.ReadLine();

            /******************************************/
            ArrayList arrayList = new ArrayList { 102, "hello", 'a', 2.5 };
            //使用索引访问arrayList集合中的元素，并输出
            Console.WriteLine(arrayList[0]);
            //输出指定索引的元素
            Console.WriteLine("arrayList集合中，索引1的元素是：{0}", arrayList[1]);
            Console.ReadLine();
        }
    }
}

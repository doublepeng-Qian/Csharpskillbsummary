using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp9_Dictionary
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
            //声明一个Dictionary集合
            Dictionary<string, Student> dictionary = new Dictionary<string, Student>();     //键是string类型，值是Student类型
            //声明两个Student对象
            Student stu1 = new Student() { Name = "张三", Score = 80 };
            Student stu2 = new Student() { Name = "李四", Score = 90 };
            //将声明的两个Student对象，stu1和stu2，添加到dictionary集合里
            dictionary.Add("张三", stu1);
            dictionary.Add("李四", stu2);
            //访问数据
            Console.WriteLine(dictionary["张三"].Name);       //使用键访问元素，并取出Name属性
            //遍历集合中的元素
            Console.WriteLine("\n通过values，遍历dictionary集合中的元素");
            foreach (Student stu in dictionary.Values)
            {
                stu.SayHello();
            }
            Console.WriteLine("\n通过keys，遍历dictionary集合中的元素");
            foreach (string str in dictionary.Keys)         //因为前面声明dictionary元素的时候，key是string类型
            {
                Console.WriteLine(str);
            }
            Console.WriteLine("\n通过KeyValuePair<k,v>，遍历dictionary集合中的元素");
            foreach (KeyValuePair<string, Student> item in dictionary)
            {
                Console.WriteLine(item.Key);
                (item.Value).SayHello();
            }
            Console.ReadLine();
        }
    }
}

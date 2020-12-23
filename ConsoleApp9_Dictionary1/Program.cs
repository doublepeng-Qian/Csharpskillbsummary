using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp9_Dictionary1
{
    //声明一个学生类
    class Student
    {
        public string Name { get; set; }
        public float Score { get; set; }
        public void SayHello()
        {
            Console.WriteLine("大家好，我是学生{0},这次考了{1}分", Name, Score);
        }
    }
    //自定义分数比较器
    class ScoreComparer : IComparer<Student>
    {
        public int Compare(Student x, Student y)
        {
            return x.Score.CompareTo(y.Score);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //使用List<T>
            List<Student> list = new List<Student>() {
                new Student() { Name = "jack", Score = 80 },
                new Student() { Name = "may", Score = 89 },
                new Student() { Name = "jhon", Score = 66 }
            };
            //按分数排序
            list.Sort(new ScoreComparer());
            //遍历排序后的集合
            foreach (Student stu in list)
            {
                stu.SayHello();
            }
            Console.ReadLine();
        }
    }

}

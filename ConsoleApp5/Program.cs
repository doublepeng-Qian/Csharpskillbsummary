using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    //学生结构
    struct Student
    {
        public string Name { get; set; }
        public float Score { get; set; }
        public void ShowScore()         //声明一个方法，输出分数
        {
            Console.WriteLine("{0}考了{1}分", Name, Score);
        }
    }

    //老师类
    class Teacher
    {
        public void AddScore(Student stu)           //声明一个方法，给学生加分
        {
            stu.Score += 10;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Student student = new Student();           //实例化一个学生对象
            student.Name = "小明";
            student.Score = 55;
            Teacher teacher = new Teacher();           //实例化一个老师对象
            teacher.AddScore(student);                 //调用方法，加分
            student.ShowScore();                       //调用方法，输出分数
            Console.ReadLine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp10
{
    /// <summary>
    /// 学生类
    /// </summary>
    class Student : Person      //  类名后面加冒号，表示学生类，继承人类
    {
        public float Score { get; set; }        //学生类，在人类的基础上，增加一个分数属性
        public Student(string name, int age, string gender, float score) : base(name, age, gender)//声明一个有参构造方法，使用base关键字调用父类Person的有参构造方法
        {
            this.Score = score;
            Console.WriteLine("调用子类Student的有参构造方法");
        }
        public Student() { }//声明一个无参构造方法，当我们定义有参构造方法后，系统将不再默认生成无参构造方法
        public void Study()//声明一个方法，输出学习状态
        {
            Console.WriteLine("\n考了{0}分的{1}同学,今年{2}岁，性别：{3}，在学习中...",
                this.Score, this.Name, this.Age, this.Gender);
        }
    }
}

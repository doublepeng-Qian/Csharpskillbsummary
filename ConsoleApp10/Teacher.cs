using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp10
{
    /// <summary>
    /// 教师类
    /// </summary>
    class Teacher : Person          //  类名后面加冒号，表示教师类，继承人类
    {
        public string Subject { get; set; }          //教师类，在人类的基础上，增加一个学科属性
        public void Teach()
        {
            Console.WriteLine("\n教{0}科目的{1}老师，性别：{2}，年龄{3}在上课中...", this.Subject, this.Name, this.Gender, this.Age);
        }
        public Teacher(string name, int age, string gender, string subject)//声明一个有参构造方法，没有使用base关键字
        {
            this.Name = name;       //由于没有使用base关键词，因此这里要初始化
            this.Age = age;
            this.Gender = gender;
            this.Subject = subject;
            Console.WriteLine("调用子类Teacher的，有参构造方法");
        }
        public Teacher()
        {
            Console.WriteLine("调用子类Teacher的，无参构造方法");
        }
    }
}

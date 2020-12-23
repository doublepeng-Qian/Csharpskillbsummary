using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp10
{
    class Program
    {
        static void Main(string[] args)
        {
            Student student = new Student("jack", 18, "男", 88);//new一个学生对象，并初始化            
            List<Person> list = new List<Person>();//new一个泛型集合
            list.Add(student);//将学生对象，添加到泛型集合list中
            Console.WriteLine();//空行
            
            Teacher teacher = new Teacher("jhon", 30, "男", "C#");//new一个教师对象，并初始化
            list.Add(teacher);//将教师对象，添加到泛型集合list中（注意：此时list中，有两种数据类型）
            
            foreach (Person person in list)//遍历输出泛型集合中的数据
            {
                if (person is Student)//判断数据类型，如果是Student类型
                {
                    ((Student)person).Study();//将数据强制转换为Student类型，并调用Study()方法
                }
                
                if (person is Teacher)
                {
                    ((Teacher)person).Teach();
                }
            }
            
            Console.ReadLine();
        }
    }
}

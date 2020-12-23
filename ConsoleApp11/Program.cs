using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp11
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> persons = new List<Person>()//声明一个Person类型的集合，用初始化器批量添加数据
            {
                new Chinese("王迪"),           //new一个Chinese类的对象，王迪
                new American("jack"),         //new一个American类的对象，jack
                new Indian("jhon")             //new一个Indian类的对象，jhon
             };
            foreach (Person outItem in persons)//将集合persons里的数据，遍历输出
            {
                outItem.Eat();//调用对象的Eat()方法
            }
            Console.ReadLine();
        }
    }
}

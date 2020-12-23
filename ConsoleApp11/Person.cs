using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp11
{
    class Person
    {
        public string Name { get; set; }//声明一个姓名属性
        public Person(string name)//声明一个有参构造方法
        {
            this.Name = name;
        }
        public virtual void Eat()//使用virtual关键字，声明一个虚方法
        {
            Console.Write(this.Name + "来吃饭了！");
        }
    }
}

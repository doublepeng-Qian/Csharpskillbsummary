using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Clerk
    {
        private string _name;                   //声明一个私有字段_name
        public string Name { get; set; }        //声明一个自动属性

        private string _gender;
        public string Gender                    //声明一个只读属性
        {
            get
            {
                _gender = "男";
                return _gender;
            }
        }

        private int _age;
        public int Age                          //声明一个读写属性
        {
            get { return _age; }
            set
            {
                if (_age < 1 || _age > 100)     //判断写入的年龄数据，是否符合逻辑
                {
                    value = 18;                 //如果写入的年龄小于1或大于100，则设置value值为18
                }
                _age = value;                   //将value值，赋值给_age
            }
        }

        //声明方法，输出职员信息
        public void SayHello()
        {
            Console.WriteLine("{0}，{1}，{2}", Name, Gender, Age);
        }
    }
}

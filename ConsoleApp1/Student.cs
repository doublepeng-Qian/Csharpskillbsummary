using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Student
    {
        public string _name;
        public string _gender;
        public int _age;
        public Student()
        {

        }

        public void Write()
        {
            Console.WriteLine("{0}, {1}, {2}", _name, _gender, _age);
        }
    }
}

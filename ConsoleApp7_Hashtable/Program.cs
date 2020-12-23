using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ConsoleApp7_Hashtable
{
    //声明一个学生类Student
    class Student
    {
        public string Name { get; set; }
        public float Score { get; set; }
        public string Id { get; set; }

        //声明一个方法，输出学生的分数
        public void SayHello()
        {
            Console.WriteLine("大家好，我是学生：{0}，学号是{1}，我这次考了{2}分。", Name, Id, Score);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //声明一个Hashtable对象
            Hashtable hashtable = new Hashtable();      //初次使用，注意引入命名空间using System.Collections;
            //声明两个Student对象
            Student stu1 = new Student { Name = "张三", Score = 82, Id = "1001" };
            Student stu2 = new Student { Name = "李四", Score = 90.5f, Id = "1002" };
            //将上一步声明的两个student对象，添加到hashtable里（装箱操作）
            hashtable.Add(stu1.Id, stu1);      //Add方法里，stu1.Id是键，stu1是值
            hashtable.Add(stu2.Id, stu2);
            ////取出hashtable里的数据（拆箱操作），通过键取对应的元素
            //Student student = (Student)hashtable["1001"];
            ////调用student对象的方法
            //student.SayHello();
            //((Student)hashtable["1002"]).SayHello();        //将上面两行代码简化
            ////Console.ReadLine();


            ////查找是否存在某个元素，可以通过ContainsKey()方法和ContainsValue()方法，返回true或false
            //Console.WriteLine(hashtable.ContainsKey("1003"));
            //Console.WriteLine(hashtable.ContainsValue(stu1));

            ////使用Remove()方法，删除指定键值的元素
            //hashtable.Remove("1002");
            //Console.WriteLine("hashtable元素中共有{0}个元素", hashtable.Count);      //使用Count属性，获取元素个数
            ////使用Clear()方法，删除全部元素
            //hashtable.Clear();

            //遍历hashtable里的数据
            Console.WriteLine("1、遍历hashtable里的key");
            foreach (object obj in hashtable.Keys)      //遍历hashtable里的key
            {
                Console.WriteLine(obj);
            }

            Console.WriteLine("\n2、遍历hashtable里的Values");
            foreach (object obj in hashtable.Values)      //遍历hashtable里的Values
            {
                Student stu = (Student)obj;
                stu.SayHello();
            }

            Console.WriteLine("\n3、同时遍历hashtable里的key和Value");
            foreach (DictionaryEntry entry in hashtable)      //遍历hashtable里的Values
            {
                Console.WriteLine(entry.Key);
                Student stu = (Student)entry.Value;
                stu.SayHello();
            }

            Console.ReadLine();
        }
    }

}

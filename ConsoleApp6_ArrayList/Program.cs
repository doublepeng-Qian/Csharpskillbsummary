using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ConsoleApp6_ArrayList
{
    class Program
    {
        static void Main(string[] args)
        {
            //声明一个动态数组，并指定初始容量是3
            ArrayList arrayList = new ArrayList(3);
            Console.WriteLine("arrayList初始化后，有{0}个元素", arrayList.Count);//Count属性，获取元素个数

            //使用Add方法，向ArrayList中添加元素，每次只能添加一个元素
            arrayList.Add(1024);
            arrayList.Add("hello");
            arrayList.Add('a');
            arrayList.Add(25.6);
            Console.WriteLine("使用Add方法添加4个元素之后，有{0}个元素", arrayList.Count);

            string[] stuStringArray = { "张三", "李四", "王五", "赵六" };       //声明一个string类型的数组，并存放数据
            //AddRange方法，用于一次性向ArrayList中添加多个元素，可以是一个数组
            arrayList.AddRange(stuStringArray);
            Console.WriteLine("使用AddRange方法添加数组之后，有{0}个元素", arrayList.Count);

            //使用Insert方法，向ArrayList中指定的索引位置，添加元素
            arrayList.Insert(1, "我会出现在索引1的位置");
            Console.WriteLine("使用Insert方法添加元素之后，有{0}个元素", arrayList.Count);

            //使用foreach方法，遍历ArrayList中的元素
            foreach (object a in arrayList)
            {
                Console.Write(a + "\t");
            }
            Console.ReadLine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp12
{
    //使用abstract关键字，定义一个抽象类Animal
    abstract class Animal
    {
        public abstract void Move();        //定义一个抽象方法Move()，注意抽象方法没有方法体
    }
    //定义子类Dog
    class Dog : Animal
    {
        public override void Move()         //重写父类的抽象方法
        {
            Console.WriteLine("小狗跑了起来");
        }
    }
    //定义子类Bird
    class Bird : Animal
    {
        public override void Move()
        {
            Console.WriteLine("小鸟飞了起来");
        }
    }
    //定义子类Fish
    class Fish : Animal
    {
        public override void Move()
        {
            Console.WriteLine("小鱼游了起来");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Animal dog = new Dog();         //将子类对象new Dog()，赋值给父类对象Animal dog
            dog.Move();        //编译器根据具体对象的类型（子类Dog），决定调用那种Move方法
            Animal bird = new Bird();
            bird.Move();
            Animal fish = new Fish();
            fish.Move();
            Console.ReadLine();
        }
    }
}

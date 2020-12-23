using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp11
{
    class Chinese : Person    //继承父类Person
    {
        public Chinese(string name) : base(name)    //使用base关键字调用父类的有参构造方法
        {

        }
        public override void Eat()
        {
            base.Eat();
            Console.WriteLine(this.Name + "是中国人，习惯用筷子吃饭");
        }
    }
}

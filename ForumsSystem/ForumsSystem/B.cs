using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumsSystem
{
    class B : A
    {
        public B() :base()
        {

        }

        public override void init()
        {
            Console.WriteLine("B init");
        }

    }
}

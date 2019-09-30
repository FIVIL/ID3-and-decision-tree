using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace prolog
{
    class Program
    {
        static void Main(string[] args)
        {
            PEngine pe = new PEngine();
            string s;
            pe.execute(@"kb:
a(a)
");
            pe.execute(@"kbc:
b(c):-
~a(a)
e(e)
");
            pe.execute(@"kbc:
b(d):-
c(c)
~a(a)
d(d)
e(e)
");
            Console.WriteLine(pe.execute("b(c)"));


            Console.ReadKey();
        }

    }
}

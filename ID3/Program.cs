using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ID3
{
    class Program
    {
        static void Main(string[] args)
        {
            //decisionTree d = new decisionTree(@"C:\Users\h\Desktop\uni\AIP\tab.xml", @"C:\Users\h\Desktop\uni\AIP\tab2.xml");
            //d.fill();
            //d.print();
            //Console.WriteLine("done");
            //Console.WriteLine(d.findlvl());
            //////////////////////////////////////////////////////////////////////
            //ices ic = new ices();
            //ic.loadexcel(@"C:\Users\h\Desktop\uni\AIP\table_final.xlsx");
            //ic.loadxml(@"C:\Users\h\Desktop\uni\AIP\tab2.xml");
            //Console.WriteLine(ic);
            //////////////////////////////////////////////////////////////////////
            symptoms s = new symptoms();
            s.loadexcel(@"C:\Users\h\Desktop\bakhtar-semi\b.xlsx");
            ////s.save(@"C:\Users\h\Desktop\uni\AIP\tab.xml");
            //diseases d = new diseases(s);
            //statics.H(d, s);
            //Node a = new Node(null, 1, d[0]);
            //Console.WriteLine(a);
            //Node b = new Node(null, 0, s[0]);
            //Console.WriteLine(b);
            //a = b;
            //Console.WriteLine(a);
            //s.sort();
            //Console.WriteLine(s);
            ///////////////////////////////////////////////////////////////////////
            //Console.ReadKey();
        }
    }
}

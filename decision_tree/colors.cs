using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace decision_tree
{
    public class colors
    {
        public bool Isdark;
        public string[] cs = new string[3];
        public string back;
        public colors(bool inb,string s,string i,string d,string b)
        {
            Isdark = inb;
            cs[0] = i;
            cs[1] = d;
            cs[2] = s;
            back = b;
        }
    }
}

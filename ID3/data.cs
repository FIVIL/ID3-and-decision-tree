using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID3
{
    public class data
    {
        public int i;
        public symptom s;
        public disease d;
        public ic ic;
        public data(int i, object obj)
        {
            if (i==0)
            {
                ic = null;
                d = null;
                s = (symptom)obj;
            }
            else if(i==1)
            {
                ic = null;
                s = null;
                d = (disease)obj;
            }
            else if(i==2)
            {
                ic = (ic)obj;
                s = null;
                d = null;
            }
            else
            {
                ic = null;
                s = null;
                d = null;
            }
        }
        public override string ToString()
        {
            string st = "";
            if (i == 0) st += s;
            else if (i == 1) st += d;
            else st += ("null\r\n_________________________\r\n");
            return st;
        }
    }
}

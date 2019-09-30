using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prolog
{
    public class Predicate
    {
        public string Name { get; set; }
        public List<string> varables;
        public bool YN;
        public Predicate(string s)
        {
            if (s[0] == '~')
            {
                YN = false;
                s = s.Remove(0, 1);
            }
            else YN = true;
            string p;
            int h = s.IndexOf('(');
            p = s.Substring(0, h);
            s = s.Remove(0, h + 1);
            Name = p;
            List<string> sl = new List<string>();
            while (true)
            {
                h = s.IndexOf(',');
                if (h == -1) break;
                p = s.Substring(0, h);
                s = s.Remove(0, h + 1);
                sl.Add(p);
            }
            h = s.IndexOf(')');
            p = s.Substring(0, h);
            sl.Add(p);
            varables = new List<string>();
            foreach (var item in sl)
            {
                varables.Add(item);
            }
        }
        public Predicate(string n, string[] a,bool b)
        {
            Name = n;
            varables = new List<string>();
            foreach (var item in a)
            {
                varables.Add(item);
            }
            YN = b;
        }
        public Predicate(Predicate inp)
        {
            Name = inp.Name;
            varables = new List<string>();
            foreach (var item in inp.varables)
            {
                varables.Add(item);
            }
            YN = inp.YN;
        }
        public void SetYN(bool inp)
        {
            YN = inp;
        }
        public bool Verification(string[] inp)
        {
            foreach (var item in inp)
            {
                if (!varables.Contains(item)) return false;
            }
            return true;
        }
        public bool Verification(Predicate p)
        {
            string[] inp = p.varables.ToArray();
            return Verification(inp);
        }
        public override bool Equals(object obj)
        {
            Predicate p = (Predicate)obj;
            if (YN != p.YN) return false;
            if (Name == p.Name)
            {
                foreach (var item in varables)
                {
                    if (!p.varables.Contains(item)) return false;
                }
                return true;
            }
            return false;
        }
        public string inside()
        {
            return varables[0];
        }
        public override string ToString()
        {
            string s = "";
            if (!YN) s += "~";
            s += Name;
            s += "(";
            for (int i = 0; i < varables.Count - 1; i++) 
            {
                s += (varables[i] + ",");
            }
            s += (varables[varables.Count - 1] + ")");
            return s;
        }
    }
}

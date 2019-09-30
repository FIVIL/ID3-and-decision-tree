using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prolog
{
    public class ConditionalPredicate
    {
        public Predicate root;
        public List<Predicate> conditions;
        public ConditionalPredicate(Predicate r,List<Predicate> cond)
        {
            set(r, cond);
        }
        public ConditionalPredicate(string s)
        {
            string p;
            int h = s.IndexOf(":-");
            p = s.Substring(0, h);
            s = s.Remove(0, h + 2);
            root = new Predicate(p);
            List<string> sl = new List<string>();
            while (true)
            {
                h = s.IndexOf(',');
                if (h == -1) break;
                p = s.Substring(0, h);
                s = s.Remove(0, h + 3);
                sl.Add(p);
            }
            h = s.IndexOf('.');
            p = s.Substring(0, h);
            sl.Add(p);
            conditions = new List<Predicate>();
            foreach (var item in sl)
            {
                conditions.Add(new Predicate(item));
            }
            
        }
        public ConditionalPredicate(ConditionalPredicate cp)
        {
            set(cp.root, cp.conditions);

        }
        public void set(Predicate r, List<Predicate> cond)
        {
            root = new Predicate(r);
            root.SetYN(true);
            conditions = new List<Predicate>();
            foreach (var item in cond)
            {
                Predicate h = new Predicate(item);
                conditions.Add(h);
            }
        }
        public bool check(List<Predicate> pl)
        {
            foreach (var item in conditions)
            {
                if (!pl.Contains(item)) return false;
            }
            return true;
        }
        public override string ToString()
        {
            string s = "";
            s += root.ToString();
            s += ":-\r\n";
            foreach (var item in conditions)
            {
                s += (item.ToString() + "\r\n");
            }
            return s;
        }
        public string tostring2()
        {
            string s = "";
            foreach (var item in conditions)
            {
                s += (item.ToString() + "\r\n");
            }
            return s;
        }
    }
}

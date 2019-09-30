using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prolog
{
    public class PEngine
    {
        workingStorage ws;
        knowledgeBase kb;
        int TopGoal;
        List<string> PredicatesName;
        public PEngine()
        {
            ws = new workingStorage();
            kb = new knowledgeBase();
            PredicatesName = new List<string>();
            TopGoal = -1;
        }
        private void NewPredicate(Predicate p)
        {
            if (!PredicatesName.Contains(p.Name)) PredicatesName.Add(p.Name);
            kb.add(p);
        }
        private void NewConditionalPredicate(ConditionalPredicate p)
        {
            if (!PredicatesName.Contains(p.root.Name)) PredicatesName.Add(p.root.Name);
            kb.add(p);
        }
        private void asserta(string p)
        {
            Predicate pr = new Predicate(p);
            PredicatesName.Add(pr.Name);
            ws.asserta(pr);
        }
        private void assertz(string p)
        {
            Predicate pr = new Predicate(p);
            PredicatesName.Add(pr.Name);
            ws.assertz(pr);
        }
        private Predicate BackWardChaininigTopGoal()
        {
            Predicate p = null;
            List<Predicate> pl = new List<Predicate>();
            foreach (var item in ws.predicates)
            {
                pl.Add(item);
            }
            foreach (var item in kb.predicates)
            {
                pl.Add(item);
            }
            int itrator = 0;
            foreach (ConditionalPredicate item in kb.ConditionalPredicates)
            {
                if(item.check(pl))
                {
                    if (itrator > TopGoal) 
                    {
                        p = new Predicate(item.root);
                        TopGoal = itrator;
                        return p;
                    }
                }
                itrator++;
            }
            return p;
        }
        private Predicate BackWardChaininigName(Predicate q)
        {
            Predicate p = null;
            List<Predicate> pl = new List<Predicate>();
            pl.AddRange(ws.predicates);
            pl.AddRange(kb.predicates);
            foreach (ConditionalPredicate item in kb.ConditionalPredicates)
            {
                if (item.root.Verification(q))
                {
                    if (item.check(pl))
                    {
                        p = new Predicate(item.root);
                        return p;
                    }
                }
            }
            return p;
        }
        private void setknown(string s)
        {
            List<string> sl = new List<string>();
            string p;
            while (true)
            {
                int h = s.IndexOf('\r');
                if (h == -1) break;
                p = s.Substring(0, h);
                s = s.Remove(0, h + 2);
                sl.Add(p);
            }
            foreach (var item in sl)
            {
               NewPredicate(new Predicate(item));
            }
        }
        private void setconditions(string s)
        {
            List<Predicate> pl = new List<Predicate>();
            Predicate root;
            string p;
            int h = s.IndexOf(":-");
            p = s.Substring(0, h);
            s = s.Remove(0, h + 4);
            root = new Predicate(p);
            while (true)
            {
                h = s.IndexOf('\r');
                if (h == -1) break;
                p = s.Substring(0, h);
                s = s.Remove(0, h + 2);
                pl.Add(new Predicate(p));
            }
            NewConditionalPredicate(new ConditionalPredicate(root, pl));
        }
        public string execute(string s)
        {
            string retvalue = string.Empty;
            if (s.Contains("asserta"))
            {
                int h = s.IndexOf("asserta");
                h += 7;
                int h2 = s.IndexOf('(', h);
                int h3 = s.IndexOf(')', h2);
                int h4 = s.IndexOf(')', h3);
                string p = s.Substring(h2 + 1, h3 - h2);
                asserta(p);
                retvalue += ("true/r/n");
            }
            else if (s.Contains("assertz"))
            {
                int h = s.IndexOf("assertz");
                h += 7;
                int h2 = s.IndexOf('(', h);
                int h3 = s.IndexOf(')', h2);
                int h4 = s.IndexOf(')', h3);
                string p = s.Substring(h2 + 1, h3 - h2);
                assertz(p);
                retvalue += ("true/r/n");
            }
            else if (s.Contains("kb:"))
            {
                int h = s.IndexOf("kb:");
                h += 5;
                s = s.Remove(0, h);
                setknown(s);
                retvalue += ("true/r/n");
            }
            else if (s.Contains("kbc:"))
            {
                int h = s.IndexOf("kbc:");
                h += 6;
                s = s.Remove(0, h);
                setconditions(s);
                retvalue += ("true/r/n");
            }
            else if(s.Contains("top goal:"))
            {
                TopGoal = -1;
                Predicate result = null;
                result = BackWardChaininigTopGoal();
                if (result != null) retvalue += (result + "\r\n");
                else retvalue += ("false\r\n");
            }
            else if (s.Contains("name goal:"))
            {
                int h = s.IndexOf("name goal:");
                s = s.Remove(0, h+10);
                Predicate result = null;
                List<string> slist = new List<string>();
                h = s.IndexOf('(');
                string pin = s.Substring(0, h);
                s = s.Remove(0, h+1);
                string pp;
                while (true)
                {
                    h = s.IndexOf(',');
                    if (h == -1) break;
                    pp = s.Substring(0, h);
                    s = s.Remove(0, h + 1);
                    slist.Add(pp);
                }
                h = s.IndexOf(')');
                pp = s.Substring(0, h);
                slist.Add(pp);
                Predicate hold = new Predicate(pin, slist.ToArray(),true);
                result = BackWardChaininigName(hold);
                if (result != null) retvalue += (result.Name + "\r\ntrue\r\n");
                else retvalue += ("false\r\n");
            }
            else if (s.Contains("X"))
            {
                int h = s.IndexOf('(');
                string p = s.Substring(0, h);
                List<Predicate> lp = new List<Predicate>();
                lp.AddRange(ws.predicates);
                lp.AddRange(kb.predicates);
                bool flag = false;
                foreach (var item in lp)
                {
                    if (item.Name == p)
                    {
                        retvalue = ("X=" + item.inside());
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    List<ConditionalPredicate> cp = new List<ConditionalPredicate>();
                    cp.AddRange(kb.ConditionalPredicates);
                    foreach (var item in cp)
                    {
                        if (item.root.Name == p)
                        {
                            string blockresult = execute("name goal:" + item.root.ToString());
                            if (blockresult.Contains("true"))
                            {
                                flag = true;
                                retvalue = ("X=" + item.root.inside());
                            }
                        }
                    }
                }
                if(!flag) retvalue = ("false\r\n");
            }
            else if (s.Contains("why:"))
            {
                s = s.Remove(0, 4);
                int h = s.IndexOf("(");
                bool yn = true;
                string p;
                if (s[0] == '~')
                {
                    yn = false;
                    p = s.Substring(1, h-1);
                }
                else p = s.Substring(0, h);
                s = s.Remove(0, h + 1);
                h = s.IndexOf(")");
                string p2 = s.Substring(0, h);
                string[] str = new string[1];
                str[0] = p2;
                Predicate pred = new Predicate(p,str, yn);
                retvalue += why(pred);
            }
            else if (s.Contains("how:"))
            {
                s = s.Remove(0, 4);
                int h = s.IndexOf("(");
                bool yn = true;
                string p;
                if (s[0] == '~')
                {
                    yn = false;
                    p = s.Substring(1, h - 1);
                }
                else p = s.Substring(0, h);
                s = s.Remove(0, h + 1);
                h = s.IndexOf(")");
                string p2 = s.Substring(0, h);
                string[] str = new string[1];
                str[0] = p2;
                Predicate pred = new Predicate(p, str, yn);
                retvalue += how(pred);
            }
            else if (s.Contains("why not:"))
            {
                s = s.Remove(0, 8);
                int h = s.IndexOf("(");
                bool yn = true;
                string p;
                if (s[0] == '~')
                {
                    yn = false;
                    p = s.Substring(1, h - 1);
                }
                else p = s.Substring(0, h);
                s = s.Remove(0, h + 1);
                h = s.IndexOf(")");
                string p2 = s.Substring(0, h);
                string[] str = new string[1];
                str[0] = p2;
                Predicate pred = new Predicate(p, str, yn);
                retvalue += whynot(pred);
            }
            else
            {
                Predicate predic = new Predicate(s);
                List<Predicate> searchspace = new List<Predicate>();
                searchspace.AddRange(ws.predicates);
                searchspace.AddRange(kb.predicates);
                if (searchspace.Contains(predic))
                {
                    retvalue = "true";
                }
                else
                {
                    retvalue = "false";
                }
            }
            return retvalue;
        }
        public string why(Predicate p)
        {
            string s = "";
            p.SetYN(false);
            foreach (var item in kb.ConditionalPredicates)
            {
                if (item.conditions.Contains(p))
                {
                    s += item.ToString();
                }
            }
            p.SetYN(true);
            foreach (var item in kb.ConditionalPredicates)
            {
                if (item.conditions.Contains(p))
                {
                    s += item.ToString();
                }
            }
            if (s == string.Empty) s = "null";
            return s;
        }
        public string how(Predicate pred)
        {
            string p = "";
            pred.SetYN(true);
            foreach (var item in kb.ConditionalPredicates)
            {
                if (item.root.Equals(pred)) p += item.tostring2();
            }
            if (p == string.Empty) p = "null";
            return p;
        }
        public string whynot(Predicate pred)
        {
            string p = "";
            pred.SetYN(true);
            List<Predicate> pl = new List<Predicate>();
            pl.AddRange(ws.predicates);
            pl.AddRange(kb.predicates);
            foreach (var item in kb.ConditionalPredicates)
            {
                if (item.root.Equals(pred))
                {
                    foreach (var item2 in item.conditions)
                    {
                        if (!pl.Contains(item2)) p += (item2.ToString() + "\r\n");
                    }
                    break;
                }
            }
            if (p == string.Empty) p = "null";
            return p;
        }
        public string wstostirng()
        {
            string s = "";
            foreach (var item in ws.predicates)
            {
                s += (item + "\r\n");
            }
            return s;
        }
        public List<string> wstolist()
        {
            List<string> outval = new List<string>();
            foreach (var item in ws.predicates)
            {
                outval.Add(item + "\r\n");
            }
            return outval;
        }
        public string kbtostring()
        {
            string s = "";
            foreach (var item in kb.predicates)
            {
                s += (item + "\r\n\r\n");
            }
            foreach (var item in kb.ConditionalPredicates)
            {
                s += (item + "\r\n\r\n");
            }
            return s;
        }
        public void reset()
        {
            ws.predicates.Clear();
        }
        public void clear()
        {
            reset();
            kb.predicates.Clear();
            kb.ConditionalPredicates.Clear();
        }
    }
}

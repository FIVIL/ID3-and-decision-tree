using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID3
{
    public static class statics
    {
        public static Dictionary<int, string> map = new Dictionary<int, string>();
        public static Dictionary<string, int> map2 = new Dictionary<string, int>();
        public static Dictionary<int, string> map3 = new Dictionary<int, string>();
        public static void map2set()
        {
            map2.Clear();
            for (int i = 0; i < map.Count; i++) map2.Add(map[i], i);
        }
        public static symptoms clear(symptoms s, int x)
        {
            symptoms p = new symptoms();
            p.Clear();
            diseases ds = new diseases(s);
            foreach (symptom item in s)
            {
                if (item.Disease.Count < x)
                {
                    //foreach (disease d in ds)
                    //{
                    //    if (d.symptoms.Count < 2 && d.symptoms.Contains(item.Number)) p.Add(item);
                    //}
                    p.Add(item);
                }
                //else
                //{
                //    p.Add(item);
                //}

            }
            return p;
        }
        //public static double entropy(double inp)
        //{

        //    return (-(inp * Math.Log(inp, 2)) - ((1 - inp) * Math.Log((1 - inp), 2)));
        //}
        //public static double entropy(disease d,symptom s)
        //{
        //    return (d.py) * (d.symptoms.Contains(s.Number)?(entropy((double)1/d.symptoms.Count)):0) + (d.pn) * (d.symptoms.Contains(s.Number) ? entropy(((double)(65-d.symptoms.Count) / (65))) : 0);
        //}
        //public static double InformationGain(symptom s,disease d)
        //{
        //    return entropy(s.py) - entropy(d,s);
        //}
        public static double pck(disease d,symptom s,bool b)
        {
            if (b)
            {
                if (s.Disease.Contains(d.Num)) return (double)1 / s.Disease.Count;
                else return 0;
            }
            else
            {
                if (s.Disease.Contains(d.Num)) return 0;
                else return (double)1 / (9 - s.Disease.Count);
            }
        }
        public static double H(diseases d,symptom s)
        {
            double a = 0, b = 0;
            foreach (var item in d)
            {
                if(pck(item, s, true)!=0) a += (pck(item, s, true) * Math.Log(pck(item, s, true), 2));
                if(pck(item, s, false)!=0) b += (pck(item, s, false) * Math.Log(pck(item, s, false), 2));
                //Console.WriteLine(a + "       " + b);
            }
            
            return (s.py * -a) + (s.pn * -b);
        }
        public static void H(diseases d,symptoms s)
        {
            foreach (var item in s)
            {
                item.entropy = H(d, item);
            }
        }
        public static int Ran(symptoms s)
        {
            if (s.Count == 1) return 0;
            symptom s1;
            Random rnd = new Random();
            int i = 1;
            for (i = 1; i < s.Count; i++)
            {
                s1 = s[i - 1];
                if (s1.entropy != s[i].entropy)
                {
                    break;
                }

            }
            return rnd.Next(0,i-1);
        }
        public static int next(symptoms s,int p)
        {
            symptom s1;
            Random rnd = new Random();
            int i = p;
            for (i = p; i < s.Count; i++)
            {
                s1 = s[i - 1];
                if (s1.entropy != s[i].entropy)
                {
                    break;
                }

            }
            return i+1;
        }
    }
}

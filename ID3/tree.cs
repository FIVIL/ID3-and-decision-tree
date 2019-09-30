using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID3
{
    public class tree
    {
        public Node[] nodes = new Node[520];
        public List<int> endpoints = new List<int>();
        string p;
        public decisionTree d;
        public Dictionary<int, int> map = new Dictionary<int, int>();
        public int passvalue;
        public tree(string path1, string path2)
        {
            endpoints.Clear();
            map.Clear();
            for (int j = 0; j < 520; j++)
            {
                nodes[j] = new Node();
            }

            d = new decisionTree(path1, path2);
            d.fill();
            int i = 1;
            nodes[i] = d.root;
            for (int j = 1; j < 259; j++)
            {
                if (nodes[j].NodeDef == 0)
                {
                    nodes[j * 2] = nodes[j].yes;
                    nodes[(j * 2) + 1] = nodes[j].no;
                }
                if (nodes[j].NodeDef == 2)
                {
                    endpoints.Add(j);
                    map.Add(j, map.Count);
                    nodes[j * 2] = nodes[j].ic;
                    nodes[(j * 2) + 1].NodeDef = 3;
                }
                if (nodes[j].NodeDef == 1)
                {
                    if (nodes[j / 2].NodeDef == 0 && nodes[j / 4].NodeDef == 0)
                    {
                        endpoints.Add(j);
                        map.Add(j, map.Count);
                    }
                    nodes[j * 2] = nodes[j * 2];
                    nodes[(j * 2) + 1] = nodes[(j * 2) + 1];
                }
            }
            int numberofnodes = 0;
            for (int j = 0; j < 520; j++)
            {
                if (nodes[j].NodeDef < 4) numberofnodes = j;
            }
            int lvl = (int)Math.Log(numberofnodes, 2) + 1;
            int numberoflastlvlnodes = (int)Math.Pow(2, lvl - 1);
            int size = (int)((numberoflastlvlnodes * 2) - 1);
            p += (lvl + "\r\n");
            p += (numberofnodes + "\r\n");
            p += (numberoflastlvlnodes + "\r\n");
            p += (size + "\r\n");
            passvalue = size;
            if (size < 70) size *= 20;
            else size *= 10;
            nodes[1].X_pos = (size / 2);
            nodes[1].Y_pos = 10;
            size /= 4;
            List<int> tc = new List<int>();
            tc.Add(1);
            tc.Add(3);
            tc.Add(7);
            tc.Add(15);
            tc.Add(31);
            tc.Add(63);
            for (int j = 1; j < 259; j++)
            {

                if (nodes[j].NodeDef == 0)
                {
                    nodes[j * 2].X_pos = ((nodes[j].X_pos) - (size));
                    nodes[(j * 2) + 1].X_pos = ((nodes[j].X_pos) + (size));
                    nodes[j * 2].Y_pos = nodes[j].Y_pos + 150;
                    nodes[(j * 2) + 1].Y_pos = nodes[j].Y_pos + 150;
                }
                if (nodes[j].NodeDef == 2)
                {
                    nodes[j * 2].X_pos = (nodes[j].X_pos);
                    nodes[j * 2].Y_pos = nodes[j].Y_pos + 150;
                }
                if (tc.Contains(j)) size /= 2;
            }
            //int numberofallpossiblenodes = (int)Math.Pow(2, lvl) - 1;
            //int y = lvl * 150;
            //int x = 0;
            //for (int j = numberofallpossiblenodes-numberoflastlvlnodes; j < numberofallpossiblenodes; j++)
            //{
            //    nodes[j].Y_pos = y;
            //    nodes[j].X_pos = x;
            //    x += 2;
            //}
            //for (int j = numberofallpossiblenodes; j >1 ; j-=2)
            //{
            //    nodes[j / 2].Y_pos = nodes[j].Y_pos - 150;
            //    nodes[j / 2].Y_pos = (nodes[j].X_pos + nodes[j - 1].X_pos) / 2;
            //}
            for (int j = 0; j < 520; j++)
            {
                if (nodes[j / 4].name() == "Rubella") nodes[j].X_pos += 30;
            }
            //Puffy eyes or face
            int ct = 0;
            foreach (var item in nodes)
            {
                if (item.name() == "Puffy eyes or face") ct++;
            }
            if (ct > 1) reset(path1, path2);
        }
        public void reset(string path1, string path2)
        {
            d.clear();
            endpoints.Clear();
            map.Clear();
            for (int j = 0; j < 100; j++)
            {
                nodes[j] = new Node();
            }

            d = new decisionTree(path1, path2);
            d.fill();
            int i = 1;
            nodes[i] = d.root;
            for (int j = 1; j < 259; j++)
            {
                if (nodes[j].NodeDef == 0)
                {
                    nodes[j * 2] = nodes[j].yes;
                    nodes[(j * 2) + 1] = nodes[j].no;
                }
                if (nodes[j].NodeDef == 2)
                {
                    endpoints.Add(j);
                    map.Add(j, map.Count);
                    nodes[j * 2] = nodes[j].ic;
                    nodes[(j * 2) + 1].NodeDef = 3;
                }
                if (nodes[j].NodeDef == 1)
                {
                    if (nodes[j / 2].NodeDef == 0 && nodes[j / 4].NodeDef == 0)
                    {
                        endpoints.Add(j);
                        map.Add(j, map.Count);
                    }
                    nodes[j * 2] = nodes[j * 2];
                    nodes[(j * 2) + 1] = nodes[(j * 2) + 1];
                }
            }
            int numberofnodes = 0;
            for (int j = 0; j < 520; j++)
            {
                if (nodes[j].NodeDef < 4) numberofnodes = j;
            }
            int lvl = (int)Math.Log(numberofnodes, 2) + 1;
            int numberoflastlvlnodes = (int)Math.Pow(2, lvl - 1);
            int size = (int)((numberoflastlvlnodes * 2) - 1);
            p += (lvl + "\r\n");
            p += (numberofnodes + "\r\n");
            p += (numberoflastlvlnodes + "\r\n");
            p += (size + "\r\n");
            passvalue = size;
            if (size < 70) size *= 20;
            else size *= 10;
            nodes[1].X_pos = (size / 2);
            nodes[1].Y_pos = 10;
            size /= 4;
            List<int> tc = new List<int>();
            tc.Add(1);
            tc.Add(3);
            tc.Add(7);
            tc.Add(15);
            tc.Add(31);
            tc.Add(63);
            for (int j = 1; j < 259; j++)
            {

                if (nodes[j].NodeDef == 0)
                {
                    nodes[j * 2].X_pos = ((nodes[j].X_pos) - (size));
                    nodes[(j * 2) + 1].X_pos = ((nodes[j].X_pos) + (size));
                    nodes[j * 2].Y_pos = nodes[j].Y_pos + 150;
                    nodes[(j * 2) + 1].Y_pos = nodes[j].Y_pos + 150;
                }
                if (nodes[j].NodeDef == 2)
                {
                    nodes[j * 2].X_pos = (nodes[j].X_pos);
                    nodes[j * 2].Y_pos = nodes[j].Y_pos + 150;
                }
                if (tc.Contains(j)) size /= 2;
            }
            //int numberofallpossiblenodes = (int)Math.Pow(2, lvl) - 1;
            //int y = lvl * 150;
            //int x = 0;
            //for (int j = numberofallpossiblenodes-numberoflastlvlnodes; j < numberofallpossiblenodes; j++)
            //{
            //    nodes[j].Y_pos = y;
            //    nodes[j].X_pos = x;
            //    x += 2;
            //}
            //for (int j = numberofallpossiblenodes; j >1 ; j-=2)
            //{
            //    nodes[j / 2].Y_pos = nodes[j].Y_pos - 150;
            //    nodes[j / 2].Y_pos = (nodes[j].X_pos + nodes[j - 1].X_pos) / 2;
            //}
            for (int j = 0; j < 259; j++)
            {
                if (nodes[j / 4].name() == "Rubella") nodes[j].X_pos += 30;
            }
            int ct = 0;
            foreach (var item in nodes)
            {
                if (item.name() == "Puffy eyes or face") ct++;
            }
            if (ct > 1) reset(path1, path2);
        }
        public string[] rulles()
        {
            string[] s = new string[endpoints.Count];
            for (int i = 0; i < endpoints.Count; i++)
            {
                s[i] += "if:\r\n";
                int h = endpoints[i];
                while (h > 1)
                {
                    if (h % 2 == 0)
                    {
                        if (nodes[h / 2].NodeDef == 0) s[i] += (nodes[h / 2].name() + "\r\n");
                    }
                    else
                    {
                        if (nodes[h / 2].NodeDef == 0) s[i] += ("not " + nodes[h / 2].name() + "\r\n");
                    }
                    h /= 2;
                }
                s[i] += "then: " + nodes[endpoints[i]].name();
            }

            return s;
        }
        public List<string> IcPredicate()
        {
            List<string> s = new List<string>();
            for (int i = 0; i < endpoints.Count; i++)
            {
                if (nodes[endpoints[i]].NodeDef == 2)
                {
                    if (nodes[endpoints[i]].name() == "Rubella")
                    {
                        string p = (nodes[endpoints[i]].name() + "(");
                        p += (nodes[endpoints[i] * 4].name() + "):-\r\n");
                        p += ("symptom(" + nodes[endpoints[i] * 2].name() + ")");
                        s.Add(p);
                        p = (nodes[endpoints[i]].name() + "(");
                        p += (nodes[(endpoints[i] * 4) + 1].name() + "):-\r\n");
                        p += ("~symptom(" + nodes[endpoints[i] * 2].name() + ")");
                        s.Add(p);
                    }
                    else
                    {
                        string p = (nodes[endpoints[i]].name() + "(");
                        p += (nodes[endpoints[i]*2].name() + ")");
                        s.Add(p);
                    }
                }
            }
            return s;
        }
        public List<string> IcName()
        {
            List<string> s = new List<string>();
            for (int i = 0; i < endpoints.Count; i++)
            {
                if (nodes[endpoints[i]].NodeDef == 2)
                {
                    s.Add("ic("+ nodes[endpoints[i]].name() + ")\r\n");
                }
            }
            return s;
        }
        public string[] ConvertToPredicate()
        {
            string[] s = new string[endpoints.Count];
            for (int i = 0; i < endpoints.Count; i++)
            {
                s[i] = ("disease(" + nodes[endpoints[i]].name() + "):-\r\n");
                int h = endpoints[i];
                while (h > 1)
                {
                    if (h % 2 == 0)
                    {
                        if (nodes[h / 2].NodeDef == 0) s[i] += ("symptom(" + nodes[h / 2].name() + ")\r\n");
                    }
                    else
                    {
                        if (nodes[h / 2].NodeDef == 0) s[i] += ("~symptom(" + nodes[h / 2].name() + ")\r\n");
                    }
                    h /= 2;
                }
            }
            return s;
        }
        public List<string> ConvertSymptomsToPredicate(string[] s)
        {
            List<string> p = new List<string>();
            for (int i = 0; i < s.Length; i++)
            {
                while (true)
                {
                    int h = s[i].IndexOf("symptom");
                    if (h == -1) break;
                    s[i] = s[i].Remove(0, h);
                    int h2 = s[i].IndexOf('\r');
                    String v = s[i].Substring(0, h2);
                    s[i] = s[i].Remove(0, h2 + 2);
                    System.Diagnostics.Debug.WriteLine(v);
                    if (!p.Contains(v)) p.Add(v);
                }
            }
            p.Add("symptom(Puffy eyes or face)");
            return p;
        }
            public override string ToString()
        {
            //p = "";
            for (int i = 0; i < 520; i++)
            {
                p += (i + " " + nodes[i] + "\r\n");
            }
            return p;
        }
    }
}

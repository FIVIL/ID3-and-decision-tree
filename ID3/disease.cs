using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID3
{
    public class disease
    {
        public double py { get; set; }
        public double pn { get; set; }
        public int Num { get; set; }
        public string Name { get; set; }
        public bool Isic { get; set; }
        public List<int> symptoms { get; set; }
        public disease(int x,string name,bool isic,symptoms s)
        {
            Num = x;
            symptoms = new List<int>();
            Name = name;
            Isic = isic;
            for(int i = 0; i < s.Count; i++)
            {
                if (s[i].Disease.Contains(statics.map2[name])) symptoms.Add(i);
            }
            py = ((double)symptoms.Count / s.Count);
            pn = 1 - py;
        }
        public override string ToString()
        {
            string s="";
            s += ("name: "+Name + "\r\nsymptoms:\r\n");
            foreach (var item in symptoms) s += (statics.map3[item] + "\r\n");
            s += "__________________________________________________________________";
            s += "\r\n";
            return s;
        }
    }
}

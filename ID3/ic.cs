using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace ID3
{
    [Serializable()]
   public class ic
    {
        public List<int> diseases;
        public List<int> symptoms;
        public int Number { get; set; }
        public string Name { get; set; }
        public string Question { get; set; }
        public ic(int n,string name,List<int> d,List<int> s,string q)
        {
            diseases = new List<int>();
            symptoms = new List<int>();
            Number = n;
            Name = name;
            foreach (var item in s)
            {
                symptoms.Add(item);
            }
            foreach (var item in d)
            {
                diseases.Add(item);
            }
            Question =q;
        }
        public ic()
        {

        }
        public override string ToString()
        {
            string s = "";
            s += ("name: "+Name + "\r\n");
            s += ("diseases:\r\n");
            foreach (var item in diseases)
            {
                s += (statics.map[item] + "\r\n");
            }
            s += ("\r\nsymptoms:\r\n");
            foreach (var item in symptoms)
            {
                s += (statics.map3[item] + "\r\n");
            }
            return s;


        }
    }
}

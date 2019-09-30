using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace ID3
{
    [Serializable()]
    public class symptom:IComparable<symptom>
    {
        private string name;
        //0 CHD
        //1 ASD
        //2 ACD
        //3 EA
        //4 DS
        //5 TIA
        //6 PE
        //7 IAA
        //8 R
        private bool[] disease = new bool[9];
        //value 0 normal
        //value 1 inheritance
        //value 2 mother
        //value 3 IC
        private int category;
        private string question;
        public double[] ic = new double[9];
        public double py { get; set; }
        public double pn { get; set; }
        public double entropy { get; set; }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int Number { get; set; }
        [XmlArray]
        public List<int> Disease
        {
            get
            {
                List<int> retvalue = new List<int>();
                for (int i = 0; i < 9; i++) if (disease[i]) retvalue.Add(i);
                return retvalue;
            }
            set
            {
                for (int i = 0; i < 9; i++) disease[i] = false;
                for (int i = 0; i < value.Count; i++) disease[value[i]] = true;
            }
        }
        public int Category
        {
            get { return category; }
            set { category = value; }
        }
        public string Question
        {
            get { return question; }
            set { question = value; }
        }
        public symptom() { }
        public symptom(int i,string name,List<int> disease,int category,string question)
        {
            Number = i;
            Name = name;
            Disease = disease;
            Category = category;
            Question = question;
            py = ((double)disease.Count/9);
            pn = 1 - py;
        }
        public override string ToString()
        {

            string p="";
            p += ("name: "+Name+"  ");
            p += "\r\nDiseases:\r\n";
            for (int i = 0; i < Disease.Count; i++)
            {
                p += statics.map[Disease[i]];
                p += "\r\n";
            }
            //p += Category.ToString();
            p += "\r\n";
            //p+=(py+"     "+pn+"\r\n");
            p += ("entropy: "+entropy + "\r\n");
            p += "________________________________________________";
            p += "\r\n";
            return p;

        }

        public int CompareTo(symptom other)
        {
            // A null value means that this object is greater.
            if (other == null)
                return 1;

            else
                return (this.entropy.CompareTo(other.entropy));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID3
{
   
    public class decisionTree
    {
        public symptoms AllSymptoms;
        public symptoms allnotchanged;
        public diseases AllDiseases;
        public ices AllIces;
        public Node root;
        public decisionTree(string path1,string path2)
        {
            root = null;
            AllSymptoms = new symptoms();
            AllSymptoms.loadxml(path1);
            allnotchanged = new symptoms();
            allnotchanged.loadxml(path1);
            AllDiseases = new diseases(AllSymptoms);
            AllIces = new ices();
            AllIces.loadxml(path2);
            statics.H(AllDiseases, AllSymptoms);
            statics.H(AllDiseases, allnotchanged);
            AllSymptoms.sort();
        }
      private Node insert(Node par,diseases d,List<symptom> used)
        {
            //Console.WriteLine(d);
            if (d.Count == 1)
            {
                //Console.WriteLine("here"+d);
                //Console.WriteLine("disease\r\n"+d);
                if (!d[0].Isic) return new Node(par,1, d[0]);
                else return new Node(par,2, AllIces.CastToIc(d[0]));
            }
            else
            {
                List<symptom> passused = new List<symptom>();
                passused.AddRange(used);
                diseases passdisyes;
                diseases passdisno;
                symptoms PossibleSymptoms = AllSymptoms.SearchForSymptoms(d, passused);
                statics.H(d, PossibleSymptoms);
                PossibleSymptoms.sort();
                //Console.WriteLine(PossibleSymptoms.Count+"  "+d.Count);
                symptom ThisSymptom = PossibleSymptoms[statics.Ran(PossibleSymptoms)];
                while (ThisSymptom.Disease.Count >= d.Count)
                {
                    int rand = statics.Ran(PossibleSymptoms);
                    //Console.WriteLine(rand);
                    ThisSymptom = PossibleSymptoms[rand+2];
                }
                //symptom ThisSymptom = PossibleSymptoms[0];
                //Console.WriteLine("passs");
                passused.Add(ThisSymptom);
                //Console.WriteLine("symptoms" + d+"_________\r\n"+ThisSymptom);
                //Console.WriteLine("alld"+d.Count);
                passdisyes = d.SearchForDisease(ThisSymptom.Disease);
                passdisno = d.Rest(ThisSymptom.Disease);
                //Console.WriteLine("yesd"+passdisyes.Count+"   nod"+passdisno.Count);
                Node hold = new Node(par,0, ThisSymptom);
                hold.yes = insert(hold,passdisyes,passused);
                hold.no = insert(hold,passdisno,passused);
                return hold;
            }
        }
        public void fillic()
        {
            Queue<Node> Q = new Queue<Node>();
            Q.Enqueue(root);
            Node h;
            while (Q.Count>0)
            {
                h = Q.Dequeue();
                if (h.NodeDef == 0)
                {
                    if (h.yes != null) Q.Enqueue(h.yes);
                    if (h.no != null) Q.Enqueue(h.no);
                }
                if (h != null && h.NodeDef == 2)
                {
                    ic hold = h.d.ic;
                    diseases pasdis = new diseases(AllDiseases,hold.diseases);
                    List<symptom> passym = new List<symptom>();
                    //Console.WriteLine(pasdis.Count);
                    h.ic = insert(h,pasdis, passym);
                }
            }
        }
        public void fill()
        {
            if(root==null)
            {
                List<symptom> start = new List<symptom>();
                root = insert(root,AllDiseases, start);
            }
            //Console.WriteLine("done\n\\n\\n\n\n\\n\n");
            fillic();
        }
        public void clear()
        {
            root = null;
            GC.Collect();
        }
        public void print()
        {
            Queue<Node> Q = new Queue<Node>();
            Q.Enqueue(root);
            Node h;
            while(Q.Count>0)
            {
                h = Q.Dequeue();
                Console.WriteLine(h);
                if (h != null && h.NodeDef == 0) 
                {
                    if (h.yes != null) Q.Enqueue(h.yes);
                    if (h.no != null) Q.Enqueue(h.no);
                }
                if (h != null && h.NodeDef == 2) Q.Enqueue(h.ic);
            }
        }
       
    
        public override string ToString()
        {
            string s = "";
            Queue<Node> Q = new Queue<Node>();
            Q.Enqueue(root);
            Node h;
            while (Q.Count > 0)
            {
                h = Q.Dequeue();
                s += h;
                if (h != null && h.NodeDef == 0)
                {
                    if (h.yes != null) Q.Enqueue(h.yes);
                    if (h.no != null) Q.Enqueue(h.no);
                }
                if (h != null && h.NodeDef == 2) Q.Enqueue(h.ic);
            }
            return s;
        }

    }
}

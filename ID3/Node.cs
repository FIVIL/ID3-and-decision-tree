using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID3
{
    public class Node
    {
        //0 symptom
        //1 diseas
        //2 ic
        public int NodeDef;
        public data d;
        public Node yes;
        public Node no;
        public Node ic;
        public Node par;
        public int X_pos;
        public int Y_pos;
        public bool IsLocationSet;
        public bool Ischecked;
        public Node()
        {
            Ischecked = false;
            NodeDef = 4;
            d = null;
            yes = null;
            no = null;
            ic = null;
            par = null;
            IsLocationSet = false;
        }
        public Node(Node h)
        {
            Ischecked = h.Ischecked;
            NodeDef = h.NodeDef;
            d = h.d;
            yes = new Node(h.yes);
            no = new Node(h.no);
            ic = new Node(h.yes);
            par = new Node(h.ic);
            IsLocationSet = h.IsLocationSet;
        }
        public Node(Node pa,int i,object obj)
        {
            NodeDef = i;
            d = new data(i, obj);
            yes = null;
            no = null;
            ic = null;
            par = pa;
            IsLocationSet = false;
        }
        public string minis()
        {
            string p = "";
            if (NodeDef == 0) p += ("S: " );
            if (NodeDef == 1) p += ("D: ");
            if (NodeDef == 2) p += ("IC: ");
            if (NodeDef == 0) p += (d.s.Name);
            if (NodeDef == 1) p += (d.d.Name);
            if (NodeDef == 2) p += (d.ic.Name);
            if (p.Length > 11) return p.Remove(10, p.Length - 11) + "...";
            else return p + "...";
        }
        public string name()
        {
            string p = "";
            if (NodeDef == 0) p += (d.s.Name);
            if (NodeDef == 1) p += (d.d.Name);
            if (NodeDef == 2) p += (d.ic.Name);
            return p;
        }
        public string fulldetails()
        {
            string p="";
            if (this != null)
            {
                if (NodeDef == 0) p += ("symptom" + "\r\n");
                if (NodeDef == 1) p += ("diseas" + "\r\n");
                if (NodeDef == 2) p += ("ic" + "\r\n");
                if (NodeDef == 0) p += (d.s + "\r\n");
                if (NodeDef == 1) p += (d.d + "\r\n");
                if (NodeDef == 2) p += (d.ic + "\r\n");

            }
            return p;
        }
        public override string ToString()
        {
            string p = string.Empty;
            if (this != null)
            {
                if (NodeDef == 0) p += ("symptom" + "\r\n");
                if (NodeDef == 1) p += ("diseas" + "\r\n");
                if (NodeDef == 2) p += ("ic" + "\r\n");
                if (NodeDef > 2) p += ("null" + "\r\n");
                if (NodeDef == 0) p += (d.s.Name + "\r\n");
                if (NodeDef == 1) p += (d.d.Name + "\r\n");
                if (NodeDef == 2) p += (d.ic.Name + "\r\n");
                if (NodeDef > 2) p += ("null" + "\r\n");
                p += "___________________________________________\r\n";

            }
            return p;
        }
    }
}

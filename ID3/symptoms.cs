using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace ID3
{
    [Serializable]
    public class symptoms : IList<symptom>
    {
        List<symptom> sl = new List<symptom>();
        XmlSerializer xmlsr;
        string p = "";
        #region list
        public symptom this[int index]
        {
            get
            {
                
                return sl[index];
            }

            set
            {

                sl[index] = value;
            }
        }

        public int Count
        {
            get
            {
                return sl.Count;
            }
        }
        

        public bool IsReadOnly
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Add(symptom item)
        {
            sl.Add(item);
        }
        public void add(symptom item)
        {
            loadxml(p);
            sl.Add(item);
            save(p);
        }
        public void Clear()
        {
            sl.Clear();
        }

        public bool Contains(symptom item)
        {
            return sl.Contains(item);
        }

        public void CopyTo(symptom[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<symptom> GetEnumerator()
        {
            return sl.GetEnumerator();
        }

        public int IndexOf(symptom item)
        {
            return sl.IndexOf(item);
        }

        public void Insert(int index, symptom item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(symptom item)
        {
            return sl.Remove(item);
        }
        public bool Remove(List<symptom> s)
        {
            bool flag = false;
            foreach (var item in s)
            {
               if(Contains(item)) flag = Remove(item);
                if (flag) break;
            }
            return flag;
        }
        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return sl.GetEnumerator();
        }
        public void sort()
        {
            sl.Sort();
        }
        #endregion
        public symptoms()
        {
            xmlsr = new XmlSerializer(typeof(symptoms), new XmlRootAttribute(this.GetType().Name));
            statics.map.Clear();
            statics.map.Add(0,"Cyanotic heart disease");
            statics.map.Add(1,"Atrial septal defect");
            statics.map.Add(2,"Atrioventricular canal defect ");
            statics.map.Add(3,"Ebstein's anomaly");
            statics.map.Add(4,"Down Syndrome");
            statics.map.Add(5,"transient ischemic attacks");
            statics.map.Add(6,"Rubella");
            statics.map.Add(7,"Interrupted aortic arch");
            statics.map.Add(8,"Paradoxical embolism");
            statics.map2set();
        }
        public symptoms(List<symptom> l)
        {
            xmlsr = new XmlSerializer(typeof(symptoms), new XmlRootAttribute(this.GetType().Name));
            statics.map.Clear();
            statics.map.Add(0, "Cyanotic heart disease");
            statics.map.Add(1, "Atrial septal defect");
            statics.map.Add(2, "Atrioventricular canal defect ");
            statics.map.Add(3, "Ebstein's anomaly");
            statics.map.Add(4, "Down Syndrome");
            statics.map.Add(5, "transient ischemic attacks");
            statics.map.Add(6, "Rubella");
            statics.map.Add(7, "Interrupted aortic arch");
            statics.map.Add(8, "Paradoxical embolism");
            statics.map2set();
            foreach (var item in l)
            {
                sl.Add(item);
            }
        }
        public symptoms SearchForSymptoms(diseases d, List<symptom> sin)
        {
            //Console.WriteLine("sfs");
            symptoms ret = new symptoms();
            bool flag;
            foreach (var item in this)
            {
                flag = true;
                foreach (var item2 in sin)
                {
                    if (item.Number == item2.Number)
                    {
                        flag = false;
                        break;
                    }
                }
                foreach (var item2 in d)
                {
                    if (item2.symptoms.Contains(item.Number) && !ret.Contains(item) && flag) ret.Add(item);
                }
               
            }
   
            //Console.WriteLine("sfsd");
            return ret;
        }
        public void save(string path)
        {
            using(StreamWriter sw=new StreamWriter(path))
            {
                xmlsr.Serialize(sw, this);
            }
        }
        public void loadxml(string path)
        {
            statics.map3.Clear();
            int i = 0;
            bool nflag = false, qflag = false, dflag = false, cflag = false, iflag = false;
            string name = "", question = "", h = "";
            List<int> disease = new List<int>();
          
            int category = 0;
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;
            XmlReader reader = XmlReader.Create(path, settings);
            reader.MoveToContent();
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.Name == "Name")
                    {
                        nflag = true;
                        qflag = false;
                        dflag = false;
                        cflag = false;
                        iflag = false;
                    }
                    else if (reader.Name == "Question")
                    {
                        nflag = false;
                        qflag = true;
                        dflag = false;
                        cflag = false;
                        iflag = false;
                    }
                    else if (reader.Name == "Category")
                    {
                        nflag = false;
                        qflag = false;
                        dflag = false;
                        cflag = true;
                        iflag = false;
                    }
                    else if (reader.Name == "Disease")
                    {
                        disease.Clear();
                        nflag = false;
                        qflag = false;
                        dflag = true;
                        cflag = false;
                        iflag = false;
                    }
                    else if (reader.Name == "py" || reader.Name == "pn" || reader.Name == "entropy" || reader.Name == "Number") 
                    {
                        nflag = false;
                        qflag = false;
                        dflag = false;
                        cflag = false;
                        iflag = true;
                    }
                }

                else if(reader.NodeType == XmlNodeType.Text)
                {
                    if (nflag) name = reader.Value;
                    if (qflag) question = reader.Value;
                    if (cflag) category = Int32.Parse(reader.Value);
                    if (dflag) disease.Add(Int32.Parse(reader.Value));
                    if (iflag) h = reader.Value;
                }
                else if(reader.NodeType == XmlNodeType.EndElement)
                {
                    if (reader.Name == "symptom") if (disease.Count > 0)
                        {
                            statics.map3.Add(i++, name);
                            this.Add(new symptom(i, name, disease, category, question));
                        }
                }
                
                
            }
            reader.Close();
            reader.Dispose();
        }
        public void loadexcel(string path)
        {
            int z = 0;
            string name="", question="";
            List<int> disease = new List<int>();
            int category=0;
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(path);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;
            int colCount = xlRange.Columns.Count;
            
            Console.WriteLine((xlRange.Cells[6,3].Value!=null).ToString());
            for (int i = 2; i < 63; i++)
            {
                disease.Clear();
                if (xlRange.Cells[i, 1] != null && xlRange.Cells[i, 1].Value2 != null) name = xlRange.Cells[i, 1].Value2;
                if (xlRange.Cells[i, colCount] != null && xlRange.Cells[i, colCount].Value2 != null)
                    question = xlRange.Cells[i, colCount].Value2;
                if (i != 53 && i != 56 && i != 64 && i != 54) 
                {
                    for (int j = 2; j <colCount ; j++)
                        if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                        {
                            if (xlRange.Cells[i, j].Value2 == "yes") disease.Add(j - 2);

                        }
                    if (i < 53) category = 0;
                    else if (i < 56) category = 1;
                    else if (i < 64) category = 2;
                    else category = 3;
                    this.Add(new symptom(z++,name, disease, category,  question));
                }
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
        }
        public override string ToString()
        {
            int i = 1;
            string p = string.Empty;
            foreach(symptom s in this)
            {
                p +=(i+"\r\n"+s.ToString());
                i++;
            }
            return p;
        }
    }
}

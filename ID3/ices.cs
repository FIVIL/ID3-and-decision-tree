using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Excel = Microsoft.Office.Interop.Excel;


namespace ID3
{
    [Serializable]
    public class ices:IList<ic>
    {
        List<ic> sl = new List<ic>();
        XmlSerializer xmlsr;
        #region list
        public ic this[int index]
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

        public void Add(ic item)
        {
            sl.Add(item);
        }
        public void Clear()
        {
            sl.Clear();
        }

        public bool Contains(ic item)
        {
            return sl.Contains(item);
        }

        public void CopyTo(ic[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<ic> GetEnumerator()
        {
            return sl.GetEnumerator();
        }

        public int IndexOf(ic item)
        {
            return sl.IndexOf(item);
        }

        public void Insert(int index, ic item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(ic item)
        {
            return sl.Remove(item);
        }
        public bool Remove(List<ic> s)
        {
            bool flag = false;
            foreach (var item in s)
            {
                flag = Remove(item);
                if (!flag) break;
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
        #endregion
        public ices()
        {
            xmlsr = new XmlSerializer(typeof(ices), new XmlRootAttribute(this.GetType().Name));
        }
        public ic CastToIc(disease d)
        {
            foreach (ic item in sl)
            {
               // Console.WriteLine(item.Name+"    "+d.Name);
                if (item.Name.Length==d.Name.Length) return item;
            }
            Console.WriteLine("null");
            return null;
        }
        public void loadexcel(string path)
        {
            int z = 0;
            string name = "", question = "";
            List<int> disease = new List<int>();
            List<int> symptoms = new List<int>();
            Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(path);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;
            int colCount = xlRange.Columns.Count;
            for (int i = 65; i < 70; i++)
            {
                disease.Clear();
                symptoms.Clear();
                if (xlRange.Cells[i, 1] != null && xlRange.Cells[i, 1].Value2 != null) name = xlRange.Cells[i, 1].Value2;
                if (xlRange.Cells[i, colCount] != null && xlRange.Cells[i, colCount].Value2 != null)
                    question = xlRange.Cells[i, colCount].Value2;
                for (int j = 2; j <= 5; j++)
                {
                    if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                    {
                        if (xlRange.Cells[i, j].Value2 == "yes") disease.Add(j - 2);
                    }
                }
                for (int j = 2; j < 63; j++)
                {
                    if (i != 53 && i != 56 && i != 64)
                    {
                        if (xlRange.Cells[j, i-59] != null && xlRange.Cells[j, i-59].Value2 != null)
                        {
                            if (xlRange.Cells[j, i-59].Value2 == "yes") symptoms.Add(j - 2);
                        }
                    }
                }
                    Add(new ic(z++, name, disease, symptoms, question));
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
        }
        public void save(string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                xmlsr.Serialize(sw, this);
            }
        }
        public void loadxml(string path)
        {
            bool nflag = false, qflag = false, dflag = false, sflag = false, nuflag = false;
            string name = "", question = "";
            int nu = 0;
            List<int> disease = new List<int>();
            List<int> symptom = new List<int>();
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
                        sflag = false;
                        nuflag = false;
                    }
                    else if (reader.Name == "Question")
                    {
                        nflag = false;
                        qflag = true;
                        dflag = false;
                        sflag = false;
                        nuflag = false;
                    }
                    else if (reader.Name == "diseases")
                    {
                        disease.Clear();
                        nflag = false;
                        qflag = false;
                        dflag = true;
                        sflag = false;
                        nuflag = false;
                    }
                    else if (reader.Name == "symptoms")
                    {
                        symptom.Clear();
                        nflag = false;
                        qflag = false;
                        dflag = false;
                        sflag = true;
                        nuflag = false;
                    }
                    else if (reader.Name == "Number")
                    {
                        nflag = false;
                        qflag = false;
                        dflag = false;
                        sflag = false;
                        nuflag = true;
                    }
                }
                else if (reader.NodeType == XmlNodeType.Text)
                {
                    if (nflag) name = reader.Value;
                    if (qflag) question = reader.Value;
                    if (dflag) disease.Add(Int32.Parse(reader.Value));
                    if (sflag) symptom.Add(Int32.Parse(reader.Value));
                    if (nuflag) nu = Int32.Parse(reader.Value);
                }
                else if (reader.NodeType == XmlNodeType.EndElement)
                {
                    if (reader.Name == "ic") Add(new ic(nu,name,disease,symptom,question));
                }


            }
            reader.Close();
            reader.Dispose();
        }
        public override string ToString()
        {
            int i = 0;
            string s=string.Empty;
            foreach (ic item in this)
            {
                s += ((i++) + "." + item.ToString());
                s += "\r\n__________________________________________________________________________________________________\r\n";
            }
            return s;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID3
{
    public class diseases : IList<disease>
    {
        List<disease> sl = new List<disease>();
        #region list
        public disease this[int index]
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

        public void Add(disease item)
        {
            sl.Add(item);
        }
        public void Clear()
        {
            sl.Clear();
        }

        public bool Contains(disease item)
        {
            return sl.Contains(item);
        }

        public void CopyTo(disease[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<disease> GetEnumerator()
        {
            return sl.GetEnumerator();
        }

        public int IndexOf(disease item)
        {
            return sl.IndexOf(item);
        }

        public void Insert(int index, disease item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(disease item)
        {
            return sl.Remove(item);
        }
        public bool Remove(List<disease> s)
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
        public diseases() { }
        public diseases(List<disease> l)
        {
            foreach (var item in l)
            {
                sl.Add(item);
            }
        }
        public diseases(symptoms s)
        {
            int x = 0;
            bool isis = true; ;
            for(int i = 0; i < 9; i++)
            {
                if (i > 3) isis = false;
                sl.Add(new disease(x++, statics.map[i], !isis, s));
            }
        }
        public diseases(diseases d,List<int> d2)
        {
            foreach (var item in d2)
            {
                foreach (var item2 in d)
                {
                    if (item2.Num == item) sl.Add(item2);
                }
            }
        }
        public diseases SearchForDisease(List<int> d)
        {
            diseases retvalue = new diseases();
            foreach (var item in this)
            {
                foreach (var item2 in d)
                {
                    if (item.Num == item2) retvalue.Add(item);
                }
            }
            return retvalue;
        }
        public diseases Rest(List<int> d)
        {
            diseases retvalue = new diseases();
            bool flag;
            foreach (var item in this)
            {
                flag = true;
                foreach (var item2 in d)
                {
                    if (item.Num == item2)
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag) retvalue.Add(item);
            }
            return retvalue;
        }
        public void set(symptoms s)
        {
            sl.Clear();
            int x = 0;
            bool isis = true; ;
            for (int i = 0; i < statics.map.Count; i++)
            {
                if (i > 3) isis = false;
                sl.Add(new disease(x++, statics.map[i], !isis, s));
            }
        }
        public override string ToString()
        {
            int i = 1;
            string p = string.Empty;
            foreach (disease s in this)
            {
                p += (i + "\r\n" + s.ToString());
                i++;
            }
            return p;
        }
    }
}

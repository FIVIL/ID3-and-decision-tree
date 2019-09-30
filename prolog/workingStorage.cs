using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prolog
{
    public class workingStorage
    {
        public List<Predicate> predicates;
        public workingStorage()
        {
            predicates = new List<Predicate>();
        }
        public void asserta(Predicate p)
        {
            predicates.Insert(0, p);
        }
        public void assertz(Predicate p)
        {
            predicates.Add(p);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prolog
{
    public class knowledgeBase
    {
        public List<Predicate> predicates;
        public List<ConditionalPredicate> ConditionalPredicates;
        public knowledgeBase()
        {
            predicates = new List<Predicate>();
            ConditionalPredicates = new List<ConditionalPredicate>();
        }
        public void add(object obj)
        {
            if (obj.GetType() == typeof(Predicate))
            {
                
                Predicate hold = new Predicate((Predicate)obj);
                //Console.WriteLine("pred" + hold.Name);
                predicates.Add(hold);
            }
            else if (obj.GetType() == typeof(ConditionalPredicate))
            {
                ConditionalPredicate hold = new ConditionalPredicate((ConditionalPredicate)obj);
                //Console.WriteLine("cond" + hold.root.Name);
                ConditionalPredicates.Add(hold);
            }
        }
    }
}

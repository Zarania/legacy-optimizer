using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legacy_Optimizer
{
    public class Decks
    {
        public List<Deck> PossibleDecks { get; set; }
    }
    public class Deck
    {
        public IEnumerable<Constraint> Constraints { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public class Constraint
    {
        public string Name { get; set; }
        public int Count { get; set; }
    }

    public class Collection
    {
        public IEnumerable<Constraint> Haves { get; set; }
    }
}

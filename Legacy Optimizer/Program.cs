using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Legacy_Optimizer
{
    class Program
    {
        static void Main(string[] args)
        {
            var collectionString = File.ReadAllText("collection.json");
            var collection = JsonConvert.DeserializeObject<Collection>(collectionString);
            var deckString = File.ReadAllText("decks.json");
            var decks = JsonConvert.DeserializeObject<Decks>(deckString);
            var solutions = PossibleDecks(decks, collection, new LinkedList<Decks>(), 0, new LinkedList<Deck>());

            File.WriteAllLines("decks.txt", solutions.Where(s => s.PossibleDecks.Count >= 8).Select(s => string.Join(", ", s.PossibleDecks)));
        }

        static IEnumerable<Decks> PossibleDecks(Decks decks, Collection collection, LinkedList<Decks> solutions, int place, LinkedList<Deck> chain)
        {
            if (place == decks.PossibleDecks.Count)
            {
                solutions.AddLast(new Decks()
                {
                    PossibleDecks = chain.ToList()
                });

                return solutions;
            }

            if(CanAdd(decks.PossibleDecks[place], chain, collection))
            {
                chain.AddLast(decks.PossibleDecks[place]);
                PossibleDecks(decks, collection, solutions, place + 1, chain);
                chain.RemoveLast();
            }

            PossibleDecks(decks, collection, solutions, place + 1, chain);


            return solutions;
        }

        static bool CanAdd(Deck deck, LinkedList<Deck> chain, Collection collection)
        {
            foreach (var constraint in deck.Constraints)
            {
                var total =
                    chain.SelectMany(d => d.Constraints).Where(c => c.Name == constraint.Name).Sum(c => c.Count) +
                    constraint.Count;

                var have = collection.Haves.FirstOrDefault(c => c.Name == constraint.Name);
                if (have == null || have.Count < total)
                {
                    return false;
                }
            }

            return true;
        }
    }
}

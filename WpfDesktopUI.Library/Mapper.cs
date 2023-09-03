using Logger.Library;
using SqliteDataAccess.Library;
using SqliteDataAccess.Library.DbOperations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WpfDesktopUI.Library.Models;

namespace WpfDesktopUI.Library
{
    public class Mapper
    {
        public static async Task UpdateDatabase()
        {
            await DbDataAccess.UpdateDatabase();
        }

        public static async Task<List<Card>> ToCardDisplay()
        {
            var cards = await SelectStatements.LoadCards();
            await Log.Info("AllCards is ready to be mapped to DisplayObjects");

            var standardMonsters = cards.StandardMonsters.Select(x => new StandardMonster(x));
            var pendulumMonsters = cards.PendulumMonsters.Select(x => new PendulumMonster(x));
            var linkMonsters = cards.LinkMonsters.Select(x => new LinkMonster(x));
            var spells = cards.Spells.Select(x => new Spell(x));
            var traps = cards.Traps.Select(x => new Trap(x));
            var skills = cards.Skills.Select(x => new Skill(x));

            List<Card> result = new();
            result.AddRange(standardMonsters);
            result.AddRange(pendulumMonsters);
            result.AddRange(linkMonsters);
            result.AddRange(spells);
            result.AddRange(traps);
            result.AddRange(skills);

            return result.OrderBy(x => x.Name).ToList();
        }

        public static async Task<IEnumerable<CollectionCard>> ToCollectionCardDisplay()
        {
            var collectionCards = await SelectStatements.LoadCollection();

            List<CollectionCard> result = new (collectionCards.Select(x => new CollectionCard(x)));

            return result;
        }

        public static IEnumerable<string> GetSetNames()
        {
           return SelectStatements.GetSetNames();
        }

        public static IEnumerable<CollectionCard> GetCardsFromSet(string setName)
        {
            var cards = SelectStatements.GetCardsFromSet(setName);

            return cards.Select(x => new CollectionCard(x));
        
        }

    }
}

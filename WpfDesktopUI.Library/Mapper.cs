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

        public static async Task<List<CardDisplay>> ToCardDisplay()
        {
            var cards = await SelectStatements.LoadCards();
            await Log.Info("AllCards is ready to be mapped to DisplayObjects");

            var standardMonsters = cards.StandardMonsters.Select(x => new StandardMonsterDisplay(x));
            var pendulumMonsters = cards.PendulumMonsters.Select(x => new PendulumMonsterDisplay(x));
            var linkMonsters = cards.LinkMonsters.Select(x => new LinkMonsterDisplay(x));
            var spells = cards.Spells.Select(x => new SpellDisplay(x));
            var traps = cards.Traps.Select(x => new TrapDisplay(x));
            var skills = cards.Skills.Select(x => new SkillDisplay(x));

            List<CardDisplay> result = new();
            result.AddRange(standardMonsters);
            result.AddRange(pendulumMonsters);
            result.AddRange(linkMonsters);
            result.AddRange(spells);
            result.AddRange(traps);
            result.AddRange(skills);

            return result.OrderBy(x => x.Name).ToList();
        }

        public static async Task<IEnumerable<CollectionCardDisplay>> ToCollectionCardDisplay()
        {
            var collectionCards = await SelectStatements.LoadCollection();

            List<CollectionCardDisplay> result = new (collectionCards.Select(x => new CollectionCardDisplay(x)));

            return result;
        }

        public static IEnumerable<string> GetSetNames()
        {
           return SelectStatements.GetSetNames();
        }


    }
}

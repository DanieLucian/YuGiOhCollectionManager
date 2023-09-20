using ExternalServices.DbOperations;
using SqliteDataAccess.Library.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WpfDesktopUI.Library.Models;
using Newtonsoft.Json;
using System.IO;
using System.Configuration;

namespace ExternalServices
{
    public class Mapper
    {

        public static async Task UpdateDatabase()
        {
            await DbDataAccess.UpdateDatabase();
        }

        private static StandardMonster ToDomain(StandardMonsterDTO standardMonsterDTO)
        {
            return new StandardMonster(
                   standardMonsterDTO.Name,
                   standardMonsterDTO.Description,
                   standardMonsterDTO.Attribute,
                   standardMonsterDTO.Race,
                   standardMonsterDTO.Type,
                   standardMonsterDTO.Atk,
                   standardMonsterDTO.Def,
                   standardMonsterDTO.LvlRank);
        }

        private static PendulumMonster ToDomain(PendulumMonsterDTO pendulumMonsterDTO)
        {
            return new PendulumMonster(
                   pendulumMonsterDTO.Name,
                   pendulumMonsterDTO.Description,
                   pendulumMonsterDTO.Attribute,
                   pendulumMonsterDTO.Race,
                   pendulumMonsterDTO.Type,
                   pendulumMonsterDTO.Atk,
                   pendulumMonsterDTO.Def,
                   pendulumMonsterDTO.LvlRank,
                   pendulumMonsterDTO.Scale);
        }

        private static LinkMonster ToDomain(LinkMonsterDTO linkMonsterDTO)
        {
            return new LinkMonster(
                   linkMonsterDTO.Name,
                   linkMonsterDTO.Description,
                   linkMonsterDTO.Attribute,
                   linkMonsterDTO.Race,
                   linkMonsterDTO.Type,
                   linkMonsterDTO.Atk,
                   linkMonsterDTO.LinkRating,
                   linkMonsterDTO.LinkArrows);
        }

        private static Spell ToDomain(SpellDTO spellDTO)
        {
            return new Spell(spellDTO.Name, spellDTO.Description, spellDTO.Icon);
        }

        private static Trap ToDomain(TrapDTO trapDTO)
        {
            return new Trap(trapDTO.Name, trapDTO.Description, trapDTO.Icon);
        }

        private static Skill ToDomain(SkillDTO skillDTO)
        {
            return new Skill(skillDTO.Name, skillDTO.Description, skillDTO.Character);
        }

        private static CollectionCardDTO ToDomain(CollectionCard collectionCard)
        {
            return new CollectionCardDTO(
                   collectionCard.SetId,
                   collectionCard.CardId,
                   collectionCard.RarityName,
                   collectionCard.Quantity);
        }

        public static async Task<List<Card>> ToCardDisplay()
        {
            var cards = await SelectStatements.LoadCards();

            var standardMonsters = cards.StandardMonsterDTOs.Select(ToDomain);
            var pendulumMonsters = cards.PendulumMonsterDTOs.Select(ToDomain);
            var linkMonsters = cards.LinkMonsterDTOs.Select(ToDomain);
            var spells = cards.SpellDTOs.Select(ToDomain);
            var traps = cards.TrapDTOs.Select(ToDomain);
            var skills = cards.SkillDTOs.Select(ToDomain);

            List<Card> result = new();
            result.AddRange(standardMonsters);
            result.AddRange(pendulumMonsters);
            result.AddRange(linkMonsters);
            result.AddRange(spells);
            result.AddRange(traps);
            result.AddRange(skills);

            return result.OrderBy(x => x.Name).ToList();
        }

        public static IEnumerable<string> GetSetNames()
        {
            return SelectStatements.GetSetNames();
        }

        public static IEnumerable<CollectionCard> GetCardsFromSet(string setName)
        {
            var cards = SelectStatements.GetCardsFromSet(setName);

            return cards;
        }

        public static async Task<IEnumerable<CollectionCard>> GetCollection()
        {
            return await SelectStatements.LoadCollection();
        }

        public static async Task<int> UpdateCardQuantity(IEnumerable<CollectionCard> nonZeroQty)
        {
            var nonZeroQtyDTOs = nonZeroQty.Select(ToDomain);

            return await UpdateStatements.UpdateCardQuantity(nonZeroQtyDTOs);
        }
        
        public static void SerializeCollection(IEnumerable<CollectionCard> nonZeroQty)
        {
            var aaa = JsonConvert.SerializeObject(nonZeroQty, Formatting.Indented);
            var Path = ConfigurationManager.AppSettings["logPath"];

            using (StreamWriter streamWriter = new(Path, append: false))
            {
                streamWriter.Write(aaa);
            }
        }
    }
}

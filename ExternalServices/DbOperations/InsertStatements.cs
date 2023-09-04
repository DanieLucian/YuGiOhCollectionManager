using ApiDataAccess.Library.Models;
using ApiDataAccess.Library.Models.Monsters;
using ApiDataAccess.Library.Models.NonMonsters;
using Dapper;
using SqliteDataAccess.Library.HelperTableDTOs;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ExternalServices.DbOperations
{
    internal static class InsertStatements
    {

        internal static async Task InsertIntoCardSet(IDbConnection connection, IDbTransaction transaction, CardModel card, HelperData helperData)
        {
            string query = string.Join(
                           Environment.NewLine,
                           "INSERT OR IGNORE INTO CardSet (CardId, SetId, Rarity, RarityCode)",
                           "VALUES (@CardId, @SetId, @Rarity, @RarityCode);");

            foreach (var set in card.SetInfo)
            {
                var valuesToInsert = new
                {
                    CardId = card.Id,
                    SetId = helperData.Sets
                                                           .FirstOrDefault(s => s.Name
                                                                                 .Equals(set.SetName, StringComparison.OrdinalIgnoreCase) &&
                                                                                s.SetCode
                                                                                 .Equals(set.SetCode.Split('-')[0], StringComparison.OrdinalIgnoreCase))
                                                           .Id,
                    Rarity = set.RarityName,
                    set.RarityCode
                };

                await connection.ExecuteAsync(query, valuesToInsert, transaction: transaction);
            }


        }

        internal static async Task InsertIntoSet(IDbConnection connection, IDbTransaction transaction, SetModel set)
        {
            string query = string.Join(
                           Environment.NewLine,
                           "INSERT OR IGNORE INTO [Set] (Name, SetCode)",
                           "VALUES (@Name, @SetCode);");

            var valuesToInsert = new
            {
                Name = set.SetName,
                set.SetCode
            };

            await connection.ExecuteAsync(query, valuesToInsert, transaction: transaction);
        }

        internal static async Task<int> InsertIntoCard(IDbConnection connection, IDbTransaction transaction, CardModel card)
        {
            string query = string.Join(
                           Environment.NewLine,
                           "INSERT OR IGNORE INTO Card (Id, Name, Description)",
                           "VALUES (@Id, @Name, @Description);");

            var valuesToInsert = new
            {
                card.Id,
                card.Name,
                card.Description
            };

            return await connection.ExecuteAsync(query, valuesToInsert, transaction: transaction);
        }

        internal static async Task InsertIntoCardType(IDbConnection connection, IDbTransaction transaction, MonsterModel card, HelperData helperData)
        {
            string query = string.Join(
                           Environment.NewLine,
                           "INSERT INTO CardType (CardId, TypeId)",
                           "VALUES (@CardId, @TypeId)");

            var valuesToInsert = card.Type
                                     .Select(type => new
                                     {
                                         CardId = card.Id,
                                         TypeId = helperData.Types.First(t => t.Name == type).Id
                                     });

            await connection.ExecuteAsync(query, valuesToInsert, transaction: transaction);
        }

        internal static async Task InsertIntoLinkMonster(IDbConnection connection, IDbTransaction transaction, LinkMonsterModel card)
        {
            string query = string.Join(
                           Environment.NewLine,
                           "INSERT INTO LinkMonster (CardId, LinkRating)",
                           "VALUES (@CardId, @LinkRating)");

            var valuesToInsert = new
            {
                CardId = card.Id,
                card.LinkRating
            };

            await connection.ExecuteAsync(query, valuesToInsert, transaction: transaction);
        }

        internal static async Task InsertIntoLinkMonsterLinkArrow(IDbConnection connection, IDbTransaction transaction, LinkMonsterModel card, HelperData helperData)
        {
            string query = string.Join(
                           Environment.NewLine,
                           "INSERT INTO LinkMonsterLinkArrow (CardId, LinkArrowId)",
                           "VALUES (@CardId, @LinkArrowId)");

            var valuesToInsert = card.LinkArrows
                                     .Select(direction => new
                                     {
                                         CardId = card.Id,
                                         LinkArrowId = helperData.LinkArrows.First(l => l.Direction == direction).Id
                                     });

            await connection.ExecuteAsync(query, valuesToInsert, transaction: transaction);
        }

        internal static async Task InsertIntoMonster(IDbConnection connection, IDbTransaction transaction, MonsterModel card, HelperData helperData)
        {
            string query = string.Join(
                           Environment.NewLine,
                           "INSERT INTO Monster (CardId, AttributeId, RaceId, Atk, Def)",
                           "VALUES (@CardId, @AttributeId, @RaceId, @Atk, @Def)");

            var valuesToInsert = new
            {
                CardId = card.Id,

                AttributeId = helperData.Attributes.First(a => a.Name == card.Attribute).Id,

                RaceId = helperData.Races.First(r => r.Name == card.Race).Id,

                Atk = card.ExtraInfo[0].HasQuestionAtk ? "?" : card.Atk,

                Def = card.ExtraInfo[0].HasQuestionDef ? "?" : card.Def,
            };

            await connection.ExecuteAsync(query, valuesToInsert, transaction: transaction);
        }

        internal static async Task InsertIntoPendulumMonster(IDbConnection connection, IDbTransaction transaction, PendulumMonsterModel card)
        {
            string query = string.Join(
                           Environment.NewLine,
                           "INSERT INTO PendulumMonster (CardId, Scale)",
                           "VALUES (@CardId, @Scale)");

            var valuesToInsert = new
            {
                CardId = card.Id,
                card.Scale
            };

            await connection.ExecuteAsync(query, valuesToInsert, transaction: transaction);
        }

        internal static async Task InsertIntoSkill(IDbConnection connection, IDbTransaction transaction, SkillModel card)
        {
            string query = string.Join(
                           Environment.NewLine,
                           "INSERT INTO Skill (CardId, Character)",
                           "VALUES (@CardId, @Character)");

            var valuesToInsert = new
            {
                CardId = card.Id,
                card.Character
            };

            await connection.ExecuteAsync(query, valuesToInsert, transaction: transaction);
        }

        internal static async Task InsertIntoSpell(IDbConnection connection, IDbTransaction transaction, SpellModel card, HelperData helperData)
        {
            string query = string.Join(
                           Environment.NewLine,
                           "INSERT INTO Spell (CardId, SpellIconId)",
                           "VALUES (@CardId, @SpellIconId)");

            var valuesToInsert = new
            {
                CardId = card.Id,
                SpellIconId = helperData.SpellIcons.FirstOrDefault(s => s.Name == card.SpellIcon).Id
            };

            await connection.ExecuteAsync(query, valuesToInsert, transaction: transaction);
        }

        internal static async Task InsertIntoStandardMonster(IDbConnection connection, IDbTransaction transaction, StandardMonsterModel card)
        {
            string query = string.Join(
                           Environment.NewLine,
                           "INSERT INTO StandardMonster (CardId, LvlRank)",
                           "VALUES (@CardId, @LvlRank)");

            var valuesToInsert = new
            {
                CardId = card.Id,
                card.LvlRank
            };

            await connection.ExecuteAsync(query, valuesToInsert, transaction: transaction);

            if (card is PendulumMonsterModel pendulumMonster)
            {
                await InsertIntoPendulumMonster(connection, transaction, pendulumMonster);
            }
        }

        internal static async Task InsertIntoTrap(IDbConnection connection, IDbTransaction transaction, TrapModel card, HelperData helperData)
        {
            string query = string.Join(
                           Environment.NewLine,
                           "INSERT INTO Trap (CardId, TrapIconId)",
                           "VALUES (@CardId, @TrapIconId)");

            var valuesToInsert = new
            {
                CardId = card.Id,
                TrapIconId = helperData.TrapIcons.FirstOrDefault(t => t.Name == card.TrapIcon).Id
            };

            await connection.ExecuteAsync(query, valuesToInsert, transaction: transaction);
        }

    }
}

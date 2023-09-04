using ApiDataAccess.Library.Models.NonMonsters;
using Dapper;
using ExternalServices.Helpers;
using SqliteDataAccess.Library.DTOs;
using SqliteDataAccess.Library.HelperTableDTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using WpfDesktopUI.Library.Models;

namespace ExternalServices.DbOperations
{
    internal class SelectStatements
    {
        public static IEnumerable<CollectionCard> GetCardsFromSet(string setName)
        {
            using (IDbConnection connection = new SQLiteConnection(DbHelper.GetConnectionString("YgoTest")))
            {
                string query = string.Join(
                               Environment.NewLine,
                               "SELECT",
                               "\t[Set].Id as SetId,",
                               "\t[Set].SetCode,",
                               "\tCard.Id as CardId,",
                               "\tCard.Name as CardName,",
                               "\tCardSet.Rarity as RarityName,",
                               "\tCardSet.RarityCode,",
                               "\t0 as Quantity",
                               "FROM Card",
                               "\tJOIN CardSet on CardSet.CardId = Card.Id",
                               "\tJOIN [Set] on [Set].Id = CardSet.SetId",
                               "WHERE [Set].Name = @setName;");

                var results = connection.Query<CollectionCard>(query, new { setName });

                return results;
            }
        }

        public static IEnumerable<string> GetSetNames()
        {
            using (IDbConnection connection = new SQLiteConnection(DbHelper.GetConnectionString("YgoTest")))
            {
                string query = string.Join(
                               Environment.NewLine,
                               "SELECT DISTINCT Name FROM [Set];");

                var results = connection.Query<string>(query);

                return results;
            }
        }

        public static async Task<HashSet<T>> GetIds<T>(string query)
        {
            using (IDbConnection connection = new SQLiteConnection(DbHelper.GetConnectionString("YgoTest")))
            {
                var results = await connection.QueryAsync<T>(query);

                return new HashSet<T>(results);
            }
        }

        public static async Task<CardDTOs> LoadCards()
        {
            using (IDbConnection connection = new SQLiteConnection(DbHelper.GetConnectionString("YgoTest")))
            {
                string query = string.Join(
                               Environment.NewLine,
                               "SELECT * FROM AllStandardMonsters;",
                               "SELECT * FROM AllPendulumMonsters;",
                               "SELECT * FROM AllLinkMonsters;",
                               "SELECT * FROM AllSpells;",
                               "SELECT * FROM AllTraps;",
                               "SELECT * FROM AllSkills;");

                using (var results = await connection.QueryMultipleAsync(query))
                {
                    SqlMapper.AddTypeHandler(new StringArrayTypeHandler());

                    var cards = new CardDTOs
                    {
                        StandardMonsterDTOs = await results.ReadAsync<StandardMonsterDTO>(),
                        PendulumMonsterDTOs = await results.ReadAsync<PendulumMonsterDTO>(),
                        LinkMonsterDTOs = await results.ReadAsync<LinkMonsterDTO>(),
                        SpellDTOs = await results.ReadAsync<SpellDTO>(),
                        TrapDTOs = await results.ReadAsync<TrapDTO>(),
                        SkillDTOs = await results.ReadAsync<SkillDTO>(),
                    };

                    return cards;
                }
            }
        }

        public static async Task<IEnumerable<CollectionCard>> LoadCollection()
        {
            using (IDbConnection connection = new SQLiteConnection(DbHelper.GetConnectionString("YgoTest")))
            {
                string query = string.Join(
                               Environment.NewLine,
                               "SELECT * FROM Collection LIMIT 20;");

                var results = await connection.QueryAsync<CollectionCard>(query);

                return results.OrderBy(x => x.CardName);
            }
        }

        public static async Task<HelperData> GetHelperDataAsync(IDbConnection connection)
        {
            string query = string.Join(
                           Environment.NewLine,
                           "SELECT * FROM Attribute;",
                           "SELECT * FROM Race;",
                           "SELECT * FROM Type;",
                           "SELECT * FROM LinkArrow;",
                           "SELECT * FROM SpellIcon;",
                           "SELECT * FROM TrapIcon;",
                           "SELECT * FROM [Set];");

            using (var helperLists = await connection.QueryMultipleAsync(query))
            {
                return new HelperData
                {
                    Attributes = (await helperLists.ReadAsync<AttributeDTO>()).ToList(),
                    Races = (await helperLists.ReadAsync<RaceModelDTO>()).ToList(),
                    Types = (await helperLists.ReadAsync<TypeDTO>()).ToList(),
                    LinkArrows = (await helperLists.ReadAsync<LinkArrowModel>()).ToList(),
                    SpellIcons = (await helperLists.ReadAsync<SpellIconDTO>()).ToList(),
                    TrapIcons = (await helperLists.ReadAsync<TrapIconDTO>()).ToList(),
                    Sets = (await helperLists.ReadAsync<FullSetDTO>()).ToList(),
                };
            }
        }

    }
}

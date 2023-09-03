using ApiDataAccess.Library.Helpers;
using Dapper;
using Logger.Library;
using SqliteDataAccess.Library.DTOs;
using SqliteDataAccess.Library.Helpers;
using SqliteDataAccess.Library.HelperTableDTOs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace SqliteDataAccess.Library.DbOperations
{
    public class SelectStatements
    {
        public static IEnumerable<CollectionCardDTO> GetCardsFromSet(string setName)
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

                var results = connection.Query<CollectionCardDTO>(query, new { setName });

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
                                    StandardMonsters = await results.ReadAsync<StandardMonsterDTO>(),
                                    PendulumMonsters = await results.ReadAsync<PendulumMonsterDTO>(),
                                    LinkMonsters = await results.ReadAsync<LinkMonsterDTO>(),
                                    Spells = await results.ReadAsync<SpellModel>(),
                                    Traps = await results.ReadAsync<TrapDTO>(),
                                    Skills = await results.ReadAsync<SkillDTO>(),
                                };

                    await Log.Info("AllCards has been extracted from DB!");
                    return cards;
                }
            }
        }

        public static async Task<IEnumerable<CollectionCardDTO>> LoadCollection()
        {
            using (IDbConnection connection = new SQLiteConnection(DbHelper.GetConnectionString("YgoTest")))
            {
                string query = string.Join(
                               Environment.NewLine,
                               "SELECT * FROM Collection LIMIT 20;");

                var results = await connection.QueryAsync<CollectionCardDTO>(query);

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

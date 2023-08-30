using ApiDataAccess.Library.Helpers;
using ApiDataAccess.Library.Models;
using Dapper;
using Logger.Library;
using SqliteDataAccess.Library.Helpers;
using SqliteDataAccess.Library.HelperTableModels;
using SqliteDataAccess.Library.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace SqliteDataAccess.Library.DbOperations
{
    public class SelectStatements
    {

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

        public static async Task<CardModels> LoadCards()
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

                    var cards = new CardModels
                                {
                                    StandardMonsters = await results.ReadAsync<StandardMonsterModel>(),
                                    PendulumMonsters = await results.ReadAsync<PendulumMonsterModel>(),
                                    LinkMonsters = await results.ReadAsync<LinkMonsterModel>(),
                                    Spells = await results.ReadAsync<SpellModel>(),
                                    Traps = await results.ReadAsync<TrapModel>(),
                                    Skills = await results.ReadAsync<SkillModel>(),
                                };

                    await Log.Info("AllCards has been extracted from DB!");
                    return cards;
                }
            }
        }

        public static async Task<IEnumerable<CollectionCardModel>> LoadCollection()
        {
            using (IDbConnection connection = new SQLiteConnection(DbHelper.GetConnectionString("YgoTest")))
            {
                string query = string.Join(
                               Environment.NewLine,
                               "SELECT * FROM Collection LIMIT 20;");

                var results = await connection.QueryAsync<CollectionCardModel>(query);

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
                           Attributes = (await helperLists.ReadAsync<AttributeModel>()).ToList(),
                           Races = (await helperLists.ReadAsync<RaceModel>()).ToList(),
                           Types = (await helperLists.ReadAsync<TypeModel>()).ToList(),
                           LinkArrows = (await helperLists.ReadAsync<LinkArrowModel>()).ToList(),
                           SpellIcons = (await helperLists.ReadAsync<SpellIconModel>()).ToList(),
                           TrapIcons = (await helperLists.ReadAsync<TrapIconModel>()).ToList(),
                           Sets = (await helperLists.ReadAsync<FullSetModel>()).ToList(),
                       };
            }
        }

    }
}

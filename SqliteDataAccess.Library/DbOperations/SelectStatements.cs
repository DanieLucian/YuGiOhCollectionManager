using Dapper;
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
    public static class SelectStatements
    {

        public static CardModels LoadCards()
        {
            using (IDbConnection connection = new SQLiteConnection(DbDataAccess.GetConnectionString("YgoTest")))
            {
                string query = string.Join(
                               Environment.NewLine,
                               "SELECT * FROM AllStandardMonsters;",
                               "SELECT * FROM AllPendulumMonsters;",
                               "SELECT * FROM AllLinkMonsters;",
                               "SELECT * FROM AllSpells;",
                               "SELECT * FROM AllTraps;",
                               "SELECT * FROM AllSkills;");

                using (var results = connection.QueryMultipleAsync(query).Result)
                {
                    SqlMapper.AddTypeHandler(new StringArrayTypeHandler());

                    var cards = new CardModels
                                {
                                    StandardMonsters = results.ReadAsync<StandardMonsterModel>().Result,
                                    PendulumMonsters = results.ReadAsync<PendulumMonsterModel>().Result,
                                    LinkMonsters = results.ReadAsync<LinkMonsterModel>().Result,
                                    Spells = results.ReadAsync<SpellModel>().Result,
                                    Traps = results.ReadAsync<TrapModel>().Result,
                                    Skills = results.ReadAsync<SkillModel>().Result,
                                };

                    return cards;
                }
            }
        }

        internal static async Task<HelperData> GetHelperDataAsync(IDbConnection connection)
        {
            string query = string.Join(
                           Environment.NewLine,
                           "SELECT * FROM Attribute;",
                           "SELECT * FROM Race;",
                           "SELECT * FROM Type;",
                           "SELECT * FROM LinkArrow;",
                           "SELECT * FROM SpellIcon;",
                           "SELECT * FROM TrapIcon;");

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
                       };
            }
        }

    }
}

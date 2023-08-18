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

        public static async Task<CardModels> LoadCards()
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

using ApiDataAccess.Library;
using ApiDataAccess.Library.Models;
using ApiDataAccess.Library.Models.Monsters;
using ApiDataAccess.Library.Models.NonMonsters;
using SqliteDataAccess.Library.DbOperations;
using SqliteDataAccess.Library.HelperTableModels;
using System;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace SqliteDataAccess.Library
{
    public static class DbDataAccess
    {

        public static async Task InsertOrUpdateCards()
        {
            var jsonData = await CardProcessor.GetCardsAsync();

            if (jsonData.Any() == false)
            {
                Console.WriteLine("No new cards found! The database is up to date");
                return;
            }

            using (IDbConnection connection = new SQLiteConnection(GetConnectionString("YgoTest")))
            {
                HelperData helperData = await SelectStatements.GetHelperDataAsync(connection);

                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    foreach (var card in jsonData)
                    {
                        if (await InsertCard(connection, transaction, card) == 0)
                        {
                            await UpdateStatements.UpdateCard(connection, transaction, card);
                        }

                        else
                        {
                            switch (card)
                            {
                                case Monster monster:
                                    {
                                        await InsertMonster(connection, transaction, monster, helperData);
                                        break;
                                    }

                                case Spell spell:
                                    {
                                        await InsertSpell(connection, transaction, spell, helperData);
                                        break;
                                    }

                                case Trap trap:
                                    {
                                        await InsertTrap(connection, transaction, trap, helperData);
                                        break;
                                    }
                                case Skill skill:
                                    {
                                        await InsertSkill(connection, transaction, skill);
                                        break;
                                    }
                            }
                        }
                    }

                    await Task.Run(transaction.Commit);
                }
            }
        }

        public static string GetConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        private static async Task InsertMonster(IDbConnection connection, IDbTransaction transaction, Monster card, HelperData helperData)
        {
            await InsertStatements.InsertIntoMonster(connection, transaction, card, helperData);
            await InsertStatements.InsertIntoCardType(connection, transaction, card, helperData);

            switch (card)
            {
                case StandardMonster standardMonster:
                    {
                        await InsertStatements.InsertIntoStandardMonster(connection, transaction, standardMonster);
                        break;
                    }

                case LinkMonster linkMonster:
                    {
                        await InsertStatements.InsertIntoLinkMonster(connection, transaction, linkMonster);
                        await InsertStatements.InsertIntoLinkMonsterLinkArrow
                              (connection, transaction, linkMonster, helperData);
                        break;
                    }

                default:
                    break;
            }
        }

        private static async Task<int> InsertCard(IDbConnection connection, IDbTransaction transaction, Card card)
        {
            return await InsertStatements.InsertIntoCard(connection, transaction, card);
        }

        private static async Task InsertSpell(IDbConnection connection, IDbTransaction transaction, Spell card, HelperData helperData)
        {
            await InsertStatements.InsertIntoSpell(connection, transaction, card, helperData);
        }

        private static async Task InsertTrap(IDbConnection connection, IDbTransaction transaction, Trap card, HelperData helperData)
        {
            await InsertStatements.InsertIntoTrap(connection, transaction, card, helperData);
        }

        private static async Task InsertSkill(IDbConnection connection, IDbTransaction transaction, Skill card)
        {
            await InsertStatements.InsertIntoSkill(connection, transaction, card);
        }



    }
}

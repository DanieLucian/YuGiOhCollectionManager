using ApiDataAccess.Library.Models.Monsters;
using ApiDataAccess.Library.Models.NonMonsters;
using ApiDataAccess.Library.Models;
using Logger.Library;
using SqliteDataAccess.Library.DbOperations;
using SqliteDataAccess.Library.HelperTableModels;
using SqliteDataAccess.Library.Models;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;

namespace ApiDataAccess.Library.Helpers
{
    internal class DbHelper
    {

        public static string GetConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        /// <summary>
        ///  Returns a List of cards that are either not in the Database (no ID match) OR  they are in the database
        ///  under a different Name or Description (ID match, different INFO).
        /// </summary>
        /// <returns></returns>
        private static async Task<IEnumerable<Card>> GetNewCards(IEnumerable<Card> jsonData)
        {
            var oldData = await SelectStatements.GetIds<FullCardModel>("SELECT * FROM [Card];");

            var matchingData = from oldCard in oldData
                               join newCard in jsonData
                               on new { oldCard.Id, oldCard.Name, oldCard.Description }
                           equals new { newCard.Id, newCard.Name, newCard.Description }
                               select newCard;

            var newData = jsonData.Except(matchingData);

            return newData;
        }

        private static async Task<IEnumerable<Set>> GetNewSets(IEnumerable<Set> jsonData)
        {
            // var jsonData = (await DataProcessor.GetSetsAsync()).ToList();

            var oldData = await SelectStatements.GetIds<FullSetModel>("SELECT * FROM [Set];");

            var matchingData = from oldSet in oldData
                               join newSet in jsonData
                               on new { SetName = oldSet.Name, oldSet.SetCode }
                           equals new { newSet.SetName, newSet.SetCode }
                               select newSet;

            var newData = jsonData.Except(matchingData);

            return newData;
        }

        private static async Task<int> InsertCard(IDbConnection connection, IDbTransaction transaction, Card card)
        {
            return await InsertStatements.InsertIntoCard(connection, transaction, card);
        }

        private static async Task InsertMonster(IDbConnection connection, IDbTransaction transaction, Monster card, HelperData helperData)
        {
            await InsertStatements.InsertIntoMonster(connection, transaction, card, helperData);
            await InsertStatements.InsertIntoCardType(connection, transaction, card, helperData);

            switch (card)
            {
                case StandardMonster standardMonster :
                {
                    await InsertStatements.InsertIntoStandardMonster(connection, transaction, standardMonster);
                    break;
                }

                case LinkMonster linkMonster :
                {
                    await InsertStatements.InsertIntoLinkMonster(connection, transaction, linkMonster);
                    await InsertStatements.InsertIntoLinkMonsterLinkArrow(
                          connection,
                          transaction,
                          linkMonster,
                          helperData);
                    break;
                }

                default :
                    break;
            }
        }

        public static async Task InsertOrUpdateCards(IEnumerable<Card> jsonData)
        {
            var newCards = (await GetNewCards(jsonData)).ToList();

            if (newCards.Count > 0)
            {
                using (IDbConnection connection = new SQLiteConnection(GetConnectionString("YgoTest")))
                {
                    var helperData = await SelectStatements.GetHelperDataAsync(connection);
                    await Log.Info("HelperData has been extracted from Database");

                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        foreach (var card in newCards)
                        {
                            await InsertStatements.InsertIntoCardSet(connection, transaction, card, helperData);

                            if (await InsertCard(connection, transaction, card) == 0)
                            {
                                await UpdateStatements.UpdateCard(connection, transaction, card);
                            }

                            else
                            {
                                switch (card)
                                {
                                    case Monster monster :
                                    {
                                        await InsertMonster(connection, transaction, monster, helperData);
                                        break;
                                    }

                                    case Spell spell :
                                    {
                                        await InsertSpell(connection, transaction, spell, helperData);
                                        break;
                                    }

                                    case Trap trap :
                                    {
                                        await InsertTrap(connection, transaction, trap, helperData);
                                        break;
                                    }

                                    case Skill skill :
                                    {
                                        await InsertSkill(connection, transaction, skill);
                                        break;
                                    }
                                }
                            }
                        }

                        await Log.Info("Transaction for insertion is ready for commit!");
                        await Task.Run(transaction.Commit);

                        // transaction.Commit();
                        await Log.Info("Transaction for insertion has been commited");
                    }
                }
            }
        }

        public static async Task InsertSets(IEnumerable<Set> jsonData)
        {
            var newSets = (await GetNewSets(jsonData)).ToList();

            if (newSets.Count > 0)
            {
                using (IDbConnection connection = new SQLiteConnection(GetConnectionString("YgoTest")))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        foreach (var set in newSets)
                        {
                            await InsertStatements.InsertIntoSet(connection, transaction, set);
                        }

                        await Task.Run(transaction.Commit);
                    }
                }
            }
        }

        private static async Task InsertSkill(IDbConnection connection, IDbTransaction transaction, Skill card)
        {
            await InsertStatements.InsertIntoSkill(connection, transaction, card);
        }

        private static async Task InsertSpell(IDbConnection connection, IDbTransaction transaction, Spell card, HelperData helperData)
        {
            await InsertStatements.InsertIntoSpell(connection, transaction, card, helperData);
        }

        private static async Task InsertTrap(IDbConnection connection, IDbTransaction transaction, Trap card, HelperData helperData)
        {
            await InsertStatements.InsertIntoTrap(connection, transaction, card, helperData);
        }

    }
}

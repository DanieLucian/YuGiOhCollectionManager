using ApiDataAccess.Library;
using ApiDataAccess.Library.Models;
using ApiDataAccess.Library.Models.Monsters;
using ApiDataAccess.Library.Models.NonMonsters;
using Dapper;
using Logger.Library;
using SqliteDataAccess.Library.DbOperations;
using SqliteDataAccess.Library.HelperTableModels;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace SqliteDataAccess.Library;

public static class DbDataAccess
{

    private static readonly string[] Formats = { "TCG", "Speed Duel" };

    public static async Task InsertOrUpdateCards()
    {
        var jsonData = (await CardProcessor.GetCardsAsync()).ToList();
        jsonData.RemoveAll(x => x == null || x.ExtraInfo[0].Formats.Intersect(Formats).Count() == 0);

        var standard1 = jsonData.OfType<StandardMonster>().Count();
        var pendulum1 = jsonData.OfType<PendulumMonster>().Count();
        var link1 = jsonData.OfType<LinkMonster>().Count();
        var spell1 = jsonData.OfType<Spell>().Count();
        var trap1 = jsonData.OfType<Trap>().Count();
        var skill1 = jsonData.OfType<Skill>().Count();

        var cardIds = (SelectStatements.GetCardIds()).AsList();

        var newData = jsonData.Where(obj => !cardIds.Contains(obj.Id)).AsList();

        if (newData.Count() > 0)
        {
            using (IDbConnection connection = new SQLiteConnection(GetConnectionString("YgoTest")))
            {
                HelperData helperData = await SelectStatements.GetHelperDataAsync(connection);
                await Log.Info("HelperData has been extracted from Database");

                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    foreach (var card in newData)
                    {
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
            case StandardMonster standardMonster :
            {
                await InsertStatements.InsertIntoStandardMonster(connection, transaction, standardMonster);
                break;
            }

            case LinkMonster linkMonster :
            {
                await InsertStatements.InsertIntoLinkMonster(connection, transaction, linkMonster);
                await InsertStatements.InsertIntoLinkMonsterLinkArrow(connection, transaction, linkMonster, helperData);
                break;
            }

            default :
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

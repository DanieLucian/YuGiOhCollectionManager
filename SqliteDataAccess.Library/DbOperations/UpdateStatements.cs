using ApiDataAccess.Library.Helpers;
using ApiDataAccess.Library.Models;
using Dapper;
using System;
using System.Collections;
using System.Data;
using System.Data.SQLite;
using System.Threading.Tasks;

namespace SqliteDataAccess.Library.DbOperations
{
    internal static class UpdateStatements
    {
        internal static async Task UpdateCard(IDbConnection connection, IDbTransaction transaction, Card card)
        {
            string query = @"UPDATE Card 
                            SET
                                Name = @Name,
                                Description = @Description
                           	WHERE Id = @Id;";
            var values = new
            {
                card.Id,
                card.Name,
                card.Description
            };

            await connection.ExecuteAsync(query, values, transaction: transaction);
        }

        internal static async Task UpdateCardQuantity()
        {
            using (IDbConnection connection = new SQLiteConnection(DbHelper.GetConnectionString("YgoTest")))
            {
                string query = string.Join(
                               Environment.NewLine,
                               "UPDATE CardSet",
                               "SET Quantity = Quantity + @Quantity",
                               "WHERE SetId = @SetId && CardId = CardId && Rarity = @RarityName");

                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    await connection.ExecuteAsync(query);

                    transaction.Commit();
                }
            }
        }

    }
}

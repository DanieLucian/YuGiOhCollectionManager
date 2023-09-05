using ApiDataAccess.Library.Models;
using Dapper;
using ExternalServices.Helpers;
using SqliteDataAccess.Library.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace ExternalServices.DbOperations
{
    internal static class UpdateStatements
    {
        internal static async Task UpdateCard(IDbConnection connection, IDbTransaction transaction, CardModel card)
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

        internal static async Task<int> UpdateCardQuantity(IEnumerable<CollectionCardDTO> nonZeroQtyDTOs)
        {
            using (IDbConnection connection = new SQLiteConnection(DbHelper.GetConnectionString("YgoTest")))
            {

                string query = string.Join(
                               Environment.NewLine,
                               "UPDATE CardSet",
                               "SET Quantity = Quantity + @Quantity",
                               "WHERE SetId = @SetId AND CardId = @CardId AND Rarity = @RarityName");

                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    foreach (var card in nonZeroQtyDTOs)
                    {
                        await connection.ExecuteAsync(
                              query,
                              new
                              {
                                  card.SetId,
                                  card.CardId,
                                  card.RarityName,
                                  card.Quantity
                              },
                              transaction);
                    }

                    transaction.Commit();

                    return nonZeroQtyDTOs.Count();
                }
            }
        }

    }
}

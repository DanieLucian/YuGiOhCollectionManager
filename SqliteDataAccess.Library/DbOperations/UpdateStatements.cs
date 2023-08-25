using ApiDataAccess.Library.Models;
using Dapper;
using System.Data;
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
    }
}

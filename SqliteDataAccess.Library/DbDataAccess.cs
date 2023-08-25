using ApiDataAccess.Library;
using ApiDataAccess.Library.Helpers;
using ApiDataAccess.Library.Models;
using SqliteDataAccess.Library.Helpers;
using System.Linq;
using System.Threading.Tasks;

namespace SqliteDataAccess.Library;

public static class DbDataAccess
{

    private static readonly string[] Formats = { "TCG", "Speed Duel" };

    public static async Task UpdateDatabase()
    {
        var jsonCards = (await DataProcessor.GetCardsAsync()).ToList();
        jsonCards.RemoveAll(x => x is null || x.ExtraInfo[0].Formats.Intersect(Formats).Count() == 0);

        var jsonSets = jsonCards.SelectMany(x => x.SetInfo)
                                .Select(x => new Set { SetName = x.SetName, SetCode = x.SetCode.Split('-')[0] })
                                .ToList()
                                .Distinct(new GenericComparer<Set>("SetName", "SetCode"))
                                .OrderBy(x => x.SetName)
                                .ToList();

        await DbHelper.InsertSets(jsonSets);
        await DbHelper.InsertOrUpdateCards(jsonCards);
    }

}
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
        var jsonCards = (await DataProcessor.GetCardsAsync());

        jsonCards = jsonCards.Where(x => x is not null &&
                                         x.SetInfo is not null &&
                                         x.ExtraInfo[0].Formats.Intersect(Formats).Count() > 0);

        var jsonSets = jsonCards.SelectMany(x => x.SetInfo)
                                .Select(y => new SetModel { SetName = y.SetName, SetCode = y.SetCode.Split('-')[0] })
                                .ToList()
                                .Distinct(new GenericComparer<SetModel>("SetName", "SetCode"))
                                .OrderBy(z => z.SetName);

        await DbHelper.InsertSets(jsonSets);
        await DbHelper.InsertOrUpdateCards(jsonCards);
    }

}
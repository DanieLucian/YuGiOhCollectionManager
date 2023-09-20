using ApiDataAccess.Library.Models;
using ExternalServices.Helpers;
using System.Linq;
using System.Threading.Tasks;

namespace ExternalServices;

public static class DbDataAccess
{

    private static readonly string[] Formats = { "TCG", "Speed Duel" };

    public static async Task UpdateDatabase()
    {
        var jsonCards = await WebApiDataAccess.GetCardsAsync();

        jsonCards = jsonCards.Where(x => x is not null &&
                                         x.Sets is not null &&
                                         x.ExtraInfo[0].Formats.Intersect(Formats).Count() > 0);

        var jsonSets = jsonCards.SelectMany(x => x.Sets)
                                .Select(y => new SetModel { SetName = y.SetName, SetCode = y.SetCode.Split('-')[0] })
                                .ToList()
                                .Distinct(new GenericComparer<SetModel>("SetName", "SetCode"))
                                .OrderBy(z => z.SetName);

        var jsonCardsSets = jsonCards.SelectMany(x => x.Sets, (x, set) => new CollectionCardModel
                                                                             {
                                                                                 CardId = x.Id,
                                                                                 SetName = set.SetName,
                                                                                 SetCode = set.SetCode.Split('-')[0],
                                                                                 RarityName = set.RarityName,
                                                                                 RarityCode = set.RarityCode
                                                                             })
                                     .ToList()
                                     .Distinct(new GenericComparer<CollectionCardModel>(
                                               "CardId",
                                               "SetName",
                                               "SetCode",
                                               "RarityName"));


        await DbHelper.InsertSets(jsonSets);
        await DbHelper.InsertOrUpdateCards(jsonCards);
        await DbHelper.InsertCardsSets(jsonCardsSets);
    }

}
using ApiDataAccess.Library.Helpers;
using ApiDataAccess.Library.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ExternalServices
{
    public class WebApiDataAccess
    {
        public static async Task<IEnumerable<CardModel>> GetCardsAsync()
        {
            string url = "https://db.ygoprodeck.com/api/v7/cardinfo.php?misc=yes";
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            using (var response = await ApiHelper.ApiClient.SendAsync(request, HttpCompletionOption.ResponseContentRead))
            {
                response.EnsureSuccessStatusCode();

                var jsonString = await response.Content.ReadAsStringAsync();

                var jsonData = JsonConvert.DeserializeObject<CardList>(jsonString, new JsonCardConverter());

                return jsonData.AllCards;
            }
        }

        public static async Task<IEnumerable<SetModel>> GetSetsAsync()
        {
            string url = "https://db.ygoprodeck.com/api/v7/cardsets.php";
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            using (var response = await ApiHelper.ApiClient.SendAsync(request, HttpCompletionOption.ResponseContentRead))
            {
                response.EnsureSuccessStatusCode();

                var jsonData = await response.Content.ReadAsAsync<IEnumerable<SetModel>>();

                return jsonData;
            }
        }
    }
}

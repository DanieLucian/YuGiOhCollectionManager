using ApiDataAccess.Library.Helpers;
using ApiDataAccess.Library.Models;
using Logger.Library;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiDataAccess.Library
{
    public class DataProcessor
    {
        public static async Task<IEnumerable<CardModel>> GetCardsAsync()
        {
            string url = "https://db.ygoprodeck.com/api/v7/cardinfo.php?misc=yes";
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            using (var response = await ApiHelper.ApiClient.SendAsync(request, HttpCompletionOption.ResponseContentRead))
            {
                response.EnsureSuccessStatusCode();
                await Log.Info("API response successful!");

                var jsonString = await response.Content.ReadAsStringAsync();
                await Log.Info("API body has been read as JSON string");

                var jsonData = JsonConvert.DeserializeObject<CardList>(jsonString, new JsonCardConverter());
                await Log.Info("API body has been deserialized into object of type CardList.");

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
                await Log.Info("API response successful!");

                var jsonData = await response.Content.ReadAsAsync<IEnumerable<SetModel>>();
                await Log.Info("API body has been read as IEnumerable of Sets");

                return jsonData;
            }
        }
    }
}

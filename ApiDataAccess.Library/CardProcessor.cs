using ApiDataAccess.Library.Helpers;
using ApiDataAccess.Library.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiDataAccess.Library
{
    public class CardProcessor
    {
        public static async Task<IEnumerable<Card>> GetCardsAsync()
        {
            using (var client = new HttpClient())
            {
                string url = "https://db.ygoprodeck.com/api/v7/cardinfo.php?misc=yes&format=tcg";
                var request = new HttpRequestMessage(HttpMethod.Get, url);

                using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead))
                {
                    response.EnsureSuccessStatusCode();
                        string jsonString = await response.Content.ReadAsStringAsync();
                          var jsonData = JsonConvert.DeserializeObject<CardList>(jsonString, new JsonCardConverter());
                        return jsonData.Data;
                }
            }
        }

    }
}

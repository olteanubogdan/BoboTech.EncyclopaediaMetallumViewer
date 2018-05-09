using BoboTech.EncyclopaediaMetallumViewer.Common;
using BoboTech.EncyclopaediaMetallumViewer.Common.Extensions;
using BoboTech.EncyclopaediaMetallumViewer.Models.Api;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace BoboTech.EncyclopaediaMetallumViewer.UILogic.Services
{
    public static class DataService
    {
        static async Task<T> GetAsync<T>(string query) where T : class
        {
            using (var memoryStream = new MemoryStream())
            using (var client = new HttpClient())
            {
                var stream = await client.GetStreamAsync(new Uri($"{Settings.EncyclopaediaMetallumApiProxy.BaseUrl}{query}"));
                await stream.CopyToAsync(memoryStream);
                await memoryStream.SaveForDebugAsync("json");
                return memoryStream.SerializeJson<T>();
            }
        }

        public static Task<SearchBandResponse> SearchBandAsync(string bandName) => GetAsync<SearchBandResponse>($"/search/band_name/{bandName}");

        public static Task<GetBandResponse> GetBandAsync(long id) => GetAsync<GetBandResponse>($"/band/{id}");
    }
}
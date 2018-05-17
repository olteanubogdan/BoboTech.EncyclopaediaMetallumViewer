using BoboTech.EncyclopaediaMetallumViewer.Common;
using BoboTech.EncyclopaediaMetallumViewer.Common.Extensions;
using BoboTech.EncyclopaediaMetallumViewer.Models.Api;
using System;
using System.IO;
using System.Linq;
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
                var response = await client.GetAsync(new Uri($"{Settings.EncyclopaediaMetallumApiProxy.BaseUrl}{query}"));

                if (!response.IsSuccessStatusCode)
                    if (response.Headers.Contains("ErrorId"))
                        throw new Exception($"API proxy failed with error id {response.Headers.GetValues("ErrorId").FirstOrDefault()}.");
                    else
                        throw new Exception($"API proxy failed with status code {response.StatusCode} and reason '{response.ReasonPhrase}'.");

                await response.Content.CopyToAsync(memoryStream);
                await memoryStream.SaveForDebugAsync("json");
                return memoryStream.SerializeJson<T>();
            }
        }

        public static Task<SearchBandResponse> SearchBandAsync(string bandName) => GetAsync<SearchBandResponse>($"/search/band_name/{bandName}");

        public static Task<GetBandResponse> GetBandAsync(long id) => GetAsync<GetBandResponse>($"/band/{id}");

        public static Task<GetAlbumResponse> GetAlbumAsync(long id) => GetAsync<GetAlbumResponse>($"/album/{id}");
    }
}
using System;
using System.Configuration;
using System.Linq;

namespace BoboTech.EncyclopaediaMetallumViewer.Common
{
    public class Settings
    {
        static Lazy<string> GetLazyValue(string keyName, string defaultValue) => new Lazy<string>(() => ConfigurationManager.AppSettings.AllKeys.Contains(keyName) ? ConfigurationManager.AppSettings[keyName] : defaultValue);

        public static class EncyclopaediaMetallum
        {
            static Lazy<string> _apiKey = GetLazyValue($"{nameof(EncyclopaediaMetallum)}.{nameof(ApiKey)}", ""); //TODO: find another way to get the key

            public static string ApiKey => _apiKey.Value;

            static Lazy<string> _baseUrl = GetLazyValue($"{nameof(EncyclopaediaMetallum)}.{nameof(BaseUrl)}", "");

            /// <summary>
            /// http://em.wemakesites.net/
            /// </summary>
            public static string BaseUrl => _baseUrl.Value;

            static Lazy<string> _searchBand = GetLazyValue($"{nameof(EncyclopaediaMetallum)}.{nameof(SearchBand)}", "");

            /// <summary>
            /// search/band_name/
            /// </summary>
            public static string SearchBand => _searchBand.Value;
        }

        public static class App
        {
            static Lazy<string> _company = GetLazyValue($"{nameof(App)}.{nameof(Company)}", "BoboTech");

            /// <summary>
            /// Company settings. Defaults to BoboTech.
            /// </summary>
            public static string Company => _company.Value;

            static Lazy<string> _name = GetLazyValue($"{nameof(App)}.{nameof(Name)}", "EncyclopaediaMetallumViewer");

            /// <summary>
            /// App name setting. Defaults to EncyclopaediaMetallum.
            /// </summary>
            public static string Name => _name.Value;
        }
    }
}
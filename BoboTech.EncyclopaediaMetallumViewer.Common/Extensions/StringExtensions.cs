using Newtonsoft.Json;
using System;
using System.IO;

namespace BoboTech.EncyclopaediaMetallumViewer.Common.Extensions
{
    public static class StringExtensions
    {
        public static T SerializeJson<T>(this string data) where T : class
        {
            T r = null;
            var jsonSerializer = new JsonSerializer();
            using (var textReader = new StringReader(data))
            using (var jsonTextReader = new JsonTextReader(textReader))
                r = jsonSerializer.Deserialize<T>(jsonTextReader);
            return r;
        }

        public static void SaveForDebug(this string contents, string fileName)
        {
            //var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), fileName);
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Settings.App.Company, Settings.App.Name, "API JSONs");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            path = Path.Combine(path, fileName);
            if (File.Exists(path))
                File.Delete(path);
            File.WriteAllText(path, contents);
        }
    }
}
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using static System.FormattableString;

namespace BoboTech.EncyclopaediaMetallumViewer.Common.Extensions
{
    public static class StreamExtensions
    {
        public static T SerializeJson<T>(this Stream stream) where T : class
        {
            T obj = null;
            var jsonSerializer = new JsonSerializer();

            if (stream.CanSeek)
                stream.Position = 0;

            using (var streamReader = new StreamReader(stream))
            using (var jsonTextReader = new JsonTextReader(streamReader))
                obj = jsonSerializer.Deserialize<T>(jsonTextReader);
            return obj;
        }

        public static async Task SaveForDebugAsync(this Stream stream, string fileExtension)
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Settings.App.Company, Settings.App.Name, "Debug");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            path = Path.Combine(path, Invariant($"{DateTime.Now:yyyyMMdd_HHmmss_ffffff}_{Guid.NewGuid():N}.{fileExtension}"));

            if (File.Exists(path))
                File.Delete(path);

            if (stream.CanSeek)
                stream.Position = 0;

            using (var fileStream = File.Create(path))
                await stream.CopyToAsync(fileStream);
        }
    }
}
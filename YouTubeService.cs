using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace VoiceControlMVP
{
    public class YouTubeService
    {
        private readonly Action<string> _log;
        public YouTubeService(Action<string> log) { _log = log; }

        // Basit: Arama endpointi kullanır, en yeni videoyu döndürür.
        public async Task<string?> GetLatestVideoUrlAsync(string query)
        {
            try
            {
                var apiKey = Config.YouTubeApiKey;
                if (string.IsNullOrWhiteSpace(apiKey))
                {
                    _log("YouTube API anahtarı configte tanımlı değil.");
                    return null;
                }

                var url = $"https://www.googleapis.com/youtube/v3/search?part=snippet&q={Uri.EscapeDataString(query)}&order=date&type=video&maxResults=1&key={apiKey}";
                using var client = new HttpClient();
                var resp = await client.GetStringAsync(url);
                var jobj = JObject.Parse(resp);
                var item = jobj["items"]?.First;
                var videoId = item?["id"]?["videoId"]?.ToString();
                if (videoId != null) return "https://www.youtube.com/watch?v=" + videoId;
            }
            catch (Exception ex)
            {
                _log("YouTubeService hata: " + ex.Message);
            }
            return null;
        }
    }
}

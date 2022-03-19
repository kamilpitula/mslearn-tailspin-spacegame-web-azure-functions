using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace TailSpin.SpaceGame.Web
{
    public class LeaderboardFunctionClient : ILeaderboardServiceClient
    {
        private string _functionUrl;

        public LeaderboardFunctionClient(string functionUrl)
        {
            this._functionUrl = functionUrl;
        }

        async public Task<LeaderboardResponse> GetLeaderboard(int page, int pageSize, string mode, string region)
        {
            using (WebClient webClient = new WebClient())
            {
                var path = $"{this._functionUrl}?page={page}&pageSize={pageSize}&mode={mode}&region={region}";
                string json = await webClient.DownloadStringTaskAsync(path);
                var result = JsonSerializer.Deserialize<LeaderboardResponse>(json);
                if(result is null)
                    throw new Exception(json + "\n" + path);
                return result;
            }
        }
    }
}

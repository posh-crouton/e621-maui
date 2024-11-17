using E621Maui.Lib.Models;
using Newtonsoft.Json;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace E621Maui.Lib
{
    public class ApiAccess
    {
        private HttpClient? _httpClient;

        public string UserName { get; }
        public string ApiKey { get; }

        public ApiAccess(string userName, string apiKey)
        {
            UserName = userName;
            ApiKey = apiKey;
        }

        public void Initialise()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://e621.net/"),
            };
            string basicAuth = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{UserName}:{ApiKey}"));
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {basicAuth}");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", $"Github posh-crouton/e621-maui in use by {UserName}");
        }

        public async Task<Post[]> GetPostsAsync()
        {
            if (_httpClient is null) return [];
            HttpResponseMessage res = await _httpClient.GetAsync($"https://e621.net/posts.json?login={UserName}&api_key={ApiKey}");
            string content = await res.Content.ReadAsStringAsync();
            string strippedContent = string.Join("", content.Skip(9).SkipLast(1));
            return JsonConvert.DeserializeObject<Post[]>(strippedContent) ?? [];
        }

        public async Task FavouritePost(Post post)
        {
            if (_httpClient is null) return;
            HttpContent content = new StringContent("");
            HttpResponseMessage res = await _httpClient.PostAsync($"https://e621.net/favorites.json?login={UserName}&api_key={ApiKey}&post_id={post.Id}",
                content);
            if (res.IsSuccessStatusCode)
            {
                post.IsFavorited = true;
            }
        }

        public async Task UnfavouritePost(Post post)
        {
            if (_httpClient is null) return;
            HttpResponseMessage res = await _httpClient.DeleteAsync($"https://e621.net/favorites.json?login={UserName}&api_key={ApiKey}&post_id={post.Id}");
            if (res.IsSuccessStatusCode)
            {
                post.IsFavorited = false;
            }
        }

        private class Posts
        {
            [JsonPropertyName("posts")]
            public Post[] AllPosts { get; init; } = [];
        }
    }
}

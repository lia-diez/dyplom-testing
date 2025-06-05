using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TerrainToolkit.Runtime.Api
{
    public static class ApiFetcher
    {
        private static readonly HttpClient HttpClient = new HttpClient();

        public static async Task<byte[]> FetchPngAsync(string url)
        {
            try
            {
                return await HttpClient.GetByteArrayAsync(url);
            }
            catch (HttpRequestException e)
            {
                Debug.LogError($"Failed to fetch PNG: {e.Message}");
                return null;
            }
        }
        
        public static async Task<byte[]> PostPromptAsync(string url, string prompt, string apiKey)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = new StringContent(
                        $"{{\"prompt\":\"{prompt}\"}}",
                        Encoding.UTF8,
                        "application/json"
                    )
                };

                request.Headers.Add("X-API-KEY", apiKey);

                var response = await HttpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsByteArrayAsync();
            }
            catch (HttpRequestException e)
            {
                Debug.LogError($"Failed to POST prompt: {e.Message}");
                return null;
            }
        }
    }
}
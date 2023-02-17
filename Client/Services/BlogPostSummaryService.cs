using Client.Models;
using System.Net.Http.Json;

namespace Client.Services
{
    public class BlogPostSummaryService
    {
        private readonly HttpClient _httpClient;
        public List<BlogPost> Summaries;
        public BlogPostSummaryService(HttpClient http)
        {
            ArgumentNullException.ThrowIfNull(http,nameof(http));
            _httpClient = http;
        }
        public async Task LoadBlogPostSummaries()
        {
            if(Summaries == null)
            {
                Summaries = await _httpClient.GetFromJsonAsync<List<BlogPost>>("api/blogposts");
            }
            
        }
    }
}

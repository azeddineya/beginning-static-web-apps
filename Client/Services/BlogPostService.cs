using Client.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Client.Services
{
    public class BlogPostService
    {
        private readonly HttpClient httpClient;
        private readonly NavigationManager  navigationManager;
        private List<BlogPost> blogPostsCache = new();
        public BlogPostService(HttpClient httpClient, NavigationManager navigationManager)
        {
            ArgumentNullException.ThrowIfNull(httpClient, nameof(httpClient));
            ArgumentNullException.ThrowIfNull(navigationManager, nameof(navigationManager));
            this.httpClient = httpClient;
            this.navigationManager = navigationManager;
        }

        public async Task<BlogPost?> GetBlogPost(Guid blogPostId, string author)
        {
            BlogPost? blogPost = blogPostsCache.FirstOrDefault( bp => bp.Id == blogPostId && bp.Author == author);
            if (blogPost is null)
            {
                var result = await httpClient.GetAsync($"api/blogposts/{author}/{blogPostId}");
                if(!result.IsSuccessStatusCode)
                {
                    navigationManager.NavigateTo("404");
                    return null;
                }
                blogPost = await result.Content.ReadFromJsonAsync<BlogPost>();
                if(blogPost is null) {
                    navigationManager.NavigateTo("404");
                    return null;
                }
                blogPostsCache.Add(blogPost);
            }
            return blogPost;
        }

    }
}

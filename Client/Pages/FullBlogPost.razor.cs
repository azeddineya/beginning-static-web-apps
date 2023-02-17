using Client.Models;
using Client.Services;
using Microsoft.AspNetCore.Components;

namespace Client.Pages
{
    public partial class FullBlogPost
    {

        private BlogPost? blogPost;
        
        [Inject]
        BlogPostService BlogPostService { get; set; }

        [Parameter]
        public Guid Id { get; set; }
        [Parameter]
        public string Author { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            blogPost = await BlogPostService.GetBlogPost(Id,Author);
        }

    }
}

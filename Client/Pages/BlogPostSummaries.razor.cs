using Client.Services;
using Microsoft.AspNetCore.Components;

namespace Client.Pages
{
    public partial class BlogPostSummaries
    {
        [Inject]
        BlogPostSummaryService BlogPostSummaryService { get; set; }
        [Inject]
        NavigationManager navigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await BlogPostSummaryService.LoadBlogPostSummaries();
        }

        public void Navigate(Guid id, string author) => navigationManager.NavigateTo($"/blogposts/{author}/{id}");

    }
}

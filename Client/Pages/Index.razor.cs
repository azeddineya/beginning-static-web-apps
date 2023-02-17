using Client.Models;
using Client.Services;
using Microsoft.AspNetCore.Components;

namespace Client.Pages
{
    public partial class Index
    {
        [Inject]
        BlogPostSummaryService BlogPostSummaryService { get; set;}

        private BlogPost? Summary => BlogPostSummaryService.Summaries.OrderByDescending(bps =>bps.PublishedDate).FirstOrDefault();

        protected override async Task OnInitializedAsync()
        {
            await BlogPostSummaryService.LoadBlogPostSummaries();
        }
    }
}

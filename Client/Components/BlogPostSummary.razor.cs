using Client.Models;
using Microsoft.AspNetCore.Components;

namespace Client.Components
{
    public partial class BlogPostSummary
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;
        [Parameter]
        public BlogPost Summary { get; set; }

        void Navigate() => NavigationManager.NavigateTo($"/blogposts/{Summary.Author}/{Summary.Id}");
    }
}

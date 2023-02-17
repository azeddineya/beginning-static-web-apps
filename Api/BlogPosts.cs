using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using Api.Models;
using System.Linq;

namespace Api
{
    public static class BlogPosts
    {
        [FunctionName("BlogPosts_Get")]
        public static IActionResult GetAllBlogPosts(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "blogposts")] HttpRequest req,
            [CosmosDB("SwaBlog","BlogContainer", Connection ="CosmosDbConnectionString",
            SqlQuery =@"
                SELECT 
                c.id,
                c.Title,
                c.Author,
                c.PublishedDate,
                LEFT(c.BlogPostMarkdown,500) As BlogPostMarkdown,
                Length(c.BlogPostMarkdown) <= 500 As PreviewIsComplete,
                c.Tags
                FROM c
                WHERE c.Status =2")
            ]IEnumerable<BlogPost> blogPosts,
            ILogger log)
        {
            return new OkObjectResult(blogPosts);
        }

        [FunctionName("BlogPosts_GetId")]
        public static IActionResult GetBlogPost(
            [HttpTrigger(AuthorizationLevel.Anonymous,"get",Route ="blogposts/{author}/{id}")]HttpRequest req, 
            [CosmosDB("SwaBlog","BlogContainer", Connection ="CosmosDbConnectionString",
            SqlQuery =@"
                SELECT
                c.id,
                c.Title,
                c.Author,
                c.PublishedDate,
                c.BlogPostMarkdpwn,
                c.Status,
                c.Tags
                FROM c
                WHERE c.id ={id} and c.Author={author}")
            ]IEnumerable<BlogPost> blogposts,
            ILogger log)
        {
            if(blogposts.ToArray().Length == 0)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(blogposts.First());
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using WildlifeAPI.Data;
using WildlifeAPI.Models;
namespace WildlifeAPI.Controllers;

public static class BlogsEndpoints
{
    public static void MapBlogsEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Blogs").WithTags(nameof(Blogs));

        group.MapGet("/", async (WildlifeAPIContext db) =>
        {
            return await db.Blogs.ToListAsync();
        })
        .WithName("GetAllBlogss")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Blogs>, NotFound>> (int id, WildlifeAPIContext db) =>
        {
            return await db.Blogs.AsNoTracking()
                .FirstOrDefaultAsync(model => model.id == id)
                is Blogs model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetBlogsById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Blogs blogs, WildlifeAPIContext db) =>
        {
            var affected = await db.Blogs
                .Where(model => model.id == id)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.id, blogs.id)
                  .SetProperty(m => m.blogTitle, blogs.blogTitle)
                  .SetProperty(m => m.blogContent, blogs.blogContent)
                  .SetProperty(m => m.author, blogs.author)
                  .SetProperty(m => m.datePosted, blogs.datePosted)
                  .SetProperty(m => m.image, blogs.image)
                );

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateBlogs")
        .WithOpenApi();

        group.MapPost("/", async (Blogs blogs, WildlifeAPIContext db) =>
        {
            db.Blogs.Add(blogs);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Blogs/{blogs.id}",blogs);
        })
        .WithName("CreateBlogs")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, WildlifeAPIContext db) =>
        {
            var affected = await db.Blogs
                .Where(model => model.id == id)
                .ExecuteDeleteAsync();

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteBlogs")
        .WithOpenApi();
    }
}

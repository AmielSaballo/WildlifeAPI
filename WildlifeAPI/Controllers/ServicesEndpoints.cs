using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using WildlifeAPI.Data;
using WildlifeAPI.Models;
namespace WildlifeAPI.Controllers;

public static class ServicesEndpoints
{
    public static void MapServicesEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Services").WithTags(nameof(Services));

        group.MapGet("/", async (WildlifeAPIContext db) =>
        {
            return await db.Services.ToListAsync();
        })
        .WithName("GetAllServicess")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Services>, NotFound>> (int id, WildlifeAPIContext db) =>
        {
            return await db.Services.AsNoTracking()
                .FirstOrDefaultAsync(model => model.id == id)
                is Services model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetServicesById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Services services, WildlifeAPIContext db) =>
        {
            var affected = await db.Services
                .Where(model => model.id == id)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.id, services.id)
                  .SetProperty(m => m.serviceName, services.serviceName)
                  .SetProperty(m => m.serviceDescription, services.serviceDescription)
                  .SetProperty(m => m.location, services.location)
                  .SetProperty(m => m.phoneNumber, services.phoneNumber)
                  .SetProperty(m => m.email, services.email)
                  .SetProperty(m => m.linkTo, services.linkTo)
                  .SetProperty(m => m.image, services.image)
                );

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateServices")
        .WithOpenApi();

        group.MapPost("/", async (Services services, WildlifeAPIContext db) =>
        {
            db.Services.Add(services);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Services/{services.id}",services);
        })
        .WithName("CreateServices")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, WildlifeAPIContext db) =>
        {
            var affected = await db.Services
                .Where(model => model.id == id)
                .ExecuteDeleteAsync();

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteServices")
        .WithOpenApi();
    }
}

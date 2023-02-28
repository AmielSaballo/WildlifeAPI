using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using WildlifeAPI.Data;
using WildlifeAPI.Models;
namespace WildlifeAPI.Controllers;

public static class AnimalsEndpoints
{
    public static void MapAnimalsEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Animals").WithTags(nameof(Animals));

        group.MapGet("/", async (WildlifeAPIContext db) =>
        {
            return await db.Animals.ToListAsync();
        })
        .WithName("GetAllAnimalss")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Animals>, NotFound>> (int id, WildlifeAPIContext db) =>
        {
            return await db.Animals.AsNoTracking()
                .FirstOrDefaultAsync(model => model.id == id)
                is Animals model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetAnimalsById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Animals animals, WildlifeAPIContext db) =>
        {
            var affected = await db.Animals
                .Where(model => model.id == id)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.id, animals.id)
                  .SetProperty(m => m.Category, animals.Category)
                  .SetProperty(m => m.ScientificName, animals.ScientificName)
                  .SetProperty(m => m.CommonName, animals.CommonName)
                  .SetProperty(m => m.Status, animals.Status)
                  .SetProperty(m => m.Location, animals.Location)
                );

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateAnimals")
        .WithOpenApi();

        group.MapPost("/", async (Animals animals, WildlifeAPIContext db) =>
        {
            db.Animals.Add(animals);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Animals/{animals.id}",animals);
        })
        .WithName("CreateAnimals")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, WildlifeAPIContext db) =>
        {
            var affected = await db.Animals
                .Where(model => model.id == id)
                .ExecuteDeleteAsync();

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteAnimals")
        .WithOpenApi();
    }
}

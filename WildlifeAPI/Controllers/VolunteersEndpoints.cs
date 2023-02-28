using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using WildlifeAPI.Data;
using WildlifeAPI.Models;
namespace WildlifeAPI.Controllers;

public static class VolunteersEndpoints
{
    public static void MapVolunteersEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Volunteers");

        group.MapGet("/", async (WildlifeAPIContext db) =>
        {
            return await db.Volunteers.ToListAsync();
        })
        .WithName("GetAllVolunteerss");

        group.MapGet("/{id}", async Task<Results<Ok<Volunteers>, NotFound>> (int id, WildlifeAPIContext db) =>
        {
            return await db.Volunteers.AsNoTracking()
                .FirstOrDefaultAsync(model => model.id == id)
                is Volunteers model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetVolunteersById");

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Volunteers volunteers, WildlifeAPIContext db) =>
        {
            var affected = await db.Volunteers
                .Where(model => model.id == id)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.id, volunteers.id)
                  .SetProperty(m => m.firstName, volunteers.firstName)
                  .SetProperty(m => m.lastName, volunteers.lastName)
                  .SetProperty(m => m.phoneNumber, volunteers.phoneNumber)
                  .SetProperty(m => m.email, volunteers.email)
                );

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateVolunteers");

        group.MapPost("/", async (Volunteers volunteers, WildlifeAPIContext db) =>
        {
            db.Volunteers.Add(volunteers);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Volunteers/{volunteers.id}",volunteers);
        })
        .WithName("CreateVolunteers");

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, WildlifeAPIContext db) =>
        {
            var affected = await db.Volunteers
                .Where(model => model.id == id)
                .ExecuteDeleteAsync();

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteVolunteers");
    }
}

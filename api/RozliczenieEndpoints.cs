using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using MiniValidation;
using Microsoft.AspNetCore.Mvc;
using pitWeb.Models;

namespace pitWeb.api
{
    public static class RozliczenieEndpoints
    {
        public static void RozliczenieEnpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/rozliczenia").WithTags("Rozliczenia");

            group.MapGet("/", GetAllRozliczenia).Produces<List<RozliczenieModel>>(StatusCodes.Status200OK).Produces(StatusCodes.Status400BadRequest);

            group.MapGet("/{num}", GetCertainNumberOfRozliczenie).Produces<List<RozliczenieModel>>(StatusCodes.Status200OK).Produces(StatusCodes.Status400BadRequest);

            group.MapPost("/", CreateRozliczenie).Accepts<RozliczenieModel>("application/json").Produces<RozliczenieModel>(StatusCodes.Status201Created).Produces(StatusCodes.Status400BadRequest);

            group.MapDelete("/{id}", DeleteRozliczenieById).Produces(StatusCodes.Status204NoContent).Produces(StatusCodes.Status404NotFound);
        }

        private static async Task<IResult> GetAllRozliczenia(AppDbContext db)
        {
            var rozliczenia = await db.Rozliczenia.ToListAsync();
            return Results.Ok(rozliczenia);
        }

        private static async Task<IResult> GetCertainNumberOfRozliczenie(AppDbContext db, [FromRoute] int num)
            {
                var rozliczenia = await db.Rozliczenia.Take(num).ToListAsync();
                return Results.Ok(rozliczenia);
        }

        private static async Task<IResult> CreateRozliczenie(AppDbContext db, [FromBody] RozliczenieModel model)
        {
            if (!MiniValidator.TryValidate(model, out var errors))
            {
                return Results.BadRequest(errors);
            }
            model.DataZapisu = DateTime.UtcNow;
            db.Rozliczenia.Add(model);
            await db.SaveChangesAsync();
            return Results.Created($"/api/rozliczenia/{model.Id}", model);
        }

        private static async Task<IResult> DeleteRozliczenieById(AppDbContext db, [FromRoute] int id)
        {
            var rozliczenie = await db.Rozliczenia.FindAsync(id);
            if (rozliczenie == null)
            {
                return Results.NotFound();
            }
            db.Rozliczenia.Remove(rozliczenie);
            await db.SaveChangesAsync();
            return Results.NoContent();
        }
    }
}

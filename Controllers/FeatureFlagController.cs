using Microsoft.EntityFrameworkCore;
using FeatureFlagAPI.Data;
using FeatureFlagAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace FeatureFlagAPI.Controllers;

public static class FeatureFlagController
{
    public static void MapFeatureFlagEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/FeatureFlag", async (FeatureFlagAPIContext db) =>
        {
            return await db.FeatureFlag.ToListAsync();
        })
        .WithName("GetAllFeatureFlags");

        routes.MapGet("/api/FeatureFlag/{id}", async (int Id, FeatureFlagAPIContext db) =>
        {
            return await db.FeatureFlag.FindAsync(Id)
                is FeatureFlag model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetFeatureFlagById");

        routes.MapPost("/api/FeatureFlag/toggle", async (FeatureFlag featureFlag, FeatureFlagAPIContext db) =>
        {
            var foundModel = await db.FeatureFlag.FirstOrDefaultAsync(x => x.Identifier.ToLower() == featureFlag.Identifier.ToLower());

            if (foundModel is null)
            {
                return Results.NotFound();
            }

            foundModel.IsEnabled = !foundModel.IsEnabled;

            await db.SaveChangesAsync();

            return Results.Created($"/FeatureFlags/{featureFlag.Id}", foundModel);
        })
        .WithName("UpdateFeatureFlag");

        routes.MapPost("/api/FeatureFlag/", async (FeatureFlag featureFlag, FeatureFlagAPIContext db) =>
        {
            db.FeatureFlag.Add(featureFlag);
            await db.SaveChangesAsync();
            return Results.Created($"/FeatureFlags/{featureFlag.Id}", featureFlag);
        })
        .WithName("CreateFeatureFlag");

        routes.MapDelete("/api/FeatureFlag/{id}", async (int Id, FeatureFlagAPIContext db) =>
        {
            if (await db.FeatureFlag.FindAsync(Id) is FeatureFlag featureFlag)
            {
                db.FeatureFlag.Remove(featureFlag);
                await db.SaveChangesAsync();
                return Results.Ok(featureFlag);
            }

            return Results.NotFound();
        })
        .WithName("DeleteFeatureFlag");
    }
}

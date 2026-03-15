using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using WebAPIDemo.Model;
namespace WebAPIDemo.Controllers;

public static class StudentEndpoints
{
    public static void MapStudentEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Student").WithTags(nameof(Student));

        group.MapGet("/", () =>
        {
            return new [] { new Student() };
        })
        .WithName("GetAllStudents");

        group.MapGet("/{id}", (int id) =>
        {
            //return new Student { ID = id };
        })
        .WithName("GetStudentById");

        group.MapPut("/{id}", (int id, Student input) =>
        {
            return TypedResults.NoContent();
        })
        .WithName("UpdateStudent");

        group.MapPost("/", (Student model) =>
        {
            //return TypedResults.Created($"/api/Students/{model.ID}", model);
        })
        .WithName("CreateStudent");

        group.MapDelete("/{id}", (int id) =>
        {
            //return TypedResults.Ok(new Student { ID = id });
        })
        .WithName("DeleteStudent");
    }
}

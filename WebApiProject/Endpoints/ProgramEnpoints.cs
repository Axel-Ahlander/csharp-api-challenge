using Microsoft.AspNetCore.Mvc;
using WebApiProject.Repository;

namespace WebApiProject.Endpoints
{
    public static class ProgramEnpoints
    {

        public static void ConfigureProgramEndpoints(this WebApplication app)
        {
            var programs = app.MapGroup("programs");
            programs.MapGet("/available", GetAvailablePrograms);
            programs.MapPost("/start/{name}", ProgramToStart);
            programs.MapGet("/curr", GetCurrentProgram);

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetAvailablePrograms(IRepository repo)
        {
            var result = await repo.AvailablePrograms();
            if (result != null)
            {
                return TypedResults.Ok(result);
            }
            return null;
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> ProgramToStart(IRepository repo, string name)
        {
            var result = await repo.AvailablePrograms();
            var program = result.FirstOrDefault(prog => prog.Name == name);

            if (program != null)
            {
                repo.ProgramToStart(program);
                return TypedResults.Ok(program);
            }

            return null;

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetCurrentProgram(IRepository repo)
        {

            var program = repo.CurrentProgram();
            if (program != null)
            {
               

                return TypedResults.Ok(program);
            }

            return null;

        }
    }
}
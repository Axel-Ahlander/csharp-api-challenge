using WebApiProject.Models;

namespace WebApiProject.Repository
{
    public interface IRepository
    {
        Task<IEnumerable<ProgramType>> AvailablePrograms();
        Task<string> ProgramToStart(ProgramType program);
        string CurrentProgram();
        Task<IEnumerable<ProgramType>> Last150Programs();
        Task<string> StopProgram(ProgramType program);
        Task<string> GetStats();
    }
}

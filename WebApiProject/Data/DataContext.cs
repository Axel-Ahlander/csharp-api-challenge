using Microsoft.EntityFrameworkCore;
using WebApiProject.Models;

namespace WebApiProject.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        

        public DbSet<ProgramType>Programs { get; set; }
        public DbSet<ProgramType>ProgramsRan { get; set; }


        public void Initialize()
        {
            Programs.AddRange(new List<ProgramType>
    {
            new ProgramType { Name = "Intensive 70", WaterConsumption = 13.5M, ElectricityConsumption = 1.35M, Time = 150 },
            new ProgramType { Name = "Eco 50", WaterConsumption = 9.0M, ElectricityConsumption = 0.65M, Time = 60 },
            new ProgramType { Name = "Half Load", WaterConsumption = 10.5M, ElectricityConsumption =  1.10M, Time = 40 },
            new ProgramType { Name = "Clean Cycle", WaterConsumption = 14, ElectricityConsumption = 1.45M, Time = 55 },

        

    });
        }
        

    }
}

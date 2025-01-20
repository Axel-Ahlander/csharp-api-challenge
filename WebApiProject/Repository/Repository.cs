using Engine;
using Microsoft.EntityFrameworkCore;
using WebApiProject.Data;
using WebApiProject.Models;

namespace WebApiProject.Repository
{
    public class Repository : IRepository
    {

        DataContext _db;
        DishwasherEngine _engine;
        

        public Repository(DataContext db, DishwasherEngine engine)
        {
            _db = db;
            _engine = engine;
            
        }

      
        public async Task<IEnumerable<ProgramType>> AvailablePrograms()
        {
            

            var availablePrograms = await _db.Programs.Where(program => program.WaterConsumption < _engine.Water && program.ElectricityConsumption < _engine.Electricity).ToListAsync(); 


            return availablePrograms;
        }

        public string CurrentProgram()
        {
            var _currProgram = _db.Programs.FirstOrDefault(program => program.Active == true);
            if ( _currProgram != null)
            {
                return $"{_currProgram.Name} is currently running, and it has {_currProgram.Time - _engine.CurrentTime} minutes left. The engine currently has {_engine.Water - _currProgram.WaterConsumption} litres of water left, and {_engine.Electricity - _currProgram.ElectricityConsumption} electricity left";
            }

            return "There is no ongoing program";
        }

        public async Task<string> GetStats()
        {
            decimal avgWater = 0;
            decimal avgElectrictiy = 0;
            int time = 0;

            await _db.ProgramsRan.ForEachAsync(program => { avgWater += program.WaterConsumption; avgElectrictiy += program.ElectricityConsumption; time += program.Time; });

            return $"average water consumption : {avgWater / _db.ProgramsRan.ToList().Count}, average energy consumption : {avgElectrictiy / _db.ProgramsRan.ToList().Count}, average time spent : {time / _db.ProgramsRan.ToList().Count}";
        }

        public async Task<IEnumerable<ProgramType>> Last150Programs()
        {
            return await _db.ProgramsRan.Where(program => program.Id < 151).ToListAsync();
        }

        public async Task<string> ProgramToStart(ProgramType program)
        {
            if (_engine.IsGoing)
            {
                return "A program is already running";
            }

            _engine.MaxTime = program.Time;

            program.Active = true;

           
            var existingProgram = await _db.Programs.FindAsync(program.Id); 
            if (existingProgram != null)
            {
                existingProgram.Active = true; 
                _db.Programs.Update(existingProgram);
            }
            else
            {
                
                _db.ProgramsRan.Add(program);
            }

            await _db.SaveChangesAsync();

            _engine.IsGoing = true;
            return await _engine.StartEngine(program.Name);
        }

        public async Task<string> StopProgram(ProgramType program)
        {
           if (_engine.IsGoing == false)
            {
                return "The engine isnt going";
            }

            return await _engine.StopEngine();

        }
    }
}

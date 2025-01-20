namespace WebApiProject.Models
{
    public class ProgramType
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal WaterConsumption { get; set; }
        public decimal ElectricityConsumption { get; set; }
        public int Time {  get; set; }
        
        public bool Active { get; set; }
    }
}

namespace Drivers_Management.Domain.Models
{
    public class Vehicle : BaseModel
    {
        public string Brand { get; set; }
        public string Plate { get; set; }
        public string Year { get; set; }
        public ICollection<Driver>  Drivers { get; set; }
    }
}
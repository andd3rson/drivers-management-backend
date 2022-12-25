namespace Drivers_Management.Domain.Models
{
    public class Vehicle : BaseModel
    {
        public string Brand { get; set; }
        public string Plate { get; set; }
        public string Year { get; set; }
        public IEnumerable<Driver> Drivers { get; set; }


        public void ToUpdate(Vehicle vehicle)
        {
            Brand = vehicle.Brand;
            Plate = vehicle.Plate;
            Year = vehicle.Year;
        }

    }
}
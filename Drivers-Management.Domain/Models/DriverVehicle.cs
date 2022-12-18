namespace Drivers_Management.Domain.Models
{
    public class DriverVehicle
    {

        public int DriversId { get; set; }
        public int VehiclesId { get; set; }
        public Driver Drivers { get; set; }
        public Vehicle Vehicles { get; set; }
    }
}
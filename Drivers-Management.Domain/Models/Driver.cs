namespace Drivers_Management.Domain.Models
{
    public class Driver : BaseModel
    {
        public string Name { get; set; }
        public string Cpf { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }

    }
}
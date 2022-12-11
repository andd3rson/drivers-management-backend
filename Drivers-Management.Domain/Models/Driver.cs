namespace Drivers_Management.Domain.Models
{
    public class Driver : BaseModel
    {
        public string Name { get; set; }
        public string Cpf { get; set; }

        public string Email { get; set; }
        public IEnumerable<Vehicle> Vehicles { get; set; }


        public void ToUpdate(Driver driver)
        {
            Name = driver.Name;
            Email = driver.Email;
        }
    }
}
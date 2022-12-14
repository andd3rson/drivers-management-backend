namespace Drivers_Management.Application.Dtos
{
    public record DriverRequest(string Name, string Cpf, string Email);
    public record DriverResponse(int Id, string Name, string Cpf, string Email, List<DriverVehicleResponse> Vehicles);
    public record DriverVehicleResponse(int Id, string Brand, string Plate, string Year);
}
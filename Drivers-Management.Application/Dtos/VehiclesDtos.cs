namespace Drivers_Management.Application.Dtos
{
    public record VehiclesRequest(string Brand, string Plate, string Year);
    
    public record VehiclesResponse(int Id, string Brand, string Plate, string Year, List<DriversResponse> Drivers);
    public record DriversResponse(int Id, string Name, string Cpf, string Email);

}
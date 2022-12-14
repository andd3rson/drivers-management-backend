namespace Drivers_Management.Application.Dtos
{
    public record VehiclesRequest(string Brand, string Plate, string Year);
    public record VehiclesResponse(string Id, string Brand, string Plate, string Year);

}
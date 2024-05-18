using Campoverde.QMS.Models;

namespace Campoverde.QMS.Interfaces;

public interface IVehicle
{
    Task<string> CreateVehicleAsync(Vehicle vehicle);
    Task<List<Vehicle>> GetAllVehiclesAsync();
}

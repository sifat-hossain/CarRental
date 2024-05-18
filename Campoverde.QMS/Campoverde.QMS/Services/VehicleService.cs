using Campoverde.QMS.Models;

namespace Campoverde.QMS.Services;

public class VehicleService : IVehicle
{
    private readonly CampoverdeDbContext _context;
    public VehicleService(CampoverdeDbContext context)
    {
        _context = context;
    }

    public async Task<string> CreateVehicleAsync(Vehicle vehicle)
    {
        try
        {
            if (vehicle != null)
            {
                var dbItem = await _context.Vehicle.Where(x => x.Id == vehicle.Id).FirstOrDefaultAsync();

                if (dbItem != null)
                {
                    var vehicleUpdate = new Vehicle
                    {
                        Id = vehicle.Id,
                        Model = vehicle.Model,
                        Price = vehicle.Price
                    };
                    _context.Vehicle.Update(vehicleUpdate);
                }
                else
                {
                    await _context.Vehicle.AddAsync(vehicle);
                }
                _context.SaveChanges();

                return "success";
            }
            else
            {
                return "Object has no data";
            }

        }
        catch (Exception ex)
        {
            return $"Falied with message {ex.Message}, inner exception {ex.InnerException?.Message}";
        }
    }

    public async Task<List<Vehicle>> GetAllVehiclesAsync()
    {
        try
        {
            var vehicles = await _context.Vehicle.ToListAsync();
            if (vehicles.Count > 0)
            {
                return vehicles;
            }
            else
            {
                return [];
            }
        }
        catch
        {
            return [];
        }
    }
}

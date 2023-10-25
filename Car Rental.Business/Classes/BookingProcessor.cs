using Car_Rental.Common.Classes;
using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;
using Car_Rental.Data.Interfaces;

namespace Car_Rental.Business.Classes;

public class BookingProcessor
{
    readonly IData _db;
    readonly InputValues _iv;

    public BookingProcessor(IData db, InputValues iv) => (_db, _iv) = (db, iv);

    public IEnumerable<IPerson> GetCustomers() => _db.Get<IPerson>(null);
    public IEnumerable<IVehicle> GetVehicles(VehicleStatuses status = default) => _db.Get<IVehicle>(null);
    public IEnumerable<IBooking> GetBookings() => _db.Get<IBooking>(null);

    public void AddVehicle(string regNo, string make, double odometer, double costKm, VehicleTypes type)
    {
        try
        {
            _iv.error = string.Empty;

            double costDay = (double)type;

            if (regNo.Length == 0 || make.Length == 0 || odometer == 0 ||
                costKm == 0 || (int)type == 0)
            {
                _iv.error = "Could not add vehicle.";
                return;
            }

            if (type == VehicleTypes.Motorcycle)
                _db.Add<IVehicle>(new Motorcycle(_db.NextVehicleId, regNo, make,
                    odometer, costKm, type, costDay, VehicleStatuses.Available));
            else
                _db.Add<IVehicle>(new Car(_db.NextVehicleId, regNo, make, odometer,
                    costKm, type, costDay, VehicleStatuses.Available));

            _iv.vehicle.RegNo = string.Empty;
            _iv.vehicle.Make = string.Empty;
            _iv.vehicle.Odometer = default;
            _iv.vehicle.CostKm = default;
            _iv.vehicle.Type = default;
        }
        catch (Exception ex)
        {
            _iv.error = ex.Message;
        }
    }

    public void AddCustomer(string ssn, string lastName, string firstName)
    {
        try
        {
            _iv.error = string.Empty;

            if (ssn.Length == 0 || lastName.Length == 0 || firstName.Length == 0)
            {
                _iv.error = "Could not add customer.";
                return;
            }

            _db.Add<IPerson>(new Customer(_db.NextPersonId, ssn, lastName, firstName));

            _iv.newCustomer.SSN = string.Empty;
            _iv.newCustomer.LastName = string.Empty;
            _iv.newCustomer.FirstName = string.Empty;
        }
        catch (Exception ex)
        {
            _iv.error = ex.Message;
        }
    }

    public async Task RentVehicle(int vehicleId, int customerId)
    {
        try
        {
            _iv.error = string.Empty;

            if (customerId == 0)
            {
                _iv.error = "Select a customer before renting the vehicle.";
                return;
            }

            var vehicle = GetVehicle(vehicleId) ?? throw new Exception("Couldn't find vehicle.");
            var customer = GetPerson(customerId) ?? throw new Exception("Couldn't find customer.");

            _iv.processing = true;
            await Task.Delay(10000);

            if (vehicle.Type == VehicleTypes.Motorcycle)
                vehicle = (Motorcycle)vehicle;
            else vehicle = (Car)vehicle;

            _db.Add<IBooking>(new Booking(_db.NextBookingId, (Customer)customer, DateTime.Today, vehicle,
                VehicleStatuses.Booked));
            vehicle.Status = VehicleStatuses.Booked;
        }
        catch (Exception ex)
        {
            _iv.error = ex.Message;
        }

        _iv.selectedCustomer = default;
        _iv.processing = false;
    }

    public void ReturnVehicle(int vehicleId, string km)
    {
        try
        {
            _iv.error = string.Empty;

            if (km.Length == 0)
            {
                _iv.error = "Write the distance driven before returning the vehicle.";
                return;
            }

            var dist = double.Parse(km);
            var booking = GetBooking(vehicleId) ?? throw new Exception("Couldn't find customer.");

            booking.Returned = DateTime.Today;
            booking.KmReturned = booking.KmRented + dist;
            booking.Cost = booking.CalculateCost(booking.Vehicle, dist);
            booking.Status = VehicleStatuses.Available;
            booking.Vehicle.Status = VehicleStatuses.Available;
            booking.Vehicle.Odometer += dist;
        }
        catch (Exception ex)
        {
            _iv.error = ex.Message;
        }

        _iv.distance = string.Empty;
    }

    public IBooking? GetBooking(int vehicleId) => _db.Single<IBooking>(b => b.Vehicle.Id == vehicleId && b.Status == VehicleStatuses.Booked);
    public IPerson? GetPerson(int customerId) => _db.Single<IPerson>(p => p.Id == customerId);
    public IVehicle? GetVehicle(int vehicleId) => _db.Single<IVehicle>(v => v.Id == vehicleId);

    // Används ej

    //public IPerson? GetPerson(string ssn) => _db.Single<IPerson>(p => p.SSN == ssn);
    //public IVehicle? GetVehicle(string regNo) => _db.Single<IVehicle>(v => v.RegNo == regNo);

    //public string[] VehicleStatusNames => _db.VehicleStatusNames;
    //public string[] VehicleTypeNames => _db.VehicleTypeNames;
    //public VehicleTypes GetVehicleType(string name) => _db.GetVehicleType(name);
}
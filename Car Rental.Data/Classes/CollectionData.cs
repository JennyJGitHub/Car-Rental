using Car_Rental.Common.Classes;
using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;
using Car_Rental.Data.Interfaces;
using System.Reflection;

namespace Car_Rental.Data.Classes;

public class CollectionData : IData
{
    readonly List<IPerson> _people = new();
    readonly List<IVehicle> _vehicles = new();
    readonly List<IBooking> _bookings = new();
    
    public int NextVehicleId => _vehicles.Count.Equals(0) ? 1 : _vehicles.Max(v => v.Id) + 1;
    public int NextPersonId => _people.Count.Equals(0) ? 1 : _people.Max(p => p.Id) + 1;
    public int NextBookingId => _bookings.Count.Equals(0) ? 1 : _bookings.Max(b => b.Id) + 1;

    public CollectionData() => SeedData();

    void SeedData()
    {
        _people.Add(new Customer(NextPersonId, "12345", "Doe", "John"));
        _people.Add(new Customer(NextPersonId, "98765", "Doe", "Jane"));
        _vehicles.Add(new Car(NextVehicleId, "ABC123", "Volvo", 10000, 1, VehicleTypes.Combi, 200));
        _vehicles.Add(new Car(NextVehicleId, "DEF456", "Saab", 20000, 1, VehicleTypes.Sedan, 100));
        _vehicles.Add(new Car(NextVehicleId, "GHI789", "Tesla", 1000, 3, VehicleTypes.Sedan, 100, VehicleStatuses.Booked));
        _vehicles.Add(new Car(NextVehicleId, "JKL012", "Jeep", 5000, 1.5, VehicleTypes.Van, 300));
        _vehicles.Add(new Motorcycle(NextVehicleId, "MNO234", "Yamaha", 30000, 0.5, VehicleTypes.Motorcycle, 50));
        _bookings.Add(new Booking(NextBookingId, (Customer)_people[0], DateTime.Today, _vehicles[2], VehicleStatuses.Booked));                                                
        _bookings.Add(new Booking(NextBookingId, (Customer)_people[1], DateTime.Today, DateTime.Today, 0, _vehicles[3], VehicleStatuses.Available));
    }

    public List<T> Get<T>(Func<T, bool>? expression)
    {
        try
        {
            var collection = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .FirstOrDefault(f => f.FieldType == typeof(List<T>)) 
                ?? throw new ArgumentException("Couldn't find list");

            var value = collection.GetValue(this) ?? throw new Exception();

            var list = ((List<T>)value).AsQueryable();

            if (expression == null) return list.ToList();

            return list.Where(expression).ToList();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public T? Single<T>(Func<T, bool>? expression)
    {
        try
        {
            var collection = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .FirstOrDefault(f => f.FieldType == typeof(List<T>))
                ?? throw new ArgumentException("Couldn't find list");

            var value = collection.GetValue(this) ?? throw new Exception();

            var list = ((List<T>)value).AsQueryable();

            if (expression == null) throw new ArgumentNullException();

            return (T?)list.SingleOrDefault(expression);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public void Add<T>(T item)
    {
        try
        {
            var collection = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .FirstOrDefault(f => f.FieldType == typeof(List<T>))
                ?? throw new ArgumentException("Couldn't find list");

            var value = collection.GetValue(this) ?? throw new Exception();

            var list = (List<T>)value;

            list.Add(item);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
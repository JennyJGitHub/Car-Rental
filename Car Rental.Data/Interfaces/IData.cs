namespace Car_Rental.Data.Interfaces;

public interface IData
{
    public List<T> Get<T>(Func<T, bool>? expression);
    public T? Single<T>(Func<T, bool>? expression);
    public void Add<T>(T item);

    int NextVehicleId { get; }
    int NextPersonId { get; }
    int NextBookingId { get; }

    // Används ej
    //public string[] VehicleStatusNames => Enum.GetNames(typeof(VehicleStatuses));
    //public string[] VehicleTypeNames => Enum.GetNames(typeof(VehicleTypes));
    //public VehicleTypes GetVehicleType(string name) => (VehicleTypes)Enum.Parse(typeof(VehicleTypes), name);
}
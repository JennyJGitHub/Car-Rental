using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;
using Car_Rental.Common.Extensions;

namespace Car_Rental.Common.Classes;

public class Booking : IBooking
{
    public int Id { get; init; }
    public string RegNo { get; init; } = string.Empty;
    public Customer Customer { get; init; }
    public double KmRented { get; init; }
    public double? KmReturned { get; set; } = null;
    public DateTime Rented { get; init; }
    public DateTime? Returned { get; set; } = null;
    public double? Cost { get; set; } = null;
    public IVehicle Vehicle { get; init; }
    public VehicleStatuses Status { get; set; }

    public Booking(int id, Customer customer, DateTime rented, DateTime returned, 
        double distance, IVehicle vehicle, VehicleStatuses status)
    {
        Id = id;
        RegNo = vehicle.RegNo;
        Customer = customer;
        KmRented = vehicle.Odometer;
        KmReturned = vehicle.Odometer + distance;
        Rented = rented;
        Returned = returned;
        Cost = CalculateCost(vehicle, distance);
        Vehicle = vehicle;
        Status = status;
    }

    public Booking(int id, Customer customer, DateTime rented, IVehicle vehicle, VehicleStatuses status)
    {
        Id = id;
        RegNo = vehicle.RegNo;
        Customer = customer;
        KmRented = vehicle.Odometer;
        Rented = rented;
        Vehicle = vehicle;
        Status = status;
    }

    public double CalculateCost(IVehicle vehicle, double distance)
    {
        var days = VehicleExtensions.Duration(Rented, DateTime.Today);
        return (days * vehicle.CostDay) + (distance * vehicle.CostKm);
    }
}



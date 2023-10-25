using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;

namespace Car_Rental.Common.Classes;

public class Vehicle : IVehicle
{
    public int Id { get; init; }
    public string RegNo { get; set; } = string.Empty;
    public string Make { get; set; } = string.Empty;
    public double Odometer { get; set; }
    public double CostKm { get; set; }
    public VehicleTypes Type { get; set; }
    public double CostDay { get; set; }
    public VehicleStatuses Status { get; set; }

    public Vehicle() {}

    public Vehicle(int id, string regNo, string make, double odometer, double costKm,
        VehicleTypes type, double costDay, VehicleStatuses status = VehicleStatuses.Available) =>
        (Id, RegNo, Make, Odometer, CostKm, Type, CostDay, Status) =
        (id, regNo, make, odometer, costKm, type, costDay, status);
}

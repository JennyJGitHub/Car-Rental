using Car_Rental.Common.Enums;

namespace Car_Rental.Common.Interfaces;

public interface IVehicle
{
    public int Id { get; init; }
    public string RegNo { get; set; }
    public string Make { get; set; }
    public double Odometer { get; set; }
    public double CostKm { get; set; }
    public VehicleTypes Type { get; set; }
    public double CostDay { get; set; }
    public VehicleStatuses Status { get; set; }
}

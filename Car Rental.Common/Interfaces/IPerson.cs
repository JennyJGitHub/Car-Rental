namespace Car_Rental.Common.Interfaces;

public interface IPerson
{
    public int Id { get; init; }
    public string SSN { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
}

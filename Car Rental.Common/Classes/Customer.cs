using Car_Rental.Common.Interfaces;

namespace Car_Rental.Common.Classes;

public class Customer : IPerson
{
    public int Id { get; init; }
    public string SSN { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;

    public Customer() {}

    public Customer(int id, string ssn, string lastName, string firstName) =>
        (Id, SSN, LastName, FirstName) = (id, ssn, lastName, firstName);
}
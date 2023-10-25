namespace Car_Rental.Common.Classes;

public class InputValues
{
    public string error = string.Empty;
    public string distance = string.Empty;
    public int selectedCustomer;
    public bool processing;
    public Customer newCustomer = new();
    public Vehicle vehicle = new();
}

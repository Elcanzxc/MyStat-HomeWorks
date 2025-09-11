
namespace MyRestaurant.Entitites;

public class Customer
{
    public int Id { get; set; }

    public required string FullName { get; set; }

    public string? PhoneNumber { get; set; }

    public DateTime RegistrationDate { get; set; }

    public ICollection<Order> Orders { get; set; }


    public override string ToString()
    {
        return 
$@"Id: {Id}
Full Name:{FullName}
Phone Number:{PhoneNumber}
RegistrationDate:{RegistrationDate}
";
    }

}

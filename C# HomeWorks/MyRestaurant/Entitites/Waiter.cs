namespace MyRestaurant.Entitites;

public class Waiter
{
    public int Id { get; set; }

    public required string FullName { get; set; }

    public  DateOnly HireDate { get; set; }

    public required decimal Salary { get; set; }

    public ICollection<Order> Orders { get; set; }

    public override string ToString()
    {
        return
$@"Id: {Id}
Full Name:{FullName}
Salary:{Salary}
HireDate:{HireDate}
";
    }


}

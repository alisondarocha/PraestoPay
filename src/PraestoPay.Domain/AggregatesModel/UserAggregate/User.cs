namespace PraestoPay.Domain.AggregatesModel.UserAggregate;

public class User() : Entity
{
    public User(string name, string lastName, string email) : this()
    {
        Name = name;
        LastName = lastName;
        Email = email;
    }

    public string Name { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
}

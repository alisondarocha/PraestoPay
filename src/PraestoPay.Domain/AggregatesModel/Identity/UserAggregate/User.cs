namespace PraestoPay.Domain.AggregatesModel.Identity.UserAggregate;

public class User() : Entity
{
    public User(string name, string lastName, string email, string passwordHash) : this()
    {
        Name = name;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
    }

    public string Name { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
}

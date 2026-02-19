namespace Azor.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }

    public User() { }

    public User(string code, string name)
    {
        Id = Guid.CreateVersion7();
        Code = code.ToUpper();
        Name = name;
    }
}
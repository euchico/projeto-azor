using Azor.Domain.Enums;

namespace Azor.Domain.Entities;

public class Account
{
    public Guid Id { get; set; }
    public Guid? ParentAccountId { get; set; }

    public string Code { get; set; }
    public string Name { get; set; }
    public User Owner { get; set; }
    public AccountType Type { get; set; }
    
    public decimal Balance { get; set; }
    public bool IncludeInParentBalance { get; set; }
    
    private readonly List<Account> _children = new();
    public IReadOnlyCollection<Account> Children => _children.AsReadOnly();

    public bool IsMainAccount => ParentAccountId == null;

    public Account() { }

    public Account(
        User owner, 
        string code, 
        string name, 
        AccountType type, 
        decimal initialBalance = 0, 
        bool includeInParentBalance = true,
        Guid? parentAccountId = null)
    {
        Id = Guid.CreateVersion7();
        Owner = owner;
        Code = code.ToUpper();
        Name = name;
        Type = type;
        Balance = initialBalance;
        IncludeInParentBalance = includeInParentBalance;
        ParentAccountId = parentAccountId;
    }


    public decimal AccumulatedBalance => Balance + _children
                                            .Where(c => c.IncludeInParentBalance)
                                            .Sum(c => c.Balance);

    public void AddChildAccount(Account child)
    {
        _children.Add(child);
    }

    public void Credit(decimal amount) => Balance += amount;
    public void Debit(decimal amount) => Balance -= amount;
}
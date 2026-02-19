using Azor.Application.Interfaces;
using Azor.Domain.Entities;

namespace Azor.Infrastructure.Persistence.Json;

public class AccountJsonRepository : JsonStoreBase<Account>, IAccountRepository
{
    protected override string FolderPath => Path.Combine("Data", "Asccounts");
    public void Save(Account account) => Save(account.Code, account);
    public Account? GetByCode(string code) => Load(code);
    public IEnumerable<Account> GetAll() => LoadAll();
    
}

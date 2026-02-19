using Azor.Domain.Entities;

namespace Azor.Application.Interfaces;

public interface IAccountRepository
{
    void Save(Account account);
    Account? GetByCode(string code);
    IEnumerable<Account> GetAll();
}

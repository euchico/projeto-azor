using Azor.Domain.Entities;

namespace Azor.Application.Interfaces;

public interface IUserRepository
{
    void Save(User user);
    User? GetByCode(string code);
    IEnumerable<User> GetAll();
}

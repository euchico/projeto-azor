using Azor.Application.Interfaces;
using Azor.Domain.Entities;

namespace Azor.Infrastructure.Persistence.Json;

public class UserJsonRepository : JsonStoreBase<User>, IUserRepository
{
    protected override string FolderPath => Path.Combine("Data", "Users");
    public void Save(User user) => Save(user.Code, user);
    public User? GetByCode(string code) => Load(code);
    public IEnumerable<User> GetAll() => LoadAll();
}
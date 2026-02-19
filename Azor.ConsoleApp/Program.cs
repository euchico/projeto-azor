using Azor.Domain.Entities;
using Azor.Domain.Enums;
using Azor.Infrastructure.Persistence.Json;

namespace Azor.ConsoleApp;

public class Program
{
    public static void Main(string[] args)
    {
        Console.Clear();

        var userRepository = new UserJsonRepository();
        var accountRepository = new AccountJsonRepository();

        // ===== USUÁRIO =====

        var users = userRepository.GetAll().ToList();

        User user;
        if (!users.Any())
        {
            user = new User("USR01", "Carlos");
            userRepository.Save(user);
        }
        else
        {
            user = users.First();
        }

        // ===== CONTAS =====

        var accounts = accountRepository.GetAll().ToList();

        if (!accounts.Any())
        {
            var mainAccount = new Account(
                owner: user,
                code: "MAIN",
                name: "Conta Principal",
                type: AccountType.Checking,
                initialBalance: 1500
            );

            var savings = new Account(
                owner: user,
                code: "SAV",
                name: "Poupança",
                type: AccountType.Savings,
                initialBalance: 500,
                parentAccountId: mainAccount.Id
            );

            accountRepository.Save(mainAccount);
            accountRepository.Save(savings);

            accounts.AddRange([mainAccount, savings]);
        }

        // ===== LISTAGEM =====

        PrintData(user, accounts);
    }

    private static void PrintData(User user, IEnumerable<Account> accounts)
    {
        Console.WriteLine("=== AZOR :: DADOS PERSISTIDOS ===");
        Console.WriteLine();
        Console.WriteLine($"Usuário: {user.Name} ({user.Code})");
        Console.WriteLine();

        foreach (var account in accounts)
        {
            Console.WriteLine($"Conta: {account.Name}");
            Console.WriteLine($"Código: {account.Code}");
            Console.WriteLine($"Tipo: {account.Type}");
            Console.WriteLine($"Saldo: {account.Balance:C}");
            Console.WriteLine($"Conta Pai: {(account.ParentAccountId?.ToString() ?? "—")}");
            Console.WriteLine();
        }
    }
}
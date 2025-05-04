using DualPay.Application.Services;

namespace DualPay.Application.Abstraction;
public interface IAppUserService
{
    Task CreateUserAsync(CreateAppUserRequest request);
    Task<DTOs.Token> LoginUserAsync(LoginAppUserRequest request);
    Task<bool> TransferBalanceAsync(string fromAccount, string toAccount, decimal amount);
}
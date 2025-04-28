using DualPay.Domain.Entities.Identity;

namespace DualPay.Application.Abstraction.Token;

public interface ITokenHandler
{
    Task<DTOs.Token> CreateTokenAsync(int minute, AppUser user);
}
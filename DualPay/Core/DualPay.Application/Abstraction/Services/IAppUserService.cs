using DualPay.Application.Common.Models;
using DualPay.Application.Services;

namespace DualPay.Application.Abstraction;

public interface IAppUserService
{
    Task<ApiResponse<object>> CreateUserAsync(CreateAppUserRequest request);
    Task<ApiResponse<object>> LoginUserAsync(LoginAppUserRequest request);
}
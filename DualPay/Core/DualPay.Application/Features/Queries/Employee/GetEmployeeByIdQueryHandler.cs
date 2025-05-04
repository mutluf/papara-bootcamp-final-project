using AutoMapper;
using DualPay.Application.Abstraction.Services;
using DualPay.Application.Common.Models;
using DualPay.Application.DTOs;
using DualPay.Domain.Entities;
using DualPay.Domain.Entities.Identity;
using MediatR;

namespace DualPay.Application.Features.Queries;

public class GetEmployeeByIdQueryHandler: IRequestHandler<GetEmployeeByIdRequest, ApiResponse<EmployeeDetailResponse>>
{
    private readonly IEmployeeService _employeeService;
    private readonly IMapper _mapper;

    public GetEmployeeByIdQueryHandler(IMapper mapper, IEmployeeService employeeService)
    {
        _mapper = mapper;
        _employeeService = employeeService;
    }

    public async Task<ApiResponse<EmployeeDetailResponse>> Handle(GetEmployeeByIdRequest request, CancellationToken cancellationToken)
    {
        EmployeeDto employee = await _employeeService.GetByIdAsync(request.Id, nameof(AppUser));
        EmployeeDetailResponse mapped = _mapper.Map<EmployeeDetailResponse>(employee);
        ApiResponse<EmployeeDetailResponse> response = new ApiResponse<EmployeeDetailResponse>(mapped);
        response.Message = "Success";

        return response;
    }
}

public class GetEmployeeByIdRequest : IRequest<ApiResponse<EmployeeDetailResponse>>
{
    public int Id { get; set; }
}

public class EmployeeDetailResponse
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string AccountNumber { get; set; }
    public string IdentityNumber { get; set; }
    
    public List<Expense>? Expenses { get; set; }
}
using AutoMapper;
using DualPay.Application.Abstraction.Services;
using DualPay.Application.Common.Models;
using DualPay.Application.DTOs;
using DualPay.Domain.Entities;
using MediatR;

namespace DualPay.Application.Features.Queries;

public class GetAllEmployeeQueryHandler : IRequestHandler<GetAllEmployeesQueryRequest,ApiResponse<List<EmployeeResponse>>>
{
    private readonly IMapper _mapper;
    private readonly IEmployeeService _employeeService;
    public GetAllEmployeeQueryHandler(IMapper mapper, IEmployeeService employeeService)
    {
        _mapper = mapper;
        _employeeService = employeeService;
    }

    public async Task<ApiResponse<List<EmployeeResponse>>> Handle(GetAllEmployeesQueryRequest request, CancellationToken cancellationToken)
    {
        List<EmployeeDto> employees =await _employeeService.GetAllAsync("AppUser");
        List<EmployeeResponse> mapped = _mapper.Map<List<EmployeeResponse>>(employees);
        return new ApiResponse<List<EmployeeResponse>>(mapped);
    }
}

public class GetAllEmployeesQueryRequest : IRequest<ApiResponse<List<EmployeeResponse>>>
{
}

public class EmployeeResponse
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string IdentityNumber { get; set; }
}
using AutoMapper;
using DualPay.Application.Abstraction.Services;
using DualPay.Application.Common.Models;
using DualPay.Application.DTOs;
using MediatR;

namespace DualPay.Application.Features.Commands;
public class UpdateEmployeeCommandHandler: IRequestHandler<UpdateEmployeeCommandRequest, ApiResponse>
{
    private IEmployeeService _employeeService;
    private readonly IMapper _mapper;

    public UpdateEmployeeCommandHandler(IMapper mapper, IEmployeeService employeeService)
    {
        _mapper = mapper;
        _employeeService = employeeService;
    }

    public async Task<ApiResponse> Handle(UpdateEmployeeCommandRequest request, CancellationToken cancellationToken)
    {
        EmployeeDto data = await _employeeService.GetByIdAsync(request.Id);
        if (data == null)
            return new ApiResponse("Employee not found");
        
        data.PhoneNumber = request.PhoneNumber ?? data.PhoneNumber;
        data.AccountNumber = request.AccountNumber ?? data.AccountNumber;
        
        EmployeeDto employeeDto = _mapper.Map<EmployeeDto>(data);
        await _employeeService.UpdateAsync(employeeDto);
        return new ApiResponse(message:"Employee updated");
    }
}

public class UpdateEmployeeCommandRequest: IRequest<ApiResponse>
{
    public int Id { get; set; }
    public string? PhoneNumber { get; set; }
    public string? AccountNumber { get; set; }
}
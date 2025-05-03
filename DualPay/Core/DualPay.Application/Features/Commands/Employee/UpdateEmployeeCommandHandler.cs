using AutoMapper;
using DualPay.Application.Abstraction.Services;
using DualPay.Application.Common.Models;
using DualPay.Domain.Entities;
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
        var entity = await _employeeService.GetByIdAsync(request.Id);
        if (entity == null)
            return new ApiResponse("Employee not found");

        Employee employee = _mapper.Map<Employee>(request);
         await _employeeService.UpdateAsync(employee);
        return new ApiResponse();
    }
}

public class UpdateEmployeeCommandRequest: IRequest<ApiResponse>
{
    public int Id { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? AccountNumber { get; set; }
}
using DualPay.Application.Abstraction.Services;
using DualPay.Application.Common.Models;
using DualPay.Domain.Entities;
using MediatR;

namespace DualPay.Application.Features.Commands;

public class DeleteEmployeeCommandHandler:IRequestHandler<DeleteEmployeeCommandRequest,ApiResponse>
{
    private readonly IEmployeeService  _employeeService;

    public DeleteEmployeeCommandHandler(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public async Task<ApiResponse> Handle(DeleteEmployeeCommandRequest request, CancellationToken cancellationToken)
    {
        Employee employee = await _employeeService.GetByIdAsync(request.Id);

        ApiResponse apiResponse = new ApiResponse();
        if (employee == null)
        {
            apiResponse.Message = "Employee not found";
            return apiResponse;
        }
        apiResponse.Message = "Delete employee success";
        return apiResponse;
    }
}

public class DeleteEmployeeCommandRequest : IRequest<ApiResponse>
{
    public int Id { get; set; }
}
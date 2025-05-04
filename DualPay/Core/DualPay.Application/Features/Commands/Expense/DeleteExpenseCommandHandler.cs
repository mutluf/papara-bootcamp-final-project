using DualPay.Application.Abstraction.Services;
using DualPay.Application.Common.Models;
using DualPay.Application.DTOs;
using MediatR;

namespace DualPay.Application.Features.Commands;
public class DeleteExpenseCommandHandler:IRequestHandler<DeleteExpenseCommandRequest,ApiResponse>
{
    private readonly IExpenseService _expenseService;

    public DeleteExpenseCommandHandler(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    public async Task<ApiResponse> Handle(DeleteExpenseCommandRequest request, CancellationToken cancellationToken)
    {
        ExpenseDto dto = await _expenseService.GetByIdAsync(request.Id);

        ApiResponse apiResponse = new ApiResponse();
        if (dto == null)
        {
            apiResponse.Message = "Expense not found";
            return apiResponse;
        }
        await _expenseService.DeleteByIdAsync(request.Id);
        apiResponse.Message = "Delete expense success";
        return apiResponse;
    }
}

public class DeleteExpenseCommandRequest : IRequest<ApiResponse>
{
    public int Id { get; set; }
}
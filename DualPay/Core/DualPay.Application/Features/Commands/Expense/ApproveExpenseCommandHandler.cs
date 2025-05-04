using DualPay.Application.Abstraction.Services;
using DualPay.Application.Common.Models;
using DualPay.Application.DTOs;
using DualPay.Domain.Entities;
using MediatR;

namespace DualPay.Application.Features.Commands.Expense;
public class ApproveExpenseCommandHandler : IRequestHandler<ApproveExpenseCommandRequest,ApiResponse>
{
    private readonly IExpenseService _expenseService;

    public ApproveExpenseCommandHandler(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    public async Task<ApiResponse> Handle(ApproveExpenseCommandRequest request, CancellationToken cancellationToken)
    {
        ExpenseDto dto = await _expenseService.GetByIdAsync(request.ExpenseId);
        if (dto == null)
        {
            return new ApiResponse(message: $"Expense with id {request.ExpenseId} does not exist");
        }
        
        dto.Status = ExpenseStatus.Approved;
        await _expenseService.UpdateAsync(dto);
        return new ApiResponse(message: $"Expense with id {request.ExpenseId} approved.");
    }
}

public class ApproveExpenseCommandRequest:IRequest<ApiResponse>
{
    public int ExpenseId { get; set; }
}

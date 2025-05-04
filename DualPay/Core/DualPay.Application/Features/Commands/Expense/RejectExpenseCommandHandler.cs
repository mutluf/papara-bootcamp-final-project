using DualPay.Application.Abstraction.Services;
using DualPay.Application.Common.Models;
using DualPay.Application.DTOs;
using DualPay.Domain.Entities;
using MediatR;

namespace DualPay.Application.Features.Commands.Expense;

public class RejectExpenseCommandHandler : IRequestHandler<RejectExpenseCommandRequest,ApiResponse>
{
    private readonly IExpenseService _expenseService;

    public RejectExpenseCommandHandler(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    public async Task<ApiResponse> Handle(RejectExpenseCommandRequest request, CancellationToken cancellationToken)
    {
        ExpenseDto dto = await _expenseService.GetByIdAsync(request.ExpenseId);
        if (dto == null)
        {
            return new ApiResponse(message: $"Expense with id {request.ExpenseId} does not exist");
        }
        
        dto.Status = ExpenseStatus.Rejected;
        dto.RejectionReason = request.RejectionReason;
        await _expenseService.UpdateAsync(dto);
        return new ApiResponse(message: $"Expense with id {request.ExpenseId} rejected.");
    }
}

public class RejectExpenseCommandRequest:IRequest<ApiResponse>
{
    public int ExpenseId { get; set; }
    public string RejectionReason { get; set; }
}

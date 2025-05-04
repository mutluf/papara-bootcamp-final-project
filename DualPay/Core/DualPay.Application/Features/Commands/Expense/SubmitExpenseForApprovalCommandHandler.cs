using DualPay.Application.Abstraction.Services;
using DualPay.Application.Common.Models;
using DualPay.Application.DTOs;
using DualPay.Domain.Entities;
using MediatR;

namespace DualPay.Application.Features.Commands.Expense;

public class SubmitExpenseForApprovalCommandHandler : IRequestHandler<SubmitExpenseForApprovalCommandRequest,ApiResponse>
{
    private IExpenseService  _expenseService;

    public SubmitExpenseForApprovalCommandHandler(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    public async Task<ApiResponse> Handle(SubmitExpenseForApprovalCommandRequest request, CancellationToken cancellationToken)
    {
        ExpenseDto dto = await _expenseService.GetByIdAsync(request.ExpenseId);
        if (dto == null)
        {
            return new ApiResponse(message: $"Expense with id {request.ExpenseId} does not exist");
        }

        if (dto.Status != ExpenseStatus.Pending)
        {
            return new ApiResponse(message: $"Expense with id {request.ExpenseId} already sended.");
        }
        
        dto.Status = ExpenseStatus.InProgress;
        await _expenseService.UpdateAsync(dto);
        return new ApiResponse(message: $"Expense with id {request.ExpenseId} sended.");
    }
}


public class SubmitExpenseForApprovalCommandRequest:IRequest<ApiResponse>
{
    public int ExpenseId { get; set; }
    public ExpenseStatus Status { get; set; }
}


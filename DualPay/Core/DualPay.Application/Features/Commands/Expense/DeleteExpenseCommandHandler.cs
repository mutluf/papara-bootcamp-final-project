using DualPay.Application.Abstraction.Services;
using DualPay.Application.Common.Models;
using DualPay.Domain.Entities;
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
        Expense category = await _expenseService.GetByIdAsync(request.Id);

        ApiResponse apiResponse = new ApiResponse();
        if (category == null)
        {
            apiResponse.Message = "Expense not found";
            return apiResponse;
        }
        apiResponse.Message = "Delete expense success";
        return apiResponse;
    }
}

public class DeleteExpenseCommandRequest : IRequest<ApiResponse>
{
    public int Id { get; set; }
}
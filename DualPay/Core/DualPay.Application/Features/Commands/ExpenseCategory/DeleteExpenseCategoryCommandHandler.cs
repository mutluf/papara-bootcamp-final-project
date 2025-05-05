using DualPay.Application.Abstraction.Services;
using DualPay.Application.Common.Models;
using DualPay.Application.DTOs;
using MediatR;

namespace DualPay.Application.Features.Commands.ExpenseCategories;
public class DeleteExpenseCategoryCommandHandler:IRequestHandler<DeleteExpenseCategoryCommandRequest,ApiResponse>
{
    private readonly IExpenseCategoryService  _expenseCategoryService;

    public DeleteExpenseCategoryCommandHandler(IExpenseCategoryService expenseCategoryService)
    {
        _expenseCategoryService = expenseCategoryService;
    }

    public async Task<ApiResponse> Handle(DeleteExpenseCategoryCommandRequest request, CancellationToken cancellationToken)
    {
        ExpenseCategoryDto category = await _expenseCategoryService.GetByIdAsync(request.Id);

        ApiResponse apiResponse = new ApiResponse();
        if (category == null)
        {
            apiResponse.Message = "Expense category not found";
            return apiResponse;
        }
        _expenseCategoryService.DeleteByIdAsync(request.Id);
        apiResponse.Message = "Delete Expense Category Success";
        apiResponse.Success = true;
        return apiResponse;
    }
}

public class DeleteExpenseCategoryCommandRequest : IRequest<ApiResponse>
{
    public int Id { get; set; }
}
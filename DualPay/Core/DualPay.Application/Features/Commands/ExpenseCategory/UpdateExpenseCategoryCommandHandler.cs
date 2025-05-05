using DualPay.Application.Abstraction.Services;
using DualPay.Application.Common.Models;
using DualPay.Application.DTOs;
using MediatR;

namespace DualPay.Application.Features.Commands.ExpenseCategories;
public class UpdateExpenseCategoryCommandHandler: IRequestHandler<UpdateExpenseCategoryCommandRequest, ApiResponse>
{
    private readonly IExpenseCategoryService _expenseCategoryService;

    public UpdateExpenseCategoryCommandHandler(IExpenseCategoryService expenseCategoryService)
    {
        _expenseCategoryService = expenseCategoryService;
    }

    public async Task<ApiResponse> Handle(UpdateExpenseCategoryCommandRequest request, CancellationToken cancellationToken)
    {
        ExpenseCategoryDto expenseCategoryDto = await _expenseCategoryService.GetByIdAsync(request.Id);
        if (expenseCategoryDto == null)
            return new ApiResponse("Expense category not found");

        expenseCategoryDto.Description = request.Description ?? expenseCategoryDto.Description;
        expenseCategoryDto.Name = request.Name ?? expenseCategoryDto.Name;
        await _expenseCategoryService.UpdateAsync(expenseCategoryDto);
        return new ApiResponse{Message = "Expense category updated",Success = true};
    }
}

public class UpdateExpenseCategoryCommandRequest: IRequest<ApiResponse>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}

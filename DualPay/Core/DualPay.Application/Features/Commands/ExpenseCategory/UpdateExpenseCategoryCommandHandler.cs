using AutoMapper;
using DualPay.Application.Abstraction;
using DualPay.Application.Abstraction.Services;
using DualPay.Application.Common.Models;
using DualPay.Domain.Entities;
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
        var entity = await _expenseCategoryService.GetByIdAsync(request.Id);
        if (entity == null)
            return new ApiResponse("Category not found");

        entity.Description = request.Description;
        entity.Name = request.Name;
        _expenseCategoryService.UpdateAsync(entity);
        return new ApiResponse();
    }
}

public class UpdateExpenseCategoryCommandRequest: IRequest<ApiResponse>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}

using AutoMapper;
using DualPay.Application.Abstraction;
using DualPay.Application.Abstraction.Services;
using DualPay.Application.Common.Models;
using DualPay.Application.DTOs;
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
        ExpenseCategoryDto expenseCategoryDto = await _expenseCategoryService.GetByIdAsync(request.Id);
        if (expenseCategoryDto == null)
            return new ApiResponse("Category not found");

        expenseCategoryDto.Description = request.Description;
        expenseCategoryDto.Name = request.Name;
        _expenseCategoryService.UpdateAsync(expenseCategoryDto);
        return new ApiResponse();
    }
}

public class UpdateExpenseCategoryCommandRequest: IRequest<ApiResponse>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}

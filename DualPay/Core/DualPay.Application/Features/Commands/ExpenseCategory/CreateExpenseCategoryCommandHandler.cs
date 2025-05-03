using AutoMapper;
using DualPay.Application.Abstraction;
using DualPay.Application.Abstraction.Services;
using DualPay.Application.Common.Models;
using DualPay.Application.DTOs;
using DualPay.Domain.Entities;
using MediatR;
namespace DualPay.Application.Features.Commands.ExpenseCategories;

public class CreateExpenseCategoryCommandHandler: IRequestHandler<CreateExpenseCategoryCommandRequest, ApiResponse<ExpenseCategoryResponse>>
{
    private readonly IMapper _mapper;
    private readonly IExpenseCategoryService _expenseCategoryService;

    public CreateExpenseCategoryCommandHandler(IMapper mapper, IExpenseCategoryService expenseCategoryService)
    {
        _mapper = mapper;
        _expenseCategoryService = expenseCategoryService;
    }

    public async Task<ApiResponse<ExpenseCategoryResponse>> Handle(CreateExpenseCategoryCommandRequest request, CancellationToken cancellationToken)
    {
        ExpenseCategoryDto expenseCategory = _mapper.Map<ExpenseCategoryDto>(request);
        ExpenseCategoryDto expenseCategoryDto = await _expenseCategoryService.AddAsync(expenseCategory);
        ExpenseCategoryResponse response = _mapper.Map<ExpenseCategoryResponse>(expenseCategoryDto);
        return new ApiResponse<ExpenseCategoryResponse>(response ,message:"Expense category created");
    }
}

public class CreateExpenseCategoryCommandRequest: IRequest<ApiResponse<ExpenseCategoryResponse>>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}

public class ExpenseCategoryResponse
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}
using AutoMapper;
using DualPay.Application.Abstraction.Services;
using DualPay.Application.Common.Models;
using DualPay.Domain.Entities;
using MediatR;
namespace DualPay.Application.Features.Commands.ExpenseCategories;

public class CreateExpenseCommandHandler: IRequestHandler<CreateExpenseCommandRequest, ApiResponse<ExpenseResponse>>
{
    private readonly IMapper _mapper;
    private readonly IExpenseService _expenseService;

    public CreateExpenseCommandHandler(IMapper mapper, IExpenseService expenseService)
    {
        _mapper = mapper;
        _expenseService = expenseService;
    }

    public async Task<ApiResponse<ExpenseResponse>> Handle(CreateExpenseCommandRequest request, CancellationToken cancellationToken)
    {
        Expense expense= _mapper.Map<Expense>(request);
        Expense entity = await _expenseService.AddAsync(expense);
        ExpenseResponse response = _mapper.Map<ExpenseResponse>(entity);
        return new ApiResponse<ExpenseResponse>(response ,message:"Expense created");
    }
}

public class CreateExpenseCommandRequest: IRequest<ApiResponse<ExpenseResponse>>
{
    public string Description { get; set; }
    public int ExpenseCategoryId { get; set; }
    public decimal Amount { get; set; }
    public string? DocumentUrl { get; set; }
    public string? Location { get; set; }
}

public class ExpenseResponse
{
    public string Description { get; set; }
    public int ExpenseCategoryId { get; set; }
    public decimal Amount { get; set; }
}
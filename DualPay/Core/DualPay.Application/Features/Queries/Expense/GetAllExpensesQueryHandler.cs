using AutoMapper;
using DualPay.Application.Abstraction;
using DualPay.Application.Abstraction.Services;
using DualPay.Application.Common.Models;
using DualPay.Domain.Entities;
using MediatR;

namespace DualPay.Application.Features.Queries;

public class GetAllExpenseQueryHandler : IRequestHandler<GetAllExpensesQueryRequest,ApiResponse<List<ExpenseResponse>>>
{
    private readonly IMapper _mapper;
    private readonly IExpenseService _expenseService;
    public GetAllExpenseQueryHandler(IMapper mapper, IExpenseService expenseService)
    {
        _mapper = mapper;
        _expenseService = expenseService;
    }

    public async Task<ApiResponse<List<ExpenseResponse>>> Handle(GetAllExpensesQueryRequest request, CancellationToken cancellationToken)
    {
        List<Expense> expenses =await _expenseService.GetAllAsync();
        List<ExpenseResponse> mapped = _mapper.Map<List<ExpenseResponse>>(expenses);
        return new ApiResponse<List<ExpenseResponse>>(mapped);
    }
}

public class GetAllExpensesQueryRequest : IRequest<ApiResponse<List<ExpenseResponse>>>
{
}

public class ExpenseResponse
{
    public string Description { get; set; }
    public int ExpenseCategoryId { get; set; }
    public decimal Amount { get; set; }
    public string? Location { get; set; }
}
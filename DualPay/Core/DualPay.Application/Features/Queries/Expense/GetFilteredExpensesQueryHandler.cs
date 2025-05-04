using AutoMapper;
using DualPay.Application.Abstraction.Services;
using DualPay.Application.Common.Models;
using DualPay.Domain.Entities;
using MediatR;

namespace DualPay.Application.Features.Queries;

public class GetFilteredExpensesQueryHandler : IRequestHandler<GetFilteredExpensesQuery, ApiResponse<List<ExpenseDetailResponse>>>
{
    private readonly IExpenseService _expenseService;
    private readonly IMapper _mapper;
    public GetFilteredExpensesQueryHandler(IExpenseService expenseService, IMapper mapper)
    {
        _expenseService = expenseService;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<ExpenseDetailResponse>>> Handle(GetFilteredExpensesQuery request, CancellationToken cancellationToken)
    {
        var expenses = await _expenseService.GetByFilterAsync(
            request.Filters,
            nameof(Employee)
        );
        if (expenses == null || expenses.Count == 0)
        {
            return new ApiResponse<List<ExpenseDetailResponse>>(new(),"No expenses found with the provided filters.");
        }
        var responses = _mapper.Map<List<ExpenseDetailResponse>>(expenses);
        
        return new ApiResponse<List<ExpenseDetailResponse>>(responses);
    }
}

public class GetFilteredExpensesQuery : IRequest<ApiResponse<List<ExpenseDetailResponse>>>
{
    public Dictionary<string, object> Filters { get; set; }
    public List<string> Includes { get; set; }

    public GetFilteredExpensesQuery(Dictionary<string, object> filters, List<string> includes)
    {
        Filters = filters;
        Includes = includes;
    }
}
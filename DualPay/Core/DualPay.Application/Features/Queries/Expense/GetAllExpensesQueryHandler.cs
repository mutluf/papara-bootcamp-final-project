using AutoMapper;
using DualPay.Application.Abstraction.Services;
using DualPay.Application.Common.Models;
using DualPay.Application.DTOs;
using DualPay.Domain.Entities;
using MediatR;

namespace DualPay.Application.Features.Queries;

public class GetAllExpenseQueryHandler : IRequestHandler<GetAllExpensesQueryRequest,ApiResponse<List<ExpenseResponse>>>
{
    private readonly IMapper _mapper;
    private readonly IExpenseService _expenseService;
    private readonly IEmployeeService _employeeService;
    public GetAllExpenseQueryHandler(IMapper mapper, IExpenseService expenseService, IEmployeeService employeeService)
    {
        _mapper = mapper;
        _expenseService = expenseService;
        _employeeService = employeeService;
    }

    public async Task<ApiResponse<List<ExpenseResponse>>> Handle(GetAllExpensesQueryRequest request, CancellationToken cancellationToken)
    {
        List<ExpenseDto> expenseDtos = new List<ExpenseDto>();
        if (request.UserId == null)
        {
            expenseDtos = await _expenseService.Where(e=> e.Status != ExpenseStatus.Pending);
        }
        else
        {
            var employee = await _employeeService.Where(e => e.UserId == Int32.Parse(request.UserId));
            expenseDtos = await _expenseService.Where(e=> e.EmployeeId == employee[0].Id);
        }
      
        List<ExpenseResponse> mapped = _mapper.Map<List<ExpenseResponse>>(expenseDtos);
        return new ApiResponse<List<ExpenseResponse>>(mapped);
    }
}

public class GetAllExpensesQueryRequest : IRequest<ApiResponse<List<ExpenseResponse>>>
{
    public string UserId { get; set; }
}

public class ExpenseResponse
{
    public string Description { get; set; }
    public int ExpenseCategoryId { get; set; }
    public decimal Amount { get; set; }
    public string? Location { get; set; }
}
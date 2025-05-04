using AutoMapper;
using DualPay.Application.Abstraction.Services;
using DualPay.Application.Common.Models;
using DualPay.Application.DTOs;
using MediatR;

namespace DualPay.Application.Features.Commands.ExpenseCategories;
public class CreateExpenseCommandHandler: IRequestHandler<CreateExpenseCommandRequest, ApiResponse<ExpenseResponse>>
{
    private readonly IMapper _mapper;
    private readonly IExpenseService _expenseService;
    private readonly IEmployeeService _employeeService;

    public CreateExpenseCommandHandler(IMapper mapper, IExpenseService expenseService, IEmployeeService employeeService)
    {
        _mapper = mapper;
        _expenseService = expenseService;
        _employeeService = employeeService;
    }

    public async Task<ApiResponse<ExpenseResponse>> Handle(CreateExpenseCommandRequest request, CancellationToken cancellationToken)
    {
        ExpenseDto dto= _mapper.Map<ExpenseDto>(request);

        var employee = await _employeeService.Where(e => e.UserId == request.UserId);
        dto.EmployeeId = employee[0].Id;
        
        ExpenseDto expenseDto = await _expenseService.AddAsync(dto);
        ExpenseResponse response = _mapper.Map<ExpenseResponse>(expenseDto);
        return new ApiResponse<ExpenseResponse>(response ,message:"Expense created");
    }
}

public class CreateExpenseCommandRequest: IRequest<ApiResponse<ExpenseResponse>>
{
    public int UserId { get; set; }
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
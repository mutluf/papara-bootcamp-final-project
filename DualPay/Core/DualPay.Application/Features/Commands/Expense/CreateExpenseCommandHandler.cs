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

    public CreateExpenseCommandHandler(IMapper mapper, IExpenseService expenseService)
    {
        _mapper = mapper;
        _expenseService = expenseService;
    }

    public async Task<ApiResponse<ExpenseResponse>> Handle(CreateExpenseCommandRequest request, CancellationToken cancellationToken)
    {
        ExpenseDto dto= _mapper.Map<ExpenseDto>(request);
        ExpenseDto expenseDto = await _expenseService.AddAsync(dto);
        ExpenseResponse response = _mapper.Map<ExpenseResponse>(expenseDto);
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
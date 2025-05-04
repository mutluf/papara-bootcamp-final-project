using DualPay.Application.Abstraction;
using DualPay.Application.Abstraction.Services;
using DualPay.Application.Common.Models;
using DualPay.Application.DTOs;
using DualPay.Application.Events;
using DualPay.Domain.Entities;
using DualPay.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DualPay.Application.Features.Commands.Expense;
public class ApproveExpenseCommandHandler : IRequestHandler<ApproveExpenseCommandRequest,ApiResponse>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IExpenseService _expenseService;
    private IEventPublishService _eventPublishService;
    private readonly IEmployeeService _employeeService;

    public ApproveExpenseCommandHandler(IExpenseService expenseService, IEventPublishService eventPublishService, IEmployeeService employeeService, UserManager<AppUser> userManager)
    {
        _expenseService = expenseService;
        _eventPublishService = eventPublishService;
        _employeeService = employeeService;
        _userManager = userManager;
    }

    public async Task<ApiResponse> Handle(ApproveExpenseCommandRequest request, CancellationToken cancellationToken)
    {
        ExpenseDto dto = await _expenseService.GetByIdAsync(request.ExpenseId);
        if (dto == null)
        {
            return new ApiResponse(message: $"Expense with id {request.ExpenseId} does not exist");
        }
              
        dto.Status = ExpenseStatus.Approved;
        dto.ApprovedDate = DateTime.UtcNow;
        await _expenseService.UpdateAsync(dto);
        
        var employee = await _employeeService.GetByIdAsync(dto.EmployeeId);
        var admins = await _userManager.GetUsersInRoleAsync("Admin");
        if (employee != null && admins.Count !=0)
        {
            string fromAccount = admins.First().AccountNumber;
            ExpenseApprovedEvent @event = new ExpenseApprovedEvent()
            {
                Amount = dto.Amount,
                ToAccount = employee.AccountNumber,
                FromAccount = fromAccount,
            };
            await _eventPublishService.PublishAsync(@event);
        }
        return new ApiResponse(message: $"Expense with id {request.ExpenseId} approved.");
    }
}

public class ApproveExpenseCommandRequest:IRequest<ApiResponse>
{
    public int  ExpenseId { get; set; }
}
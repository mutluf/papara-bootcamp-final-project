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
    
    private readonly IEmployeeService _employeeService;
    private readonly IJobService _jobService;

    public ApproveExpenseCommandHandler(IExpenseService expenseService, IEmployeeService employeeService, UserManager<AppUser> userManager, IJobService jobService)
    {
        _expenseService = expenseService;
        _employeeService = employeeService;
        _userManager = userManager;
        _jobService = jobService;
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
                ExpenseId = request.ExpenseId,
            };
            _jobService.ScheduleSendExpenseToPaymentAsync(@event, request.PaymentDate);
            //await _eventPublishService.PublishAsync(@event);
        }
        return new ApiResponse(message: $"Expense with id {request.ExpenseId} approved.");
    }
}

public class ApproveExpenseCommandRequest:IRequest<ApiResponse>
{
    public int  ExpenseId { get; set; }
    public DateTime PaymentDate { get; set; }
}
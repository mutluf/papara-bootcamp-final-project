using DualPay.Application.Abstraction.Services;
using DualPay.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DualPay.Application.Features.Commands.Expense;

public class CreateExpenseCommandHandler : IRequestHandler<UploadExpenseFileCommandRequest>
{
    private readonly IExpenseService _expenseService;

    public CreateExpenseCommandHandler(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    public async Task Handle(UploadExpenseFileCommandRequest request, CancellationToken cancellationToken)
    {
       
        var rootPath = Path.Combine(Directory.GetCurrentDirectory(), "files");
        
        if (!Directory.Exists(rootPath))
        {
            Directory.CreateDirectory(rootPath);
        }
        
        var fileName = $"{Guid.NewGuid()}_{request.File.FileName}";
       
        var filePath = Path.Combine(rootPath, fileName);

        await using var fileStream = new FileStream(filePath, FileMode.Create);
        await request.File.CopyToAsync(fileStream, cancellationToken);
        var expense = await _expenseService.GetByIdAsync(request.ExpenseId);
        expense.DocumentUrl = filePath;
        await _expenseService.UpdateAsync(expense);
    }
}

public class UploadExpenseFileCommandRequest : IRequest
{
    public int ExpenseId { get; set; }
    
    public required IFormFile File { get; set; }
    
}
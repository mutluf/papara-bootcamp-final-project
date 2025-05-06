using DualPay.Application.Abstraction.Services;
using MediatR;

namespace DualPay.Application.Features.Commands.Expense;

public class
    GetExpenseDocumentQueryHandler : IRequestHandler<GetExpenseDocumentQueryRequest, GetExpenseDocumentQueryResponse>
{
    private readonly IExpenseService _expenseService;

    public GetExpenseDocumentQueryHandler(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    public async Task<GetExpenseDocumentQueryResponse> Handle(GetExpenseDocumentQueryRequest request, CancellationToken cancellationToken)
    {
        var expense = await _expenseService.GetByIdAsync(request.ExpenseId);
        var fileBytes = await File.ReadAllBytesAsync(expense.DocumentUrl, cancellationToken);
        GetExpenseDocumentQueryResponse response = new()
        {
            File = fileBytes,
        };

        return response;
    }
}

public class GetExpenseDocumentQueryResponse 
{
    public byte[] File { get; set; }
}

public class GetExpenseDocumentQueryRequest : IRequest<GetExpenseDocumentQueryResponse>
{
    public int ExpenseId { get; set; }
}
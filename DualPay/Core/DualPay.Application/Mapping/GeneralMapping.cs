using AutoMapper;
using DualPay.Application.Common.Models.Requests;
using DualPay.Application.Models.Responses;
using DualPay.Domain.Entities;

namespace DualPay.Application.Mapping;

public class GeneralMapping :Profile
{
    public GeneralMapping()
    {
        CreateMap<ExpenseCategory, ExpenseCategoryResponse>();
        CreateMap<CreateExpenseCategoryRequest, ExpenseCategory>();
        CreateMap<UpdateExpenseCategoryRequest, ExpenseCategory>();
        
        CreateMap<Expense, ExpenseResponse>()
            .ForMember(dest => dest.CreatedByName, opt => opt.MapFrom(src => src.AppUser.Name));
        CreateMap<CreateExpenseRequest, Expense>();
        CreateMap<UpdateExpenseRequest, Expense>();
        
        CreateMap<Employee, EmployeeResponse>();
        CreateMap<CreateEmployeeRequest, Employee>();
        CreateMap<UpdateEmployeeRequest, Employee>();
        
    }
}
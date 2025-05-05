using DualPay.API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace DualPay.API.Attributes;

public class AuthorizeEmployeeForOwnExpenseAttribute : TypeFilterAttribute
{
    public AuthorizeEmployeeForOwnExpenseAttribute() : base(typeof(AuthorizeOwnEmployeeFilter)) { }
}
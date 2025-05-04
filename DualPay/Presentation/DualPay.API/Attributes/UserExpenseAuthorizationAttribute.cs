using DualPay.API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace DualPay.API.Attributes;

public class UserExpenseAuthorizationAttribute : TypeFilterAttribute
{
    public UserExpenseAuthorizationAttribute() : base(typeof(UserExpenseAuthorizationFilter)) { }
}
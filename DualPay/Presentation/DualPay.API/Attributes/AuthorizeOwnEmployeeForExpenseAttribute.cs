using DualPay.API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace DualPay.API.Attributes;

public class AuthorizeOwnEmployeeAttribute : TypeFilterAttribute
{
    public AuthorizeOwnEmployeeAttribute() : base(typeof(AuthorizeOwnEmployeeFilter)) { }
}
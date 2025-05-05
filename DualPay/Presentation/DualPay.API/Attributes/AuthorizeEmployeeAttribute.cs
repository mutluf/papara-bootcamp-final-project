using DualPay.API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace DualPay.API.Attributes;
public class AuthorizeEmployeeAttribute : TypeFilterAttribute
{
    public AuthorizeEmployeeAttribute() : base(typeof(AuthorizeEmployeeFilter)) { }
}
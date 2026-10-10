using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Server.WebApi.Exceptions;
using WeChatAuto.Exceptions;

namespace Server.WebApi.Filters;

/// <summary>
/// 全局异常过滤器：未知客户端 → 404，其余异常 → 500，统一输出 ProblemDetails。
/// </summary>
public sealed class ApiExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var statusCode = context.Exception switch
        {
            WeChatClientNotFoundException => StatusCodes.Status404NotFound,
            WechatClientNotExistException => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError,
        };

        context.Result = new ObjectResult(new ProblemDetails
        {
            Status = statusCode,
            Title = statusCode == StatusCodes.Status404NotFound ? "微信客户端不存在" : "服务器内部错误",
            Detail = context.Exception.Message,
        })
        {
            StatusCode = statusCode,
        };

        context.ExceptionHandled = true;
    }
}

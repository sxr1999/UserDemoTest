using System.Reflection;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace UserMgr.WebApi;

public class UnitOfWorkFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var result = await next();
        if (result.Exception!=null)
        {
            //发生异常
            return;
        }

        var actionDes = context.ActionDescriptor as ControllerActionDescriptor;
        if (actionDes==null)
        {
            return;
        }
        //拿到attribute
        var attribute = actionDes.MethodInfo.GetCustomAttribute<UnitOfWorkAttribute>();
        if (attribute==null)
        {
            return;
        }

        foreach (var item in attribute._dbContextTypes)
        {
            //从DI中拿到DbContext实例
            var dbContext = context.HttpContext.RequestServices.GetService(item) as DbContext;
            if (dbContext!=null)
            {
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
// <summary>
// <copyright file="CustomActionFilterAttribute.cs" company="Axity">
// This source code is Copyright Axity and MAY NOT be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from Axity (www.axity.com).
// </copyright>
// </summary>

namespace Axity.Users.Api.Filter
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using Serilog;

    /// <summary>
    /// Custom Action Filter Attribute.
    /// </summary>
    /// <param name="logger">Seriilog.</param>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomActionFilterAttribute(ILogger logger)
        : ActionFilterAttribute
    {
        /// <inheritdoc/>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            this.Log("OnActionExecuting", context.RouteData);
        }

        /// <inheritdoc/>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            this.Log("OnActionExecuted", context.RouteData);
        }

        /// <inheritdoc/>
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            this.Log("OnResultExecuting", context.RouteData);
        }

        /// <inheritdoc/>
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            this.Log("OnResultExecuted", context.RouteData);
        }

        private void Log(string methodName, RouteData routeData)
        {
            var controllerName = routeData.Values["controller"];
            var actionName = routeData.Values["action"];

            var message = string.Format("{0} Controller: {1} Action:{2}", methodName, controllerName, actionName);
            logger.Information(message);
        }
    }
}

using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreRequestServicesNullBug
{
    public class CustomHeaderMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceScopeFactory _serviceFactory;

        public CustomHeaderMiddleware(RequestDelegate next, IServiceScopeFactory serviceFactory)
        {
            _next = next;
            _serviceFactory = serviceFactory;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.OnStarting(() =>
            {
                // not sure about performance of this
                using (var services = _serviceFactory.CreateScope())
                {
                    var service = services.ServiceProvider.GetService<ISomeService>();
                    service.SetHeader(context.Response);
                }
                
                return Task.CompletedTask;
            });

            await _next.Invoke(context).ConfigureAwait(false);
        }
    }

    public class NotWorkingHeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public NotWorkingHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.OnStarting(() =>
            {
                // context.RequestServices always null, I'm not adding null-check to see actual exception.
                var service = context.RequestServices.GetService<ISomeService>();
                service.SetHeader(context.Response);
                return Task.CompletedTask;
            });

            await _next.Invoke(context).ConfigureAwait(false);
        }
    }
}
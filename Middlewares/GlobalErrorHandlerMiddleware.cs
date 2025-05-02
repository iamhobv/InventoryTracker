
using InventoryTracker.Data;

namespace InventoryTracker.Middlewares
{
    public class GlobalErrorHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalErrorHandlerMiddleware> logger;

        public GlobalErrorHandlerMiddleware(ILogger<GlobalErrorHandlerMiddleware> logger)
        {
            this.logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);

                GeneralResponse errorResponse = new GeneralResponse { IsPass = false, Data = "Unexpected Error!" };

                await context.Response.WriteAsJsonAsync(errorResponse);
            }
        }
    }
}

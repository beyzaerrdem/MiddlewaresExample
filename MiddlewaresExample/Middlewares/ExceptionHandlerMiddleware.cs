namespace MiddlewaresExample.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionHandlerMiddleware> logger;
        public ExceptionHandlerMiddleware(RequestDelegate Next, ILogger<ExceptionHandlerMiddleware> Logger)
        {
            next = Next;
            logger = Logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await next.Invoke(httpContext); // oluşabilecek handle edilmemiş tüm hataları yakalamak için
            }
            catch (Exception ex)
            {
                //hata yönetimi
                logger.LogError(ex.Message);
            }

        }
    }
}

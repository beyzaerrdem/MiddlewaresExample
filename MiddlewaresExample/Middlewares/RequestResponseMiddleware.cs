using System.Text;

namespace MiddlewaresExample.Middlewares
{
    public class RequestResponseMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<RequestResponseMiddleware> logger;
        public RequestResponseMiddleware(RequestDelegate Next, ILogger<RequestResponseMiddleware> Logger)
        {
            next = Next;
            logger = Logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            //request

            var orjinalBodyStream = httpContext.Response.Body;

            logger.LogInformation($"Query Keys: { httpContext.Request.QueryString}");

            MemoryStream requestBody = new MemoryStream();
            await httpContext.Request.Body.CopyToAsync(requestBody);
            requestBody.Seek(0, SeekOrigin.Begin);
            string requestText = await new StreamReader(requestBody).ReadToEndAsync();
            requestBody.Seek(0, SeekOrigin.Begin);

            var tempStream = new MemoryStream();
            httpContext.Response.Body = tempStream; //bodyi okuyabilmek için boş stream

           
            await next.Invoke(httpContext); //response oluşuyor


            //response alınacak
            httpContext.Response.Body.Seek(0, SeekOrigin.Begin); //streamin başından itibaren okumaya başlamak için 
            string responseText = await new StreamReader(httpContext.Response.Body, Encoding.UTF8).ReadToEndAsync();
            httpContext.Response.Body.Seek(0, SeekOrigin.Begin);

            await httpContext.Response.Body.CopyToAsync(orjinalBodyStream);

            logger.LogInformation($"Request: {requestText}");
            logger.LogInformation($"Response: {responseText}");
        }
    }
}

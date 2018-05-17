using Microsoft.AspNetCore.Http;

namespace AspNetCoreRequestServicesNullBug
{
    public interface ISomeService
    {
        void SetHeader(HttpResponse response);
    }

    public class DefaultSomeService : ISomeService
    {
        /// <inheritdoc />
        public void SetHeader(HttpResponse response)
        {
            response.Headers.Add("X-Hello", "Hello World!");
        }
    }
}
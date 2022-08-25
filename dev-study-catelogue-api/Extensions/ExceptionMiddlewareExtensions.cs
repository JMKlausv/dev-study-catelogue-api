using dev_study_catelogue_api.Middlewares;

namespace dev_study_catelogue_api.Extensions
{
    public static  class ExceptionMiddlewareExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this WebApplication app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}


namespace Apps.Utilities._ClientInfo
{
    public sealed class _ClientInfo
    {
        public static string? IpAddress(HttpContext? httpContext)
        {
            if (httpContext == null)
            {
                return null;
            }

            return httpContext.Connection.RemoteIpAddress?.ToString();
        }

        public static string? UserAgent(HttpContext? httpContext)
        {
            if (httpContext == null)
            {
                return null;
            }
            
            return httpContext.Request.Headers["User-Agent"].ToString();
        }
    }
}
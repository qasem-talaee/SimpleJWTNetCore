using Microsoft.IdentityModel.Tokens;

namespace SimpleJWTNetCore.UI.Helper
{
    public class JWTTokenMiddleWare
    {
        private readonly RequestDelegate _next;
        public JWTTokenMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string token = context.Request.Cookies["Token"];
            if (!token.IsNullOrEmpty())
            {
                context.Request.Headers["Authorization"] = "Bearer " + token;
            }
            await _next(context);
        }
    }
}

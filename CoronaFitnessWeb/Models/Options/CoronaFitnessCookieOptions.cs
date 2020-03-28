using Microsoft.AspNetCore.Http;

namespace CoronaFitnessWeb.Models.Options
{
    public class CoronaFitnessCookieOptions : ICoronaFitnessCookieOptions
    {
        public string AuthCookie { get; set; }

        public bool IsAuthenticated(HttpContext context)
        {
            return context.Request.Cookies.ContainsKey(this.AuthCookie);
        }
    }
}
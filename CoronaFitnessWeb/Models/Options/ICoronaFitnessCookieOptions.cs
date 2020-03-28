using Microsoft.AspNetCore.Http;

namespace CoronaFitnessWeb.Models.Options
{
    public interface ICoronaFitnessCookieOptions
    {
        string AuthCookie { get; set; }

        bool IsAuthenticated(HttpContext context);
    }
}
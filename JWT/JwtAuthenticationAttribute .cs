using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using webAPIandMVC.DBContext;

public class JwtAuthenticationAttribute : AuthorizationFilterAttribute
{
    readonly LoginSignUpDB_Context db = new LoginSignUpDB_Context();
    public override void OnAuthorization(HttpActionContext actionContext)
    {
        var authHeader = actionContext.Request.Headers.Authorization;
        if (authHeader == null || authHeader.Scheme != "Bearer")
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Token is missing");
            return;
        }

        var token = authHeader.Parameter;
        var validator = new JwtTokenValidator();

        try
        {
            var principal = validator.ValidateToken(token);
            actionContext.RequestContext.Principal = principal;
            var userIdClaim = principal?.FindFirst("UserId");
            if (userIdClaim != null)
            {
                var userId = userIdClaim.Value;
                var FindUser = db.userData.FirstOrDefault(user => user.ID.ToString() == userId);

                actionContext.Request.Properties["UserId"] = FindUser.ID;
                actionContext.Request.Properties["UserRole"] = FindUser.email;

                System.Console.WriteLine($"Authenticated UserId: {FindUser}");
            }
        }
        catch
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid token");
        }
    }
}

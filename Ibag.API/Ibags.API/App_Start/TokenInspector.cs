using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using Ibags.API.App_Start;
using System.Security.Principal;

namespace Ibags.API.App_Start
{
    public class TokenInspector : DelegatingHandler
    {
        const string TOKEN_NAME = "X-Token";

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Headers.Contains(TOKEN_NAME))
            {
                string encryptedToken = request.Headers.GetValues(TOKEN_NAME).First();
                try
                {
                    Token token = Token.Decrypt(encryptedToken);
                    IbagsDbContext db = new IbagsDbContext();
                    bool isValidUserId = db.AccountSet.Any(u => u.AccountId == token.AccountId);
                    bool requestIPMatchesTokenIP = token.IP.Equals(request.GetClientIP());

                    if (!isValidUserId || !requestIPMatchesTokenIP)
                    {
                        HttpResponseMessage reply = request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Invalid identity or client machine.");
                        return Extensions.FromResult(reply);
                    }
                }
                catch (Exception ex)
                {
                    HttpResponseMessage reply = request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Invalid token.");
                    return Extensions.FromResult(reply);
                }
            }
            else
            {
                HttpResponseMessage reply = request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Request is missing authorization token.");
                return Extensions.FromResult(reply);
            }

            return base.SendAsync(request, cancellationToken);
        }

        public static Token GetToken(HttpRequestMessage request) {
            string encryptedToken = request.Headers.GetValues(TOKEN_NAME).First();
            return Token.Decrypt(encryptedToken);
        }
    }
}
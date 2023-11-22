using ZoomMeeting.Interfaces;
using Flurl;
using Flurl.Http;
using ZoomMeeting.Models;

namespace ZoomMeeting.Services
{
    public class TokenService : ITokenService
    {
        public string GetAuthorizationUri(string clientId, string redirectUri)
        {
            return "https://zoom.us/oauth/authorize"
                .AppendQueryParam("response_type", "code")
                .AppendQueryParam("client_id", clientId)
                .AppendQueryParam("redirect_uri", redirectUri)
                .ToString();
        }

        public async Task<(string AccessToken, string RefreshToken)> GetTokenPair(TokenRequest tokenRequest)
        {
            var response = await "https://zoom.us/oauth/token"
                .WithBasicAuth(tokenRequest.ClientId, tokenRequest.ClientSecret)
                .WithHeader("Content-Type", "application/x-www-form-urlencoded")
                .PostUrlEncodedAsync(new { 
                    code = tokenRequest.AuthorizationCode,
                    grant_type = "authorization_code",
                    redirect_uri = tokenRequest.RedirectUri,
                });

            response.ResponseMessage.EnsureSuccessStatusCode();

            var tokens = await response.GetJsonAsync<TokenResponse>();
            return (tokens.access_token, tokens.refresh_token);
        }
    }
}


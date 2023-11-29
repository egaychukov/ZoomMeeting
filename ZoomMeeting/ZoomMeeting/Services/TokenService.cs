using ZoomMeeting.Interfaces;
using Flurl;
using Flurl.Http;
using ZoomMeeting.Models;
using System.IdentityModel.Tokens.Jwt;

namespace ZoomMeeting.Services
{
    public class TokenService : ITokenService
    {
        private const string ZoomOAuthBaseUrl = "https://zoom.us/oauth";
        public string GetAuthorizationUrl(string clientId, string redirectUri)
        {
            return Url.Combine(ZoomOAuthBaseUrl, "authorize")
                .AppendQueryParam("response_type", "code")
                .AppendQueryParam("client_id", clientId)
                .AppendQueryParam("redirect_uri", redirectUri)
                .ToString();
        }

        public async Task<(string AccessToken, string RefreshToken)> GetTokenPair(TokenRequest tokenRequest)
        {
            var response = await Url.Combine(ZoomOAuthBaseUrl, "token")
                .WithBasicAuth(tokenRequest.ClientId, tokenRequest.ClientSecret)
                .PostUrlEncodedAsync(new { 
                    code = tokenRequest.AuthorizationCode,
                    grant_type = "authorization_code",
                    redirect_uri = tokenRequest.RedirectUri,
                });

            response.ResponseMessage.EnsureSuccessStatusCode();

            var tokens = await response.GetJsonAsync<TokenResponse>();
            return (tokens.access_token, tokens.refresh_token);
        }

        public async Task<string> RefreshAccessToken(RefreshTokenRequest request)
        {
            var response = await Url.Combine(ZoomOAuthBaseUrl, "token")
                .WithBasicAuth(request.ClientId, request.ClientSecret)
                .PostUrlEncodedAsync(new {
                    grant_type = "refresh_token",
                    refresh_token = request.RefreshToken,
                });

            response.ResponseMessage.EnsureSuccessStatusCode();
           
            var responseBody = await response.GetJsonAsync<RefreshTokenResponse>();
            return responseBody.access_token;
        }

        public bool TokenExpired(string token)
        {
            return DateTime.UtcNow >= GetTokenExirationDate(token);
        }

        private DateTime GetTokenExirationDate(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            return jwtToken.ValidTo;
        }
    }
}


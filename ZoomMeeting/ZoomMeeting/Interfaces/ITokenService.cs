using ZoomMeeting.Models;

namespace ZoomMeeting.Interfaces
{
    public interface ITokenService
    {
        string GetAuthorizationUrl(string cliendId, string redirectUri);
        Task<(string AccessToken, string RefreshToken)> GetTokenPair(TokenRequest tokenRequest);
        Task<string> RefreshAccessToken(RefreshTokenRequest request);
        bool TokenExpired(string token);
    }
}

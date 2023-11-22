using ZoomMeeting.Models;

namespace ZoomMeeting.Interfaces
{
    public interface ITokenService
    {
        string GetAuthorizationUri(string cliendId, string redirectUri);
        Task<(string AccessToken, string RefreshToken)> GetTokenPair(TokenRequest tokenRequest);
        Task<string> RefreshAccessToken(RefreshTokenRequest request);
    }
}

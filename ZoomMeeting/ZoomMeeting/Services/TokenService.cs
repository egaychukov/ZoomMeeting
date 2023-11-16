using ZoomMeeting.Interfaces;


namespace ZoomMeeting.Services
{
    public class TokenService : ITokenService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TokenService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public (string AccessToken, string ResreshToken) GetTokenPair(string cliendId, string redirectUri)
        {
            throw new NotImplementedException();
        }
    }
}

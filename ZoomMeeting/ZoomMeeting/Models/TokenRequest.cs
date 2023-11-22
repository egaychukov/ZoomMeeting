namespace ZoomMeeting.Models
{
    public class TokenRequest
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string AuthorizationCode { get; set; }
        public string RedirectUri { get; set; }
    }
}

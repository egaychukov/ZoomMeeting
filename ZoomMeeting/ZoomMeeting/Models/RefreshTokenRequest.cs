namespace ZoomMeeting.Models
{
    public class RefreshTokenRequest
    {
        public string? RefreshToken { get; set; }
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }
    }
}

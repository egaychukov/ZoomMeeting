namespace ZoomMeeting.Models
{
    public class MeetingCreationResponse
    {
        public long id { get; set; }
        public string join_url { get; set; }
        public string host_email { get; set; }
    }
}

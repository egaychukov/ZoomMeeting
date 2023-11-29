namespace ZoomMeeting.Models
{
    public class Meeting
    {
        public string topic { get; set; }
        public int duration { get; set; }
        public DateTime start_time { get; set; }
        public string agenda { get; set; }
    }
}

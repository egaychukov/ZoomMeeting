using ZoomMeeting.Models;

namespace ZoomMeeting.Interfaces
{
    public interface IMeetingService
    {
        Task<MeetingCreationResponse> CreateMeetingAsync(Meeting meeting, string token);
        Task DeleteMeetingAsync(string meetingId, string token);
        Task UpdateMeetingAsync(Meeting meeting, string meetingId, string token);
    }
}

using Flurl.Http;
using Flurl;
using ZoomMeeting.Interfaces;
using ZoomMeeting.Models;

namespace ZoomMeeting.Services
{
    public class MeetingService : IMeetingService
    {
        private const string MeetingApiBaseUrl = "https://api.zoom.us/v2";

        public async Task<MeetingCreationResponse> CreateMeetingAsync(Meeting meeting, string token)
        {
            var response = await Url.Combine(MeetingApiBaseUrl, "users/me/meetings")
                .WithOAuthBearerToken(token)
                .PostJsonAsync(meeting);

            response.ResponseMessage.EnsureSuccessStatusCode();

            return await response.GetJsonAsync<MeetingCreationResponse>();
        }

        public async Task DeleteMeetingAsync(string meetingId, string token)
        {
            var response = await Url.Combine(MeetingApiBaseUrl, "meetings")
                .WithOAuthBearerToken(token)
                .AppendPathSegment(meetingId)
                .DeleteAsync();

            response.ResponseMessage.EnsureSuccessStatusCode();
        }

        public async Task UpdateMeetingAsync(Meeting meeting, string meetingId, string token)
        {
            var response = await Url.Combine(MeetingApiBaseUrl, "meetings")
                .WithOAuthBearerToken(token)
                .AppendPathSegment(meetingId)
                .PatchJsonAsync(meeting);

            response.ResponseMessage.EnsureSuccessStatusCode();
        }
    }
}

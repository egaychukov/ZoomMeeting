using Microsoft.AspNetCore.Mvc;
using ZoomMeeting.Interfaces;
using ZoomMeeting.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly IMeetingService _meetingService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private const string RedirectUri = "";
        private const string ClientId = "";
        private const string ClientSecret = "";

        public HomeController(ITokenService tokenService, IMeetingService meetingService,
            IHttpContextAccessor httpContextAccessor)
        {
            _tokenService = tokenService;
            _meetingService = meetingService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Authorization(string? code)
        {
            if (code == null)
            {
                return Redirect(_tokenService.GetAuthorizationUrl(ClientId, RedirectUri));
            }
            var (accessToken, _) = await _tokenService.GetTokenPair(GenerateTokenRequest(code));
            return RedirectToAction("Index", new { token = accessToken });
        }

        private TokenRequest GenerateTokenRequest(string authorizationCode)
            => new TokenRequest()
            {
                AuthorizationCode = authorizationCode,
                RedirectUri = RedirectUri,
                ClientId = ClientId,
                ClientSecret = ClientSecret
            };

        public async Task<IActionResult> Index(string? token)
        {
            if (token == null || _tokenService.TokenExpired(token))
            {
                return RedirectToAction("Authorization");
            }

            ViewData["AccessToken"] = token;
            return View(_httpContextAccessor.HttpContext!.Request.Cookies);
        }

        public async Task<IActionResult> CreateMeeting(string token)
        {
            var response = await _meetingService.CreateMeetingAsync(GenerateMeetingData(), token);
            _httpContextAccessor.HttpContext!.Response.Cookies.Append(response.id.ToString(), response.join_url);
            return RedirectToAction("Index", new { token = token });
        }

        private Meeting GenerateMeetingData()
            => new Meeting
            {
                topic = "TestTopic",
                agenda = "TestAgenda",
                duration = 120,
                start_time = DateTime.UtcNow.AddDays(1)
            };

        public async Task<IActionResult> DeleteMeeting(string meetingId, string token)
        {
            await _meetingService.DeleteMeetingAsync(meetingId, token);
            _httpContextAccessor.HttpContext!.Response.Cookies.Delete(meetingId);
            return RedirectToAction("Index", new { token = token });
        }

        public async Task<IActionResult> UpdateMeeting(string meetingId, string token)
        {
            await _meetingService.UpdateMeetingAsync(GenerateUpdatedMeeting(), meetingId, token);
            return RedirectToAction("Index", new { token = token });
        }

        private Meeting GenerateUpdatedMeeting()
            => new Meeting
            {
                topic = "Updated_Topic",
                agenda = "Updated_Agenda",
                duration = 240,
                start_time = DateTime.UtcNow.AddDays(3)
            };
    }
}
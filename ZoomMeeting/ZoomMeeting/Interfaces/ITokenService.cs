using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoomMeeting.Interfaces
{
    internal interface ITokenService
    {
        (string AccessToken, string ResreshToken) GetTokenPair(string cliendId, string redirectUri);
    }
}

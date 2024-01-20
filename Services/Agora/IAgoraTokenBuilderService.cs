using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYPM.Services.Agora
{
    public interface IAgoraTokenBuilderService
    {
        string buildRTMToken(string appId, string appCertificate, string uid, uint privilegeTs);
        string buildRTCToken(string appId, string appCertificate, string ChannelName, string uid, uint privilegeTs);
    }
}

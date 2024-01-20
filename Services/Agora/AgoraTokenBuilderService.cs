using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYPM.Services.Agora
{
    public class AgoraTokenBuilderService : IAgoraTokenBuilderService
    {
        public AccessToken mTokenCreator;
        /// <summary>
        /// Build dynamic authentication token for Real Time Messaging
        /// </summary>
        /// <param name="appId">Application Provided ID (Secret)</param>
        /// <param name="appCertificate">Application Provided Certificate (Secret)</param>
        /// <param name="uid">User unique Identifier (Primary key in database)</param>
        /// <param name="privilegeTs">Time span in seconds for token expirey</param>
        /// <returns>RTM Token string</returns>
        public string buildRTMToken(string appId, string appCertificate, string uid, uint privilegeTs)
        {
            mTokenCreator = new AccessToken(appId, appCertificate, uid, "", "");
            mTokenCreator.addPrivilege(Privileges.kRtmLogin, privilegeTs);
            return mTokenCreator.build();
        }

        /// <summary>
        /// Build dynamic authentication token for Real Time Communication
        /// </summary>
        /// <param name="appId">Application Provided ID (Secret)</param>
        /// <param name="appCertificate">Application Provided Certificate (Secret)</param>
        /// <param name="ChannelName">Channel name to be created (Case Sensitive)</param>
        /// <param name="uid">User unique Identifier (Primary key in database)</param>
        /// <param name="privilegeTs">Time span in seconds for token expirey</param>
        /// <returns>RTC Token string</returns>
        public string buildRTCToken(string appId, string appCertificate, string ChannelName, string uid, uint privilegeTs)
        {
            mTokenCreator = new AccessToken(appId, appCertificate, ChannelName, uid, "");
            mTokenCreator.addPrivilege(Privileges.kJoinChannel, privilegeTs);
            mTokenCreator.addPrivilege(Privileges.kPublishAudioStream, privilegeTs);
            return mTokenCreator.build();
        }
        public double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalSeconds);
        }
    }
    public enum Privileges
    {
        kJoinChannel = 1,
        kPublishAudioStream = 2,
        kPublishVideoStream = 3,
        kPublishDataStream = 4,
        kPublishAudiocdn = 5,
        kPublishVideoCdn = 6,
        kRequestPublishAudioStream = 7,
        kRequestPublishVideoStream = 8,
        kRequestPublishDataStream = 9,
        kInvitePublishAudioStream = 10,
        kInvitePublishVideoStream = 11,
        kInvitePublishDataStream = 12,
        kAdministrateChannel = 101,
        kRtmLogin = 1000
    }
}


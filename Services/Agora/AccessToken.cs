﻿using Force.Crc32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYPM.Services.Agora
{
    public class AccessToken
    {
        public string _appId;
        public string _appCertificate;
        public string _channelName;
        public string _uid;
        public uint _ts;
        public uint _salt;
        public byte[] _signature;
        public uint _crcChannelName;
        public uint _crcUid;
        public byte[] _messageRawContent;
        public PrivilegeMessage message = new PrivilegeMessage();

        public AccessToken(string appId, string appCertificate, string channelName, string uid, string identity)
        {
            _appId = appId;
            _appCertificate = appCertificate;
            _channelName = channelName;
            _uid = uid;
        }

        public AccessToken(string appId, string appCertificate, string channelName, string uid, uint ts, uint salt)
        {
            this._appId = appId;
            this._appCertificate = appCertificate;
            this._channelName = channelName;
            this._uid = uid;
            this._ts = ts;
            this._salt = salt;
        }

        public void addPrivilege(Privileges kJoinChannel, uint expiredTs)
        {
            this.message.messages.Add((ushort)kJoinChannel, expiredTs);
        }

        public string build()
        {
            if (!TokenBuilderUtils.isUUID(this._appId))
            {
                return "";
            }

            if (!TokenBuilderUtils.isUUID(this._appCertificate))
            {
                return "";
            }

            this._messageRawContent = TokenBuilderUtils.pack(this.message);
            this._signature = generateSignature(_appCertificate
                    , _appId
                    , _channelName
                    , _uid
                    , _messageRawContent);

            this._crcChannelName = Crc32Algorithm.Compute(this._channelName.GetByteArray());
            this._crcUid = Crc32Algorithm.Compute(this._uid.GetByteArray());

            PackContent packContent = new PackContent(_signature, _crcChannelName, _crcUid, this._messageRawContent);
            byte[] content = TokenBuilderUtils.pack(packContent);
            return getVersion() + this._appId + TokenBuilderUtils.base64Encode(content);
        }
        public static String getVersion()
        {
            return "006";
        }

        public static byte[] generateSignature(String appCertificate
                , String appID
                , String channelName
                , String uid
                , byte[] message)
        {

            using (var ms = new MemoryStream())
            using (BinaryWriter baos = new BinaryWriter(ms))
            {
                baos.Write(appID.GetByteArray());
                baos.Write(channelName.GetByteArray());
                baos.Write(uid.GetByteArray());
                baos.Write(message);
                baos.Flush();

                byte[] sign = DynamicKeyUtil.encodeHMAC(appCertificate, ms.ToArray(), "SHA256");
                return sign;
            }
        }

        public bool fromString(string token)
        {
            if (!getVersion().Equals(token.Substring(0, TokenBuilderUtils.VERSION_LENGTH)))
            {
                return false;
            }
            this._appId = token.Substring(TokenBuilderUtils.VERSION_LENGTH, TokenBuilderUtils.APP_ID_LENGTH);
            PackContent packContent = new PackContent();
            TokenBuilderUtils.unpack(TokenBuilderUtils.base64Decode(token.Substring(TokenBuilderUtils.VERSION_LENGTH + TokenBuilderUtils.APP_ID_LENGTH)), packContent);
            this._signature = packContent.signature;
            this._crcChannelName = packContent.crcChannelName;
            this._crcUid = packContent.crcUid;
            this._messageRawContent = packContent.rawMessage;
            TokenBuilderUtils.unpack(this._messageRawContent, message);
            return true;
        }
    }
}

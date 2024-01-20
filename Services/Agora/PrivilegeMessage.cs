using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FYPM.Services.Agora
{
    public class PrivilegeMessage : IPackable
    {
        public uint salt;
        public uint ts;
        public Dictionary<ushort, uint> messages;
        public PrivilegeMessage()
        {
            this.salt = (uint)randomInt();
            this.ts = (uint)getTimestamp() + 24 * 3600;
            this.messages = new Dictionary<ushort, uint>();
        }

        public ByteBuf marshal(ByteBuf outBuf)
        {
            return outBuf.put(salt).put(ts).putIntMap(messages);
        }

        public void unmarshal(ByteBuf inBuf)
        {
            this.salt = inBuf.readInt();
            this.ts = inBuf.readInt();
            this.messages = inBuf.readIntMap();
        }

        public static int randomInt()
        {
            RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
            byte[] bytes = new byte[4];
            rngCsp.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }
        public static int getTimestamp()
        {
            TimeSpan t = DateTime.Now - new DateTime(1970, 1, 1);
            return (int)t.TotalSeconds;
        }
    }
}

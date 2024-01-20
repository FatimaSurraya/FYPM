using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYPM.Services.Agora
{
    public interface IPackable
    {
        ByteBuf marshal(ByteBuf outBuf);
        void unmarshal(ByteBuf inBuf);
    }
}

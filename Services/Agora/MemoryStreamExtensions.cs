using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYPM.Services.Agora
{
    public static class MemoryStreamExtensions
    {
        public static void write(this MemoryStream obj, string data)
        {
            var array = Encoding.UTF8.GetBytes(data);
            obj.Write(array, (int)obj.Length, array.Length);
        }

        public static byte[] GetByteArray(this string obj)
        {
            return Encoding.UTF8.GetBytes(obj);
        }
        public static byte[] getBytes(this string obj)
        {
            return Encoding.UTF8.GetBytes(obj);
        }
    }
}

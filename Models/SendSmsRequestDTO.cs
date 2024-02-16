using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYPM.Models
{
    public class SendSmsRequestDTO
    {
        public string to { get; set; }
        public string message { get; set; }
    }

    public class ResponseDTO<T>
    {
        public string Message { get; set; }
        public T Data { get; set; }
        public bool Success { get; set; }

        public ResponseDTO(string message, T data, bool success)
        {
            Message = message;
            Data = data;
            Success = success;
        }

        public static ResponseDTO<bool> CreateSuccessResponse(string message)
        {
            return new ResponseDTO<bool>(message, true, true);
        }
    }
    public static class Responses
    {
        public static ResponseDTO<T> OK<T>(string message, T data)
        {
            return new ResponseDTO<T>(message, data, true);
        }

        public static ResponseDTO<T> Error<T>(string message)
        {
            return new ResponseDTO<T>(message, default(T), false);
        }
    }
}

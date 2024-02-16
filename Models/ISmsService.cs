using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYPM.Models
{
    public interface ISmsService
    {
        ResponseDTO<bool> SendSms(SendSmsRequestDTO model);
       
    }

}

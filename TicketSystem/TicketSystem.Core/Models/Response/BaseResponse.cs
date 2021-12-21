using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.Core.Models.Enums;

namespace TicketSystem.Core.Models.Response
{
    public class BaseResponse<T>
    {
        public BaseResponse(ApiResponseCode code, T data)
        {
            Data = data;
            Message = code.ToString();
            Code = code;
        }

        public ApiResponseCode Code { get; private set; }

        public string Message { get; private set; }

        public T Data { get; private set; }

    }
}

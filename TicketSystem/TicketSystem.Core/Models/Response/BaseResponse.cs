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
        public ApiResponseCode Code { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }

    }
}

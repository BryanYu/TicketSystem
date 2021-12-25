using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSystem.Core.Models.Enums
{
    public enum ApiResponseCode
    {
        Success = 0,

        UnAuthorized = 1,

        StatusNotAllow = 2,

        DataNotFound = 3,

        TicketTypeNotAllow = 4
    }
}

using HealtSync.Application.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealtSync.Application.Response.Users.Users
{
    public class PatientResponse : BaseResponse
    {
        public dynamic? Model { get; set; }
    }
}

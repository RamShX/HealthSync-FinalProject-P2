

using HealtSync.Application.Core;
using System.Runtime.InteropServices;

namespace HealtSync.Application.Response.Users.Users
{
    public sealed class UsersResponse :BaseResponse
    {
        public dynamic? Model { get; set; }
    }
}

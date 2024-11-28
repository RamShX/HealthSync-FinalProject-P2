using HealtSync.Application.Dtos.Users.Doctors;
using Newtonsoft.Json;

namespace HealtSync.Web.Models.Users.Doctors
{
    public class DoctorGetAllResultModel : BaseResponseModel
    {
        [JsonProperty("model")]
        public List<GetSimpleDoctorDto>? Data { get; set; }

    }
}

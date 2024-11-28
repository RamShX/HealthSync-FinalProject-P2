using HealtSync.Application.Dtos.Users.Doctors;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace HealtSync.Web.Models.Users.Doctors
{
    public class DoctorGetByIdResultModel : BaseResponseModel
    {
        [JsonProperty("model")]
        public GetDetailedDoctorDto? Data { get; set; }
    }
}



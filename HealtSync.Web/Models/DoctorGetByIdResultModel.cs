using HealtSync.Application.Dtos.Users.Doctors;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace HealtSync.Web.Models
{
    public class DoctorGetByIdResultModel : BaseModel
    {
        [JsonProperty("model")]
        public GetDetailedDoctorDto? Data { get; set; }
    }
}



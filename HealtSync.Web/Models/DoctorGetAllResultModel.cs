using HealtSync.Application.Dtos.Users.Doctors;
using Newtonsoft.Json;

namespace HealtSync.Web.Models
{
    public class DoctorGetAllResultModel: BaseModel
    {
        [JsonProperty("model")]
        public List<GetSimpleDoctorDto>? Data { get; set; }
      
    }
}

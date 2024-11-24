namespace HealtSync.Web.Models
{
    public class DoctorModel
    {

        public class Rootobject
        {
            public Model[] model { get; set; }
            public bool isSuccess { get; set; }
            public object message { get; set; }
        }

        public class Model
        {
            public int doctorID { get; set; }
            public int specialityID { get; set; }
            public string education { get; set; }
            public string clinicAddress { get; set; }
            public int consultationFee { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string gender { get; set; }
            public object changeDate { get; set; }
        }

    }
}

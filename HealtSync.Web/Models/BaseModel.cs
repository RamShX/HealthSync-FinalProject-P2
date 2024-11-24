namespace HealtSync.Web.Models
{
    public abstract class BaseModel
    {
        public bool isSuccess { get; set; }
        public string? message { get; set; }
    }
}

using HealtSync.Domain.Base;


namespace HealtSync.Domain.Entities.Insurance
{
    public sealed class NetworkType : ActivatableEntity
    {
        public int NetworkTypeld { get; set; }// Primarary ID
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}

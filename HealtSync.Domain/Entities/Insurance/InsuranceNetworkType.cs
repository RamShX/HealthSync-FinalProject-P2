using HealtSync.Domain.Base;


namespace HealtSync.Domain.Entities.Insurance
{
    public sealed class InsuranceNetworkType : ActivatableEntity
    {
        public int NetworkTypeld { get; set; }// Primarary ID
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}

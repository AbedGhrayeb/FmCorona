namespace Domain.Entities
{
    public class ExternalLogin
    {
        public int Id { get; set; }
        public string ProviderId { get; set; }
        public string ProviderName { get; set; }
        public virtual AppUser User { get; set; }
    }
}
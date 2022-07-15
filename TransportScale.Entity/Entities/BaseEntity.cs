namespace TransportScale.Entity.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime? Updated { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; }
    }
}

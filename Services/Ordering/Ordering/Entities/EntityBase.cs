namespace Ordering.Entities
{
    public abstract class EntityBase
    {
        //protected set:  use in the derived class
        public int Id { get; protected set; }

        // few audit props
        public string? CreateaBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}

using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Entities
{
    public class productBrand : BaseEntity
    {
        [BsonElement("Name")]
        public string Name { get; set; }
    }
}
using LaJuana.Domain.Common;
using Lucene.Net.Documents;

using System.Text.Json.Serialization;
namespace LaJuana.Domain
{
    public class Tag : BaseDomainModel
    {
        public Tag()
        {
            Persons = new HashSet<Person>();
        }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid TagCategoryId { get; set; }
        public virtual TagCategory TagCategory { get; set; }
        public virtual ICollection<Person> Persons { get; set; }
        public Document ObjectFromLucene(){
            Document document = new Document();
            document.Add(new StringField("Id", this.Id.ToString(), Field.Store.YES));
            document.Add(new StringField("CategoryId", this.TagCategoryId.ToString(), Field.Store.YES));
            document.Add(new StringField("CategoryName", this.TagCategory.Description, Field.Store.YES));
            document.Add(new TextField("Name", this.Name, Field.Store.YES));
            return document;
        }
    }
}

using LaJuana.Domain.Common;
using Lucene.Net.Documents;
using System.Text.Json;


/// <summary>
/// 
/// </summary>
namespace LaJuana.Domain
{
    public class Address :  BaseDomainModel
    {
        public string Street { get; set; } = string.Empty;
        
        public int City { get; set; }
        
        public int State { get; set; }
        
        public int Country { get; set; }
        
        public string ZipCode { get; set; } = string.Empty ;

        public decimal Latitude { get; set; }
        
        public decimal Longitude { get; set; }

        public string Description { get; set; } = string.Empty;

        public Guid PersonId { get; set; }

        public virtual Person? Person { get; set; }
        public override string ToString()
        {
            Address a=new Address(){
                Id=this.Id,
                Street=this.Street,
                Latitude=this.Latitude,
                Longitude=this.Longitude,
                Description=this.Description,
                PersonId=this.PersonId
            };
            return JsonSerializer.Serialize(a);
        }
        
        public Document ObjectFromLucene(){
            Document document = new Document();
            document.Add(new StringField("Id", this.Id.ToString(), Field.Store.YES));
            document.Add(new StringField("PersonId", this.PersonId.ToString(), Field.Store.YES));
            document.Add(new StringField("Latitude", this.Latitude.ToString(), Field.Store.YES));
            document.Add(new StringField("Longitude", this.Longitude.ToString(), Field.Store.YES));
            document.Add(new TextField("Street", this.Street, Field.Store.YES));
            return document;
        }
    }
}

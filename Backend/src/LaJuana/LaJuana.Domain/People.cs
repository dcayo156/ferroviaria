using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Documents;
namespace LaJuana.Domain
{

    public enum Gender
    {
        Femenino,
        Masculino,
        Personalizado
    }
    public enum PronounPreference
    {
        Femenino,
        Masculino
    }
    public class People : Person
    {
        public string FirstName { get; set; } = string.Empty;

        public string SecondName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string NationalId { get; set; } = string.Empty;

        public DateTime? BirthDate { get; set; }

        public Gender? Gender { get; set; }
        public PronounPreference? PronounPreference { get; set; }

        public string ToString(string rol)
        {
            return $"Id: {Id} | FirstName: {FirstName} |SecondName: {SecondName} | LastName: {LastName} | Rol: {rol}";
        }
        
        public Document ObjectFromLucene(){
            Document document = new Document();
            document.Add(new StringField("Id", this.Id.ToString(), Field.Store.YES));
            document.Add(new TextField("FirstName", this.FirstName, Field.Store.YES));
            document.Add(new TextField("SecondName", this.SecondName, Field.Store.YES));
            document.Add(new TextField("LastName", this.LastName, Field.Store.YES));
            document.Add(new TextField("Gender", this.Gender.ToString(), Field.Store.YES));
            string tags=string.Join(" ",this.Tags.Select(t=>t.Id.ToString()).ToArray());
            document.Add(new TextField("Tags",tags, Field.Store.YES));
            foreach (Address address in this.Addresses)
            {
                document.Add(new TextField("Address", address.ToString(), Field.Store.YES));
            }
            return document;
        }

    }


}
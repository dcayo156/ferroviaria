using System;
namespace LaJuana.Application.Models.ViewModels
{
   
	public class RelationShipByPersonVm
	{
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string SecondName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;                
        public string RelationshipTypeDescription {get; set;} = string.Empty;
        public Guid RelationshipTypeDescriptionId {get; set;}

        public string ToString(string gender)
        {
            return   $"Id: {this.PersonId.ToString()} |"+
                    $"FirstName: {this.FirstName} |"+
                    $"SecondName: {this.SecondName} |"+
                    $"LastName: {this.LastName} |"+
                    $"Gender: {gender} |"+
                    $"Rol: {this.RelationshipTypeDescription} |"+
                    $"RelationshipTypeDescriptionId: {this.RelationshipTypeDescriptionId.ToString()}";;
        }
        public void FromStringToObject(string value){
            string[] part=value.Split("|");
            foreach(string d in part){
                string[] tupla=d.Split(":");
                switch(tupla[0]){
                    case "Id": 
                        this.PersonId=new Guid(tupla[1].Trim());
                    break;
                    case "FirstName": 
                        this.FirstName=tupla[1].Trim();
                    break;
                    case "SecondName": 
                        this.SecondName=tupla[1].Trim();
                    break;
                    case "LastName": 
                        this.LastName=tupla[1].Trim();
                    break;
                    case "Rol": 
                        this.RelationshipTypeDescription=tupla[1].Trim();
                    break;
                    case "RelationshipTypeDescriptionId": 
                        this.RelationshipTypeDescriptionId=new Guid(tupla[1].Trim());
                    break;
                }
            }
        }
    }
}


using LaJuana.Application.Models.ViewModels;
using MediatR;
using LaJuana.Domain;


namespace LaJuana.Application.Features.Tags.Queries.GetTagWithAddressesWithinMap
{

    public class GetTagWithAddressesWithinMapQuery : IRequest<List<PersonAddress>>
    {
		public CardinalPoint From {get;set;}
		public CardinalPoint To {get;set;} 
        public List<CategoryTags>? Categories {get;set;}
		public Boolean fromLucene { get; set; }
		public GetTagWithAddressesWithinMapQuery()
		{

		}

    }
	public class CategoryTags{
        public string Id { get; set; } = string.Empty;
		public List<string>? TagIds { get; set; }
    }
    

}
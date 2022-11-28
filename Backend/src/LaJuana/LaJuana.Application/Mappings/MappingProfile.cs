using AutoMapper;
using LaJuana.Application.Features.Addresses.Commands.CreateAddress;
using LaJuana.Application.Features.Addresses.Commands.UpdateAddress;
using LaJuana.Application.Features.Mails.Commands.CreateMail;
using LaJuana.Application.Features.Mails.Commands.UpdateMail;
using LaJuana.Application.Features.Organizations.Commands.CreateOrganization;
using LaJuana.Application.Features.Organizations.Commands.UpdateOrganization;
using LaJuana.Application.Features.Peoples.Commands;
using LaJuana.Application.Features.Peoples.Commands.UpdatePeople;
using LaJuana.Application.Features.PersonTags.Commands;
using LaJuana.Application.Features.Phones.Commands.CreatePhone;
using LaJuana.Application.Features.Phones.Commands.UpdatePhone;
using LaJuana.Application.Features.RelationshipType.Commands.CreateRelationshipType;
using LaJuana.Application.Features.RelationshipType.Commands.UpdateRelationshipType;
using LaJuana.Application.Features.TagCategories.Commads.CreateTagCategory;
using LaJuana.Application.Features.TagCategories.Commads.UpdateTagCategory;
using LaJuana.Application.Features.Tags.Commads.CreateTag;
using LaJuana.Application.Features.Tags.Commads.UpdateTag;
using LaJuana.Application.Models.ViewModels;
using LaJuana.Domain;

namespace LaJuana.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Organization, OrganizationVm>();            
            CreateMap<People, PeopleVm>();            ;
            CreateMap<People, PeopleFullVm>();
            CreateMap<Address, AddressVm>();
            CreateMap<AddressVm, Address>();
            CreateMap<Address, AddressPersonVm>(); 
            CreateMap<Tag, TagVm>();
            CreateMap<TagVm, Tag>();
            CreateMap<RelationshipType, RelationshipTypesVM>();
            CreateMap<TagCategory, TagCategoryVm>();            
            CreateMap<Tag, TagFullVm>(); 
            CreateMap<Mail, CommunicationChannelVm>();
            CreateMap<MailVm, Mail>();
            CreateMap<CreateMailCommand, Mail>();
            CreateMap<UpdateMailCommand, Mail>();
            CreateMap<Phone, CommunicationChannelVm>();
            CreateMap<PhoneVm, Phone>();
            CreateMap<CreatePhoneCommand, Phone>();
            CreateMap<UpdatePhoneCommand, Phone>();
            CreateMap<PersonCommand, People>();
            CreateMap<AddTagToPersonCommand, PersonTag>();
            CreateMap<CreateTagCommand, Tag>();
            CreateMap<UpdatePeopleCommand, People>();
            CreateMap<UpdateTagCommand, Tag>();
            CreateMap<CreateAddressCommand, Address>();
            CreateMap<UpdateAddressCommand, Address>();
            CreateMap<People, PersonVm>();
            CreateMap<Organization, PersonVm>();
            CreateMap<PersonOrganizationCommand, Organization>();
            CreateMap<UpdateOrganizationCommand, Organization>();
            CreateMap<CreateTagCategoryCommand, TagCategory>();
            CreateMap<UpdateTagCategoryCommand, TagCategory>();
            CreateMap<RelationshipTypeCommand, RelationshipType>();
            CreateMap<UpdateRelationshipTypeCommand, RelationshipType>();
            CreateMap<PersonUpdateCommand, People>();
        }
    }
}

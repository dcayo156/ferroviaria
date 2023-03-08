using AutoMapper;
using LaJuana.Application.Features.Categories.Commands.CreateCategories;
using LaJuana.Application.Features.Categories.Commands.UpdateCategories;
using LaJuana.Application.Features.Documents.Commands.CreateDocuments;
using LaJuana.Application.Features.Documents.Commands.UpdateDocuments;
using LaJuana.Application.Features.Programs.Commands.CreatePrograms;
using LaJuana.Application.Features.Programs.Commands.UpdatePrograms;
using LaJuana.Application.Models.ViewModels;
using LaJuana.Domain;

namespace LaJuana.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Program, ProgramsFullVm>();
            CreateMap<CreateProgramsCommand, Program>();
            CreateMap<UpdateProgramsCommand, Program>();

            CreateMap<Category, CategoriesFullVm>();
            CreateMap<Category, CategoriesChildrenFullVm>();
            CreateMap<CreateCategoriesCommand, Category>();
            CreateMap<UpdateCategoriesCommand, Category>();

            CreateMap<Document, DocumentsFullVm>();
            CreateMap<CreateDocumentsCommand, Document>();
            CreateMap<UpdateDocumentsCommand, Document>();

            CreateMap<InspectionTrain, InspectionTrainsFullVm>();

            CreateMap<InspectionTrain, InspectionTrainBasicAspects>();
            CreateMap<InspectionTrain, InspectionTrainTechnicalAspects>();
            CreateMap<InspectionTrain, InspectionTrainProperHandling>();

        }
    }
}

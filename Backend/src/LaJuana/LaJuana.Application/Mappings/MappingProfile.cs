using AutoMapper;
using LaJuana.Application.Features.Categories.Commands.CreateCategories;
using LaJuana.Application.Features.Categories.Commands.UpdateCategories;
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
            CreateMap<CreateCategoriesCommand, Category>();
            CreateMap<UpdateCategoriesCommand, Category>();
        }
    }
}

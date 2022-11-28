using System;
using AutoMapper;
using LaJuana.Application.Contracts.Persistence;
using LaJuana.Application.Models.ViewModels;
using MediatR;

namespace LaJuana.Application.Features.Tags.Queries.GetTagsWithAddressCountOfPerson
{
    public class GetTagsWithAddressCountOfPersonQueryHandler : IRequestHandler<GetTagsWithAddressCountOfPersonQuery, List<CategoryWithTagsVm>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTagsWithAddressCountOfPersonQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<CategoryWithTagsVm>> Handle(GetTagsWithAddressCountOfPersonQuery request, CancellationToken cancellationToken)
        {
            List<TagsWithAddressCountOfPersonVm> tagList = (await _unitOfWork.TagRepository.GetTagsWithAddressCountOfPerson()).ToList();
            string[] tagCategories = tagList.Select(x => x.CategoryId).Distinct().ToArray();

            List<CategoryWithTagsVm> categories = new();
            foreach (var categoryItem in tagCategories)
            {
                CategoryWithTagsVm item = new();
                item.Id = categoryItem;
                item.Name = tagList.Where(x => x.CategoryId == categoryItem).FirstOrDefault()?.CategoryName;
                List<TagItemVm> tagItems = new();

                foreach (var tagItem in tagList.Where(x => x.CategoryId == categoryItem))
                {
                    tagItems.Add(new TagItemVm()
                    {
                        Id = tagItem.Id,
                        Name = tagItem.Name,
                        NumberOfPeople = tagItem.NumberOfPeople,
                    });
                }
                item.Tags = tagItems.ToArray();
                categories.Add(item);
            }
            return categories;
        }
    }
}


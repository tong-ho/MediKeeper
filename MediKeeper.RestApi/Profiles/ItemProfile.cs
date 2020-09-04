using AutoMapper;
using MediKeeper.RestApi.Entities;
using MediKeeper.RestApi.Models;
using MediKeeper.RestApi.Pagination;

namespace MediKeeper.RestApi.Profiles
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<Item, ItemDto>();

            CreateMap<ItemCreateDto, Item>();

            CreateMap<ItemUpdateDto, Item>();

            CreateMap(typeof(PagedList<Item>), typeof(PagedList<ItemDto>))
                .ConvertUsing(typeof(PagedListConverter<Item, ItemDto>));
        }
    }
}

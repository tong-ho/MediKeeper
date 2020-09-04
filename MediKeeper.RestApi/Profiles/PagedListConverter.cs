using AutoMapper;
using MediKeeper.RestApi.Pagination;
using System.Collections.Generic;

namespace MediKeeper.RestApi.Profiles
{
    public class PagedListConverter<TSource, TDestination> : ITypeConverter<PagedList<TSource>, PagedList<TDestination>> where TSource : class where TDestination : class
    {
        public PagedList<TDestination> Convert(PagedList<TSource> source, PagedList<TDestination> destination, ResolutionContext context)
        {
            var collection = context.Mapper.Map<List<TSource>, List<TDestination>>(source);
            return new PagedList<TDestination>(collection, source.TotalCount, source.CurrentPage, source.PageSize);
        }
    }
}

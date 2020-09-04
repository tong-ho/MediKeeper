using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MediKeeper.RestApi.Pagination
{
    public enum ResourceUriType
    {
        PreviousPage,
        NextPage
    }

    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public long TotalCount { get; private set; }

        public bool HasPrevious
        {
            get { return CurrentPage > 1; }
        }

        public bool HasNext
        {
            get { return CurrentPage < TotalPages; }
        }

        [JsonIgnore]
        public PaginationMetadata PaginationMetadata
        {
            get
            {
                return new PaginationMetadata()
                {
                    PageNumber = CurrentPage,
                    TotalPages = TotalPages,
                    PageSize = PageSize,
                    TotalCount = TotalCount
                };
            }
        }

        public PagedList(List<T> items, long totalCount, int pageNumber, int pageSize)
        {
            TotalCount = totalCount;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            AddRange(items);
        }
    }
}

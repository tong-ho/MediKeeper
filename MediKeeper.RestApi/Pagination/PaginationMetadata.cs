using Newtonsoft.Json;

namespace MediKeeper.RestApi.Pagination
{
    public class PaginationMetadata
    {
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public long TotalCount { get; set; }
        public string PreviousPageLink { get; set; }
        public string NextPageLink { get; set; }

        [JsonIgnore]
        public bool HasPrevious
        {
            get { return PageNumber > 1; }
        }

        [JsonIgnore]
        public bool HasNext
        {
            get { return PageNumber < TotalPages; }
        }
    }
}

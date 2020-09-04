namespace MediKeeper.RestApi.Pagination
{
    /// <summary>
    /// The order used to sort results. 0 for ascending, 1 for descending.
    /// </summary>
    public enum Order
    {
        Ascending,
        Descending
    }

    public class QueryParameters
    {
        /// <summary>
        /// The column to sort by.
        /// </summary>
        public virtual string SortBy { get; set; }

        /// <summary>
        /// The order used to sort results. 0 for ascending, 1 for descending.
        /// </summary>
        public Order Order { get; set; }

        /// <summary>
        /// The page number that is being requested.
        /// </summary>
        public int PageNumber { get; set; } = 1;

        protected int _maxPageSize = 100;

        private int _pageSize = 100;
        /// <summary>
        /// The size of the page being requested. The max page size is 100.
        /// </summary>
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value > _maxPageSize ? _maxPageSize : value;
            }
        }
    }
}

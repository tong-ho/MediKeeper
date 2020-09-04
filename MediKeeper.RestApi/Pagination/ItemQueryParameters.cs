using MediKeeper.RestApi.Entities;
using System.Collections.Generic;

namespace MediKeeper.RestApi.Pagination
{
    public class ItemQueryParameters : QueryParameters
    {
        /// <summary>
        /// The column to sort by.
        /// </summary>
        public override string SortBy { get; set; } = nameof(Item.Id);
        public string Name { get; set; } = string.Empty;
    }
}

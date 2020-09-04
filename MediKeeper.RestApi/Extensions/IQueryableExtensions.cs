using MediKeeper.RestApi.Pagination;
using MediKeeper.WebApi.Pagination;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace MediKeeper.WebApi.Extensions
{
    public static class IQueryableExtensions
    {
        public static async Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> queryable, int pageNumber, int pageSize)
        {
            int count = queryable.Count();
            List<T> items = (await queryable.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync());
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }

        public static IQueryable<T> ApplySort<T>(this IQueryable<T> source, string sortBy, Order order, Dictionary<string, PropertyMappingValue> mappingDictionary)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (mappingDictionary == null)
            {
                throw new ArgumentNullException(nameof(mappingDictionary));
            }

            if (string.IsNullOrEmpty(sortBy))
            {
                return source;
            }

            //the orderBy string is separated by commas
            string[] orderByAfterSplit = sortBy.Split(',');

            //apply each orderBy clause in reverse order - otherwise, the IQueryable will be ordered wrong
            foreach (string orderByClause in orderByAfterSplit.Reverse())
            {
                string trimmedSrderByClause = orderByClause.Trim();

                int indexOfFirstSpace = trimmedSrderByClause.IndexOf(" ");
                string propertyName = indexOfFirstSpace == -1 ?
                    trimmedSrderByClause : trimmedSrderByClause.Remove(indexOfFirstSpace);

                //Find property
                if (!mappingDictionary.TryGetValue(propertyName, out PropertyMappingValue propertyMappingValue))
                {
                    throw new ArgumentException($"Key mapping for {propertyName} is missing");
                }

                foreach (var destinationProperty in propertyMappingValue.DestinationProperties.Reverse())
                {
                    if (propertyMappingValue.Revert)
                    {
                        order = order == Order.Ascending
                            ? Order.Descending
                            : Order.Ascending;
                    }

                    source = source.OrderBy($"{destinationProperty} {order}");
                }
            }

            return source;
        }
    }
}

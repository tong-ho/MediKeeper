using MediKeeper.RestApi.Entities;
using MediKeeper.RestApi.Pagination;
using MediKeeper.WebApi.Extensions;
using MediKeeper.WebApi.Pagination;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediKeeper.RestApi.Repositories
{
    public class ItemGroupRepository : IItemGroupRepository
    {
        private readonly MediKeeperContext _context;
        private readonly IPropertyMappingService _propertyMappingService;

        public ItemGroupRepository(MediKeeperContext context, IPropertyMappingService propertyMappingService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _propertyMappingService = propertyMappingService ?? throw new ArgumentNullException(nameof(propertyMappingService));
        }

        public async Task<PagedList<Item>> Get(ItemQueryParameters queryParameters = null)
        {
            var test = await _context.Item.ToListAsync();

            queryParameters ??= new ItemQueryParameters();

            return await GetGroupedQueryable()
                .Where(item => string.IsNullOrEmpty(queryParameters.Name) || item.Name.Contains(queryParameters.Name))
                .ApplySort(queryParameters.SortBy, queryParameters.Order, _propertyMappingService.GetPropertyMapping<Item>())
                .ToPagedListAsync(queryParameters.PageNumber, queryParameters.PageSize);
        }

        public async Task<Item> Get(int id)
        {
            return await _context.Item
                .FirstOrDefaultAsync(item => item.Id == id);
        }

        public bool Update(Item item)
        {
            if (!_context.Item.Any(row => row.Id == item.Id))
            {
                return false;
            }

            _context.Item.Update(item);
            return true;
        }

        public bool Delete(Item item)
        {
            if (!_context.Item.Any(row => row.Id == item.Id))
            {
                return false;
            }

            _context.Item.Remove(item);
            return true;
        }

        public void Create(Item item)
        {
            _context.Item.Add(item);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        private IQueryable<Item> GetGroupedQueryable()
        {
            var maxTable = _context.Item
                .GroupBy(item => item.Name, item => item)
                .Select(group => new Item
                {
                    Name = group.Key,
                    Cost = group.Max(item => item.Cost)
                });

            var result = _context.Item
                .Join(maxTable,
                    item => new { item.Name, item.Cost },
                    max => new { max.Name, max.Cost },
                    (item, max) => new Item
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Cost = max.Cost
                    })
                .GroupBy(item => new
                {
                    item.Name,
                    item.Cost
                }, item => item)
                .Select(group => new Item
                {
                    Id = group.Min(item => item.Id),
                    Name = group.Key.Name,
                    Cost = group.Key.Cost
                });


            return result;
        }
    }
}

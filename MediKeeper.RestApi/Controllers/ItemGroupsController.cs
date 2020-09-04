using AutoMapper;
using MediKeeper.RestApi.Entities;
using MediKeeper.RestApi.Filters;
using MediKeeper.RestApi.Models;
using MediKeeper.RestApi.Pagination;
using MediKeeper.RestApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MediKeeper.RestApi.Controllers
{
    [Route("api/ItemGroups")]
    [ApiController]
    public class ItemGroupsController : ControllerBase
    {
        private readonly IItemGroupRepository _itemGroupRepository;
        private readonly IMapper _mapper;

        public ItemGroupsController(IItemGroupRepository itemGroupRepository, IMapper mapper)
        {
            _itemGroupRepository = itemGroupRepository ?? throw new ArgumentNullException(nameof(itemGroupRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Get items grouped by name, displaying the max price.
        /// </summary>
        /// <param name="itemQueryParameters">Pagination parameters</param>
        /// <returns>A paged list of items.</returns>
        [HttpGet]
        [AutoMapperResultFilter(typeof(PagedList<Item>), typeof(PagedList<ItemDto>))]
        [PaginationResultFilter(nameof(PaginationMetadata), nameof(QueryParameters))]
        public async Task<ActionResult<PagedList<ItemDto>>> Get([FromQuery] ItemQueryParameters itemQueryParameters = null)
        {
            itemQueryParameters ??= new ItemQueryParameters();

            PagedList<Item> items = await _itemGroupRepository.Get(itemQueryParameters);

            HttpContext.Items.Add(nameof(PaginationMetadata), items.PaginationMetadata);
            HttpContext.Items.Add(nameof(QueryParameters), itemQueryParameters);

            return Ok(items);
        }

        /// <summary>
        /// Get an item grouped by name, displaying the max price.
        /// </summary>
        /// <param name="id">The id of the item.</param>
        /// <returns>The requested item.</returns>
        [HttpGet("{id}", Name = "GetItemGroup")]
        [AutoMapperResultFilter(typeof(Item), typeof(ItemDto))]
        public async Task<ActionResult<ItemDto>> Get(int id)
        {
            Item item = await _itemGroupRepository.Get(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        /// <summary>
        /// Create a new item.
        /// </summary>
        /// <param name="itemCreate">The fields othe item.</param>
        /// <returns>The requested item.</returns>
        [HttpPost]
        [AutoMapperResultFilter(typeof(Item), typeof(ItemDto))]
        public async Task<ActionResult<ItemDto>> Post([FromBody] ItemCreateDto itemCreate)
        {
            Item item = _mapper.Map<Item>(itemCreate);
            _itemGroupRepository.Create(item);
            await _itemGroupRepository.Save();

            return CreatedAtRoute("GetItemGroup", new { id = item.Id }, item);
        }

        /// <summary>
        /// Updates an item.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="itemUpdate"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ItemUpdateDto itemUpdate)
        {
            Item item = _mapper.Map<Item>(itemUpdate);
            item.Id = id;

            if (!_itemGroupRepository.Update(item))
            {
                return NotFound();
            }

            await _itemGroupRepository.Save();
            return NoContent();
        }

        /// <summary>
        /// Deletes an item.
        /// </summary>
        /// <param name="id">The id of the item.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            Item item = await _itemGroupRepository.Get(id);
            if (item == null)
            {
                return NotFound();
            }

            _itemGroupRepository.Delete(item);
            await _itemGroupRepository.Save();

            return NoContent();
        }
    }
}

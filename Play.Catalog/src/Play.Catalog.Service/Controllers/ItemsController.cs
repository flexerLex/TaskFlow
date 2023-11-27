using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.DTOs;

namespace Play.Catalog.Service.Controllers
{
    // https://localhost:5001/items
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private static readonly List<ItemDto> items = new()
        {
            new ItemDto(Guid.NewGuid(),"Potion", "Restores a small amount of HP", 5, DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(),"Antidote", "Cures poison ", 7, DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(),"Sword", "Deals a small amount of damage", 20, DateTimeOffset.UtcNow),
        };

        [HttpGet]
        public IEnumerable<ItemDto> Get()
        {
            return items;
        }

        [HttpGet("{id}")]
        // GET /items/{id}
        public ItemDto GetById(Guid id)
        {
            var item = items.Where(item => item.Id == id).SingleOrDefault();
            return item;
        }

        [HttpPost]
        // POST /items
        public ActionResult<ItemDto> Post(CreateItemDto createItemDto)
        {
            var item = new ItemDto(Guid.NewGuid(), createItemDto.Name, createItemDto.Descriprion, createItemDto.Price, DateTimeOffset.UtcNow);
            items.Add(item);

            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        // PUT /items/{id}
        public IActionResult Put(Guid guid, UpdateItemDto updateItemDto)
        {
            var existingItem = items.Where(item => item.Id == guid).SingleOrDefault();

            var updateItem = existingItem with
            {
                Name = updateItemDto.Name,
                Descriprion = updateItemDto.Descriprion,
                Price = updateItemDto.Price
            };

            var index = items.FindIndex(existingItem => existingItem.Id == guid);
            items[index] = updateItem;

            return NoContent();

        }

        [HttpDelete("{id}")]
        // DELETE /items/{id}
        public IActionResult Delete(Guid guid)
        {
            var index = items.FindIndex(existingItem => existingItem.Id == guid);
            items.RemoveAt(index);

            return NoContent();
        }
    }
}
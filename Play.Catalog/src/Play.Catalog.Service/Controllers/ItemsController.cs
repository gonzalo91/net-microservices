using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.Dtos;

namespace Play.Catalog.Service.Controllers{

    [ApiController]
    [Route("items")]
    public class ItemController : ControllerBase{
        private static readonly List<ItemDto> items = new(){
            new ItemDto(System.Guid.NewGuid(), "Potion", "Restores a small amount of HP", 5, System.DateTimeOffset.Now),
            new ItemDto(System.Guid.NewGuid(), "Antidote", "Curse Poison", 7, System.DateTimeOffset.Now),
            new ItemDto(System.Guid.NewGuid(), "Bronze sword", "Deals a samll amount of damage", 20, System.DateTimeOffset.Now)
        };

        [HttpGet]
        public IEnumerable<ItemDto> Get(){
            return items;
        }

        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetById(System.Guid id){
            
            var item = items.Find(item => item.Id == id);

            if(item == null)
                return NotFound();

            return item;

        }

        [HttpPost]
        public ActionResult<ItemDto> Post(CreateItemDto createItemDto){
            var item = new ItemDto(System.Guid.NewGuid(), createItemDto.name, createItemDto.Description, createItemDto.Price, System.DateTimeOffset.UtcNow);
            items.Add(item);

            return CreatedAtAction(nameof(GetById), new { id = item.Id}, item );
        }

        [HttpPut("{id}")]
        public IActionResult Put(System.Guid id, UpdateItemDto updateItemDto){
            var item = items.Find(i => i.Id == id);

            if(item == null){
                return NotFound();
            }

            var updatedItem = item with{
                Name = updateItemDto.name,
                Description = updateItemDto.Description,
                Price = updateItemDto.Price,
            };

            var index = items.FindIndex(i => i.Id == id);
            items[index] = updatedItem;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(System.Guid id){
            var index = items.FindIndex(i => i.Id == id);

            if(index == -1){
                return NotFound();
            }

            items.RemoveAt(index);

            return NoContent();
        }
    }

}
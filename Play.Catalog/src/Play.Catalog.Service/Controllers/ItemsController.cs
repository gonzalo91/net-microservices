using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.Dtos;
using Play.Catalog.Service.Entities;
using Play.Catalog.Service.Repositories;

namespace Play.Catalog.Service.Controllers{

    [ApiController]
    [Route("items")]
    public class ItemController : ControllerBase{
        private readonly  IItemsRepository itemsRepository; 

        public ItemController(IItemsRepository itemsRepository){
            this.itemsRepository = itemsRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetAsync(){
            
            var items = (await itemsRepository.GetAllAsync())
                        .Select(item => item.AsDto());                        
            
            return items;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetByIdAsync(System.Guid id){
            
            var item = await itemsRepository.GetAsync(id);

            if(item == null)
                return NotFound();

            return item.AsDto();
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> Post(CreateItemDto createItemDto){
            var item = new Item{
                Name = createItemDto.name,
                Description = createItemDto.Description,
                Price = createItemDto.Price,
                CreatedDate = System.DateTimeOffset.UtcNow
            };

            await itemsRepository.CreateAsync(item);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = item.Id}, item );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(System.Guid id, UpdateItemDto updateItemDto){
            var existingItem = await itemsRepository.GetAsync(id);

            if(existingItem  == null)
                return NotFound();

            existingItem.Name = updateItemDto.name;
            existingItem.Description = updateItemDto.Description;
            existingItem.Price = updateItemDto.Price;

            await itemsRepository.UpdateAsync(existingItem);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async  Task<IActionResult> Delete(System.Guid id){
           var existingItem = await itemsRepository.GetAsync(id);

            if(existingItem  == null)
                return NotFound();

            await itemsRepository.RemoveAsync(existingItem.Id);

            return NoContent();
        }
    }

}
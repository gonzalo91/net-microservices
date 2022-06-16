using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Contracts;
using Play.Catalog.Service.Dtos;
using Play.Catalog.Service.Entities;
using Play.Common;

namespace Play.Catalog.Service.Controllers{

    [ApiController]
    [Route("items")]
    public class ItemController : ControllerBase{
        private readonly  IRepository<Item> itemsRepository; 
        private readonly  IPublishEndpoint publishEndpoint;

        public ItemController(IRepository<Item> itemsRepository, IPublishEndpoint publishEndpoint){
            this.itemsRepository = itemsRepository;
            this.publishEndpoint = publishEndpoint;
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

            await publishEndpoint.Publish(new CatalogItemCreated(item.Id, item.Name, item.Description));

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
            
            await publishEndpoint.Publish(new CatalogItemUpdated(existingItem.Id, existingItem.Name, existingItem.Description));

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async  Task<IActionResult> Delete(System.Guid id){
           var existingItem = await itemsRepository.GetAsync(id);

            if(existingItem  == null)
                return NotFound();


            await itemsRepository.RemoveAsync(existingItem.Id);

            await publishEndpoint.Publish(new CatalogItemDeleted(existingItem.Id));

            return NoContent();
        }
    }

}
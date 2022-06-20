using System.Threading.Tasks;
using MassTransit;
using Play.Catalog.Contracts;
using Play.Common;
using Play.Inventory.Service.Entities;

namespace Play.Inventory.Service{

    public class CatalogItemUpdatedConsumer : IConsumer<CatalogItemUpdated>
    {
        private readonly IRepository<CatalogItem> repository;

        public CatalogItemUpdatedConsumer(IRepository<CatalogItem> repository){
            this.repository = repository;
        }
        public async Task Consume(ConsumeContext<CatalogItemUpdated> context)
        {
            var message = context.Message;
            var item    = await repository.GetAsync(message.ItemId);

            if(item == null){
                item = new CatalogItem{
                    Id = message.ItemId,
                    Name = message.name,
                    Description = message.description
                };

                await repository.CreateAsync(item);
                return;
            }

            item.Name = message.name;
            item.Description = message.description;

            await repository.UpdateAsync(item);            
        }
    }

}
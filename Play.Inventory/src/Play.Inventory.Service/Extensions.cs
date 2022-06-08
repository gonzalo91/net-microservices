using Play.Inventory.Service.Entities;

namespace Play.Inventory.Service
{
    public static class Extensions{

        public static InventoryItemDto AsDto(this InventoryItem item, string name, string Description){
            return new InventoryItemDto(item.CatalogItemId, name, Description, item.Quantity, item.AcquiredDate);
        }


    }
}
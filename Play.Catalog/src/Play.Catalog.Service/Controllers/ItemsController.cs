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


    }

}
using System;

namespace Play.Catalog.Service.DTOs
{
    public record ItemDto(Guid Id, string Name, string Descriprion, decimal Price, DateTimeOffset CreatedDate);

    public record CreateItemDto(string Name, string Descriprion, decimal Price);

    public record UpdateItemDto(string Name, string Descriprion, decimal Price);
}
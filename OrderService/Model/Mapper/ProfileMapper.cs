using AutoMapper;
using OrderService.Model.Request;
using OrderService.Model.Response;
using OrderService.Repository.Entity;

namespace OrderService.Model.Mapper
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            CreateMap<AddBasketItemRequest, BasketItem>().ReverseMap();
            // Request → Entity
            CreateMap<CreateOrderRequest, Order>()
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.Items.Sum(i => i.UnitPrice * i.Quantity)))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            CreateMap<OrderItemRequest, OrderItem>();

            // Entity → Response
            CreateMap<Order, OrderResponse>();
            CreateMap<OrderItem, OrderItemResponse>();
        }
    }
}

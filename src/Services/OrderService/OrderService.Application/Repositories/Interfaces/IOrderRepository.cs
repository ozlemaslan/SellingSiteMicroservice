using OrderService.Domain.AggregateModels.OrderAggregate;

namespace OrderService.Application.Repositories.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
    }
}

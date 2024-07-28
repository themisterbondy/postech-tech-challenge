using Bogus;
using PosTech.MyFood.WebApi.Features.Orders.Entities;

namespace PosTech.MyFood.WebApi.UnitTests.Mocks;

public static class OrderQueueMocks
{
    public static OrderQueue GenerateValidOrderQueue()
    {
        var faker = new Faker();
        var orderId = new OrderId(faker.Random.Guid());
        var createdAt = faker.Date.Past();
        var customerCpf = faker.Random.ReplaceNumbers("###########"); // Assuming this generates a valid CPF
        var items = new List<OrderItem> { OrderItemMocks.GenerateValidOrderItem() };

        return OrderQueue.Create(orderId, createdAt, customerCpf, items);
    }

    public static OrderQueue GenerateInvalidOrderQueue()
    {
        return OrderQueue.Create(null, DateTime.MinValue, null, null);
    }
}
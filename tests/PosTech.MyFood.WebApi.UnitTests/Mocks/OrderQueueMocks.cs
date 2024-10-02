using Bogus;
using PosTech.MyFood.WebApi.Features.Orders.Entities;

namespace PosTech.MyFood.WebApi.UnitTests.Mocks;

public static class OrderQueueMocks
{
    public static OrderQueue GenerateValidOrderQueue()
    {
        var faker = new Faker();
        var orderId = new OrderId(faker.Random.Guid());
        var customerCpf = faker.Random.ReplaceNumbers("###########");
        var transactionId = faker.Random.Guid().ToString();
        var items = new List<OrderItem> { OrderItemMocks.GenerateValidOrderItem() };

        return OrderQueue.Create(orderId, customerCpf, items, transactionId,OrderQueueStatus.Received);
    }

    public static OrderQueue GenerateInvalidOrderQueue()
    {
        return OrderQueue.Create(null, null, null, null, OrderQueueStatus.Received);
    }
}
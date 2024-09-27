// using PosTech.MyFood.WebApi.Features.Orders.Entities;
// using PosTech.MyFood.WebApi.Features.Products.Entities;
//
// namespace PosTech.MyFood.WebApi.UnitTests.Features.Orders.Entities;
//
// public class OrderQueueTests
// {
//     [Fact]
//     public void OrderId_ShouldInitializeCorrectly_WhenValidGuidIsProvided()
//     {
//         var guid = Guid.NewGuid();
//         var orderId = new OrderId(guid);
//
//         orderId.Should().NotBeNull();
//         orderId.Value.Should().Be(guid);
//     }
//
//     [Fact]
//     public void OrderId_ShouldGenerateNewGuid_WhenNewIsCalled()
//     {
//         var orderId = OrderId.New();
//
//         orderId.Should().NotBeNull();
//         orderId.Value.Should().NotBe(Guid.Empty);
//     }
//
//     [Fact]
//     public void OrderQueue_ShouldInitializeCorrectly_WhenValidParametersAreProvided()
//     {
//         var id = new OrderId(Guid.NewGuid());
//         var createdAt = DateTime.UtcNow;
//         var status = OrderQueueStatus.Received;
//         var customerCpf = "12345678901";
//         var items = new List<OrderItem>
//         {
//             OrderItem.Create(new OrderItemId(Guid.NewGuid()), id, new ProductId(Guid.NewGuid()), "Test Product",
//                 10.99m, 2, ProductCategory.Lanche)
//         };
//
//         var orderQueue = OrderQueue.Create(id, createdAt, customerCpf, items);
//
//         orderQueue.Should().NotBeNull();
//         orderQueue.Id.Should().Be(id);
//         orderQueue.CreatedAt.Should().Be(createdAt);
//         orderQueue.Status.Should().Be(status);
//         orderQueue.CustomerCpf.Should().Be(customerCpf);
//         orderQueue.Items.Should().BeEquivalentTo(items);
//     }
//
//     [Fact]
//     public void OrderQueue_ShouldInitializeCorrectly_WhenCustomerCpfIsNull()
//     {
//         var id = new OrderId(Guid.NewGuid());
//         var createdAt = DateTime.UtcNow;
//         var status = OrderQueueStatus.Received;
//         string customerCpf = null;
//         var items = new List<OrderItem>
//         {
//             OrderItem.Create(new OrderItemId(Guid.NewGuid()), id, new ProductId(Guid.NewGuid()), "Test Product",
//                 10.99m, 2, ProductCategory.Lanche)
//         };
//
//         var orderQueue = OrderQueue.Create(id, createdAt, customerCpf, items);
//
//         orderQueue.Should().NotBeNull();
//         orderQueue.Id.Should().Be(id);
//         orderQueue.CreatedAt.Should().Be(createdAt);
//         orderQueue.Status.Should().Be(status);
//         orderQueue.CustomerCpf.Should().BeNull();
//         orderQueue.Items.Should().BeEquivalentTo(items);
//     }
//
//     [Fact]
//     public void OrderQueue_ShouldInitializeCorrectly_WhenItemsListIsEmpty()
//     {
//         var id = new OrderId(Guid.NewGuid());
//         var createdAt = DateTime.UtcNow;
//         var status = OrderQueueStatus.Received;
//         var customerCpf = "12345678901";
//         var items = new List<OrderItem>();
//
//         var orderQueue = OrderQueue.Create(id, createdAt, customerCpf, items);
//
//         orderQueue.Should().NotBeNull();
//         orderQueue.Id.Should().Be(id);
//         orderQueue.CreatedAt.Should().Be(createdAt);
//         orderQueue.Status.Should().Be(status);
//         orderQueue.CustomerCpf.Should().Be(customerCpf);
//         orderQueue.Items.Should().BeEmpty();
//     }
// }
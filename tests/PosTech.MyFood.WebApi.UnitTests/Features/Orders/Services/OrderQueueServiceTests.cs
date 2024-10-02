// using PosTech.MyFood.WebApi.Features.Customers.Repositories;
// using PosTech.MyFood.WebApi.Features.Orders.Contracts;
// using PosTech.MyFood.WebApi.Features.Orders.Entities;
// using PosTech.MyFood.WebApi.Features.Orders.Repositories;
// using PosTech.MyFood.WebApi.Features.Orders.Services;
// using PosTech.MyFood.WebApi.Features.Products.Entities;
// using PosTech.MyFood.WebApi.Features.Products.Repositories;
// using PosTech.MyFood.WebApi.UnitTests.Mocks;
//
// namespace PosTech.MyFood.WebApi.UnitTests.Features.Orders.Services;
//
// public class OrderQueueServiceTests
// {
//     private readonly IOrderQueueRepository _orderQueueRepository;
//     private readonly OrderQueueService _service;
//
//     public OrderQueueServiceTests()
//     {
//         _orderQueueRepository = Substitute.For<IOrderQueueRepository>();
//         _service = new OrderQueueService(_orderQueueRepository);
//     }
//
//     [Fact]
//     public async Task EnqueueOrderAsync_ShouldReturnFailure_WhenProductNotFound()
//     {
//         // Arrange
//         var request = new EnqueueOrderRequest
//         {
//             Items = [new OrderItemRequest { ProductId = Guid.NewGuid(), Quantity = 1 }]
//         };
//
//         // Act
//         var result = await _service.EnqueueOrderAsync(request, CancellationToken.None);
//
//         // Assert
//         result.IsFailure.Should().BeTrue();
//         result.Error.Message.Should().Contain("Product with id");
//     }
//
//     [Fact]
//     public async Task EnqueueOrderAsync_ShouldEnqueueOrderSuccessfully_WhenValidRequestIsProvided()
//     {
//         // Arrange
//         var request = new EnqueueOrderRequest
//         {
//             CustomerCpf = "12345678901",
//             Items = [new OrderItemRequest { ProductId = Guid.NewGuid(), Quantity = 1 }]
//         };
//
//         var product = ProductMocks.GenerateValidProduct();
//         var customer = CustomerMocks.GenerateValidCustomer();
//         OrderQueueMocks.GenerateValidOrderQueue();
//
//         _customerRepository.GetByCpfAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
//             .Returns(customer);
//         _productRepository.FindByIdAsync(Arg.Any<ProductId>(), Arg.Any<CancellationToken>())
//             .Returns(product);
//         _orderQueueRepository.AddAsync(Arg.Any<OrderQueue>(), Arg.Any<CancellationToken>())
//             .Returns(Task.CompletedTask);
//
//         // Act
//         var result = await _service.EnqueueOrderAsync(request, CancellationToken.None);
//
//         // Assert
//         result.IsSuccess.Should().BeTrue();
//         result.Value.Should().NotBeNull();
//         result.Value.Items.Should().HaveCount(1);
//     }
//
//     [Fact]
//     public async Task GetOrderByIdAsync_ShouldReturnOrder_WhenOrderExists()
//     {
//         // Arrange
//         var orderQueue = OrderQueueMocks.GenerateValidOrderQueue();
//         _orderQueueRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
//             .Returns(orderQueue);
//
//         // Act
//         var result = await _service.GetOrderByIdAsync(orderQueue.Id.Value, CancellationToken.None);
//
//         // Assert
//         result.IsSuccess.Should().BeTrue();
//         result.Value.Should().NotBeNull();
//         result.Value.Id.Should().Be(orderQueue.Id.Value);
//     }
//
//     [Fact]
//     public async Task GetOrderByIdAsync_ShouldReturnFailure_WhenOrderDoesNotExist()
//     {
//         // Arrange
//         _orderQueueRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
//             .Returns((OrderQueue)null);
//
//         // Act
//         var result = await _service.GetOrderByIdAsync(Guid.NewGuid(), CancellationToken.None);
//
//         // Assert
//         result.IsFailure.Should().BeTrue();
//         result.Error.Message.Should().Contain("Order with id");
//     }
//
//     [Fact]
//     public async Task UpdateOrderStatusAsync_ShouldUpdateStatus_WhenOrderExists()
//     {
//         // Arrange
//         var orderQueue = OrderQueueMocks.GenerateValidOrderQueue();
//         _orderQueueRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
//             .Returns(orderQueue);
//
//         // Act
//         var result =
//             await _service.UpdateOrderStatusAsync(orderQueue.Id.Value, OrderQueueStatus.Preparing,
//                 CancellationToken.None);
//
//         // Assert
//         result.IsSuccess.Should().BeTrue();
//         result.Value.Status.Should().Be(OrderQueueStatus.Preparing);
//     }
//
//     [Fact]
//     public async Task UpdateOrderStatusAsync_ShouldReturnFailure_WhenOrderDoesNotExist()
//     {
//         // Arrange
//         _orderQueueRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
//             .Returns((OrderQueue)null);
//
//         // Act
//         var result =
//             await _service.UpdateOrderStatusAsync(Guid.NewGuid(), OrderQueueStatus.Preparing, CancellationToken.None);
//
//         // Assert
//         result.IsFailure.Should().BeTrue();
//         result.Error.Message.Should().Contain("Order with id");
//     }
// }
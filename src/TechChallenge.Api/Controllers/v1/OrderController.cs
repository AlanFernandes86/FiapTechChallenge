using Microsoft.AspNetCore.Mvc;
using TechChallenge.Api.Controllers.Common;
using TechChallenge.Application.Common.UseCase.Interfaces;
using TechChallenge.Application.Common.UseCase.Models;
using TechChallenge.Application.Order.CreateOrder;
using TechChallenge.Application.Order.GetOrdersById;
using TechChallenge.Application.Order.GetOrdersByStatus;
using TechChallenge.Application.Order.PutProductToOrder;
using TechChallenge.Application.Order.UpdateOrderStatus;
using TechChallenge.Domain.Entities;
using TechChallenge.Domain.Enums;
using TechChallenge.Domain.Ports.Services;

namespace TechChallenge.Api.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IUseCase<GetOrdersByStatusDAO, UseCaseOutput<IEnumerable<Order>>> _getOrdersByStatusUseCase;
    private readonly IUseCase<GetOrderByIdDAO, UseCaseOutput<Order>> _getOrderByIdUseCase;
    private readonly IUseCase<UpdateOrderStatusDAO, UseCaseOutput<int>> _updateOrderStatusUseCase;
    private readonly IUseCase<CreateOrderDAO, UseCaseOutput<int>> _createOrderUseCase;
    private readonly IUseCase<PutProductToOrderDAO, UseCaseOutput<int>> _putProductToOrderUseCase;

    public OrderController(
        IOrderService orderService,
        IUseCase<GetOrdersByStatusDAO, UseCaseOutput<IEnumerable<Order>>> getOrdersByStatusUseCase,
        IUseCase<GetOrderByIdDAO, UseCaseOutput<Order>> getOrderByIdUseCase,
        IUseCase<UpdateOrderStatusDAO, UseCaseOutput<int>> updateOrderStatusUseCase,
        IUseCase<CreateOrderDAO, UseCaseOutput<int>> createOrderUseCase,
        IUseCase<PutProductToOrderDAO, UseCaseOutput<int>> putProductToOrderUseCase
    )
    {
        _orderService = orderService;
        _getOrdersByStatusUseCase = getOrdersByStatusUseCase;
        _getOrderByIdUseCase = getOrderByIdUseCase;
        _updateOrderStatusUseCase = updateOrderStatusUseCase;
        _createOrderUseCase = createOrderUseCase;
        _putProductToOrderUseCase = putProductToOrderUseCase;
    }

    [HttpPatch("Status/{orderId}")]
    public async Task<IActionResult> UpdateOrderStatus(int orderId, [FromQuery] OrderStatus orderStatus)
    {
        var output = await _updateOrderStatusUseCase.Handle(new UpdateOrderStatusDAO(orderId, orderStatus));

        return output.ToResult(this);
    }

    [HttpGet("Status/{orderStatus}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UseCaseOutput<IEnumerable<Order>>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(UseCaseOutput<IEnumerable<Order>>))]
    public async Task<IActionResult> GetOrderByStatus(OrderStatus orderStatus)
    {
        var output = await _getOrdersByStatusUseCase.Handle(new GetOrdersByStatusDAO(orderStatus));

        return output.ToResult(this);
    }

    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetOrderById(int orderId)
    {
        var output = await _getOrderByIdUseCase.Handle(new GetOrderByIdDAO(orderId));

        return output.ToResult(this);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderDAO createOrderDAO)
    {
        var output = await _createOrderUseCase.Handle(createOrderDAO);

        return output.ToResult(this);
    }

    [HttpPut("Product")]
    public async Task<IActionResult> PutProductToOrder(PutProductToOrderDAO putProductToOrder)
    {
        var output = await _putProductToOrderUseCase.Handle(putProductToOrder);

        return output.ToResult(this);
    }

    [HttpDelete("Product/{orderProductId}")]
    public async Task<IActionResult> RemoveProductToOrder(int orderProductId)
    {
        var result = await _orderService.RemoveProductToOrder(orderProductId);

        return result != -1 ? Ok(new { id = result }) : BadRequest();
    }
}
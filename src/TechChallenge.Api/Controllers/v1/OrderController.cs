using Microsoft.AspNetCore.Mvc;
using System.Threading;
using TechChallenge.Application.Common.UseCase.Interfaces;
using TechChallenge.Application.Common.UseCase.Models;
using TechChallenge.Application.Order.GetOrdersById;
using TechChallenge.Application.Order.GetOrdersByStatus;
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

    public OrderController(
        IOrderService orderService,
        IUseCase<GetOrdersByStatusDAO, UseCaseOutput<IEnumerable<Order>>> getOrdersByStatusUseCase,
        IUseCase<GetOrderByIdDAO, UseCaseOutput<Order>> getOrderByIdUseCase
    )
    {
        _orderService = orderService;
        _getOrdersByStatusUseCase = getOrdersByStatusUseCase;
        _getOrderByIdUseCase = getOrderByIdUseCase;
    }

    [HttpPatch("Status")]
    public async Task<IActionResult> UpdateOrderStatus(int orderId, OrderStatus orderStatus)
    {
        var result = await _orderService.UpdateOrderStatus(orderId, orderStatus);

        return result != -1 ? Ok(new { id = result, status = orderStatus }) : BadRequest();
    }

    [HttpGet("ByStatus")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UseCaseOutput<IEnumerable<Order>>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(UseCaseOutput<IEnumerable<Order>>))]
    public async Task<IActionResult> GetOrderByStatus(OrderStatus orderStatus)
    {
        var result = await _getOrdersByStatusUseCase.Handle(new GetOrdersByStatusDAO(orderStatus));

        if (result.OutputStatus == OutputStatus.Success)
            return Ok(result);

        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetOrderById(int orderId)
    {
        var result = await _getOrderByIdUseCase.Handle(new GetOrderByIdDAO(orderId));

        if (result.OutputStatus == OutputStatus.Success)
            return Ok(result);

        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(Order order)
    {
        var result = await _orderService.CreateOrder(order);

        return result != -1 ? Ok(new { id = result }) : BadRequest();
    }

    [HttpPut("Product")]
    public async Task<IActionResult> PutProductToOrder(OrderProduct orderProduct, int orderId)
    {
        var result = await _orderService.PutProductToOrder(orderProduct, orderId);

        return result != -1 ? Ok(new { id = result }) : BadRequest();
    }

    [HttpDelete("Product")]
    public async Task<IActionResult> RemoveProductToOrder(int orderProductId)
    {
        var result = await _orderService.RemoveProductToOrder(orderProductId);

        return result != -1 ? Ok(new { id = result }) : BadRequest();
    }
}
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TechChallenge.Application.Common.UseCase.Interfaces;
using TechChallenge.Application.Common.UseCase.Models;
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
    private readonly IUseCase<GetOrdersByStatusDAO, UseCaseOutput<IEnumerable<Order>>> _useCase;

    public OrderController(IOrderService orderService, IUseCase<GetOrdersByStatusDAO, UseCaseOutput<IEnumerable<Order>>> useCase)
    {
        _orderService = orderService;
        _useCase = useCase;
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
    public async Task<IActionResult> GetOrderByStatus(OrderStatus orderStatus, CancellationToken cancellationToken)
    {
        var result = await _useCase.Handle(new GetOrdersByStatusDAO(orderStatus), cancellationToken);

        if (result.OutputStatus == OutputStatus.Success)
            return Ok(result);

        return StatusCode(StatusCodes.Status500InternalServerError, (object)result);
    }

    [HttpGet("ById")]
    public async Task<IActionResult> GetOrderById(int orderId)
    {
        var result = await _orderService.GetOrdersById(orderId);

        return result is not null ? Ok(result) : NotFound();
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
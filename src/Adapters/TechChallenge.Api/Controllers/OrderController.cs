using Microsoft.AspNetCore.Mvc;
using TechChallenge.Application.Services;
using TechChallenge.Domain.Entities;
using TechChallenge.Domain.Enums;
using TechChallenge.Domain.Ports.Services;

namespace TechChallengeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;            
        }

        [HttpPatch("Status")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, OrderStatus orderStatus)
        {
            var result = await _orderService.UpdateOrderStatus(orderId, orderStatus);

            return result != -1 ? Ok(new { id = result, status = orderStatus }) : BadRequest();
        }

        [HttpGet("ByStatus")]
        public async Task<IActionResult> GetOrderByStatus(OrderStatus orderStatus)
        {
            var result = await _orderService.GetOrderByStatus(orderStatus);

            return result is not null ? Ok(result) : NotFound();
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
}
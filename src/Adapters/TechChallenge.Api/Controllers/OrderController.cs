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

        [HttpGet]
        public async Task<IActionResult> GetOrder(OrderStatus orderStatus)
        {
            var result = await _orderService.GetOrderByStatus(orderStatus);

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
    }
}
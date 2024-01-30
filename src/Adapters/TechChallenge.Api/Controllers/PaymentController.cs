using Microsoft.AspNetCore.Mvc;
using TechChallenge.Domain.Entities;
using TechChallenge.Domain.Ports.Services;

namespace TechChallengeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;            
        }

        [HttpPost]
        public async Task<IActionResult> SetPayment(Payment payment)
        {
            var result = await _paymentService.SetPayment(payment);

            return result != -1 ? Ok(new { id = result }) : BadRequest();
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Stripe;
using System.Security.Claims;
using PharmacuticalE_Commerce.Repositories.Interfaces;
using PharmacuticalE_Commerce.Repositories;

namespace PharmacuticalE_Commerce.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class APIController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IConfiguration _configuration;
        public APIController(IOrderRepository orderRepository, IConfiguration configuration)
        {
            _orderRepository = orderRepository;
            _configuration = configuration;
        }
        [HttpPost("PayVisa")]
        public async Task<IActionResult> PayVisa([FromBody] int Id)
        {
            var order = _orderRepository.GetByIdWithDetails(Id);
            if (order == null || order.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return NotFound();
            }
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "EGP",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = order.OrderId.ToString(),
                            },
                            UnitAmount = (long)order.TotalAmount,
                        },
                        Quantity = 1,
                    },
                },
                Mode = "payment",
                SuccessUrl = "http://localhost:5139/API/checkout/success",
                CancelUrl = "http://localhost:5139/API/checkout/cancel",
            };
            var service = new SessionService();
            try
            {
                var session = await service.CreateAsync(options);
                return Ok(new { sessionId = session.Id });
            }
            catch (Exception ex)
            {
                // Log the error for debugging
                return StatusCode(500, "Internal server error");
            };
        }

        [HttpGet("success")]
        [Route("checkout/success")]
        public IActionResult Success()
        {
            return View();
        }

        [HttpGet("cancel")]
        [Route("checkout/cancel")]
        public IActionResult Cancel()
        {
            return View();
        }
    }
}

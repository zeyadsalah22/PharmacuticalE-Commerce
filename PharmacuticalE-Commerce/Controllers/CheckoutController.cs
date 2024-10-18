using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Stripe;
using PharmacuticalE_Commerce.Models;
using PharmacuticalE_Commerce.Viewmodels;
using PharmacuticalE_Commerce.Repositories.Interfaces;
namespace PharmacuticalE_Commerce.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CheckoutController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IOrderRepository _orderRepository;

        public CheckoutController(IConfiguration configuration, IOrderRepository orderRepository)
        {
            _configuration = configuration;
            _orderRepository = orderRepository;
        }
        [HttpPost("create-checkout-session")]
        public IActionResult CreateCheckoutSession([FromBody] CheckoutFormModel model)
        {
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
                        Currency = model.Currency,
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = model.ProductName,
                            Description = model.ProductDescription,
                        },
                        UnitAmount = model.Amount,
                    },
                    Quantity = 1,
                },
            },
                Mode = "payment",
                SuccessUrl = $"{Request.Scheme}://{Request.Host}/checkout/success/{int.Parse(model.ProductName)}",
                CancelUrl = $"{Request.Scheme}://{Request.Host}/checkout/cancel",
            };
            var service = new SessionService();
            var session = service.Create(options);
            return Ok(new { sessionId = session.Id });
        }
        [HttpGet("success/{id}")]
        public IActionResult Success(int? id)
        {
            if (id != null)
            {
                var order = _orderRepository.GetById(id.Value);
                if (order != null)
                {
                    order.IsPaid = true;
                    _orderRepository.Update(order);
                }
            }
            return View();
        }
        [HttpGet("cancel")]
        public IActionResult Cancel()
        {
            return View();
        }
    }
}
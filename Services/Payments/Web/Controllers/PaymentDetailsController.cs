using AidManager.Services.Payments.Application.Commands;
using AidManager.Services.Payments.Application.DTOs;
using AidManager.Services.Payments.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AidManager.Services.Payments.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentDetailsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentDetailsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePaymentDetail([FromBody] CreatePaymentDetailCommand command)
        {
            var paymentDetailId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetPaymentDetailById), new { id = paymentDetailId }, command);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentDetailById(int id)
        {
            var query = new GetPaymentDetailByIdQuery(id);
            var paymentDetail = await _mediator.Send(query);

            if (paymentDetail == null)
            {
                return NotFound();
            }

            return Ok(paymentDetail);
        }
    }
}

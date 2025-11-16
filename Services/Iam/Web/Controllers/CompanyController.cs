using Application.Commands;
using Application.DTOs;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompanyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Company/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompanyById(int id)
        {
            var query = new GetCompanyByIdQuery(id);
            var result = await _mediator.Send(query);
            return result != null ? Ok(result) : NotFound();
        }

        // PUT: api/Company/{id}
        // Assuming UpdateCompanyResource contains the fields to update.
        // The EditCompanyCommand will take CompanyId from the route and other details from the body.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(int id, [FromBody] UpdateCompanyResource updateCompanyResource)
        {
            if (updateCompanyResource == null)
            {
                return BadRequest("Company data is null.");
            }
            // Note: The original EditCompanyCommand was (string BrandName, string Country, string Email, int CompanyId);
            // The DTO is UpdateCompanyResource(string CompanyName, string Country, string Email);
            // Adjusting to use CompanyName from DTO.
            var command = new EditCompanyCommand(id, updateCompanyResource.CompanyName, updateCompanyResource.Country, updateCompanyResource.Email);
            var result = await _mediator.Send(command);
            return result != null ? Ok(result) : NotFound(); // Or BadRequest if the update failed for other reasons
        }
    }
}

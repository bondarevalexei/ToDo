using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using ServiceContracts;
using Shared.DTO;

namespace Presentation.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IServiceManager _service;

        public AccountController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet("{id:guid}", Name = "AccountById")]
        [Authorize]
        public async Task<IActionResult> GetAccount(Guid id)
        {
            var account =
                await _service.AccountService.GetAccountAsync(id, false);

            return Ok(account);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAccount(Guid id)
        {
            await _service.AccountService.DeleteAccountAsync(id, false);
            
            return NoContent();
        }

        [HttpPut("{id:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateAccount(Guid id, [FromBody]
            AccountForUpdateDto accountForUpdate)
        {
            await _service.AccountService.UpdateAccountAsync(id, accountForUpdate, true);
            return NoContent();    
        }
    }
}

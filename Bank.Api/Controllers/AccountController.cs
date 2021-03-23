using Bank.Api.Commands;
using Bank.Api.DTOs;
using Bank.Infra;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly BankDbContext _context;

        public AccountController(IMediator mediator, BankDbContext context)
        {
            this._mediator = mediator;
            _context = context;
        }

        [Route("withdraw")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]        
        public async Task<IActionResult> Withdraw([FromBody] WithdrawCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Success
                ? Ok()
                : BadRequest(result.Error);
                
        }

        [Route("deposit")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Deposit([FromBody] DepositCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Success
                ? Ok()
                : BadRequest(result.Error);
        }

        [Route("payment")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Payment([FromBody] PaymentCommand command)
        {            
            var result = await _mediator.Send(command);
            return result.Success
                ? Ok()
                : BadRequest(result.Error);
        }

        [Route("{id}/statement")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType(typeof(AccountDto))]
        public async Task<IActionResult> GetStatements(int id)
        {
            var account = await _context.Accounts    
                                        .AsNoTracking()
                                        .Where(a => a.No == id)
                                        .Include(a => a.Operations)
                                        .Include(a => a.Owner)
                                        .SingleOrDefaultAsync();
            if (account is null)
                return NotFound($"Não foi possível localizar conta com número {id}");

            AccountDto dto = account;
            return Ok(dto);

        }

        [Route("calculateIncome")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted)]        
        public IActionResult CalculateIncome([FromBody] CalculateIncomeCommand command)
        {
            _mediator.Send(command);
            return Accepted();
        }
    }

   
}

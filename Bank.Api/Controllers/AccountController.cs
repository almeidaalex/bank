﻿using Bank.Api.Commands;
using Bank.Api.ViewModels;
using Bank.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [Route("withdraw")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]        
        public async Task<IActionResult> Withdraw([FromBody] WithdrawViewModel model)
        {
            var result = await _mediator.Send(new WithdrawCommand(model.Amount, model.AccountNo));
            return result.Success
                ? Ok(result)
                : BadRequest(result.Error);
                
        }

        [Route("deposit")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Deposit([FromBody] DepositViewModel model)
        {
            var result = await _mediator.Send(new DepositCommand(model.Amount, model.AccountNo));
            return result.Success
                ? Ok(result)
                : BadRequest(result.Error);
        }

        [Route("payment")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Payment([FromBody] PaymentViewModel model)
        {            
            var result = await _mediator.Send(new PaymentCommand(model.AccountNo, model.Invoice));
            return result.Success
                ? Ok(result)
                : BadRequest(result.Error);
        }
    }

   
}

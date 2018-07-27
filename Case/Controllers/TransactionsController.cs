using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Case.Data;
using Microsoft.AspNetCore.Authorization;

namespace Case.Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class TransactionsController : ControllerBase {
    private readonly TransactionsRepository _Repository;

    public TransactionsController(TransactionsRepository repository) {
      _Repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<object>> Get([FromQuery] TransactionQuery query) {
      return new { Results = (await _Repository.Get(query)).ToList() };
    }

    [HttpGet]
    [Route("secure")]
    [Authorize(Policy = "Transactions")]
    public async Task<ActionResult<object>> GetSecure([FromQuery] TransactionQuery query) {
      return new { Results = (await _Repository.Get(query)).ToList() };
    }
  }
}

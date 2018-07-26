using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Case.Data;

namespace Case.Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class TransactionsController : ControllerBase {
    private readonly TransactionRepository _Repository;

    public TransactionsController(TransactionRepository repository) {
      _Repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<object>> Get([FromQuery] TransactionQuery query) {
      foreach (var e in query.Date) {
        Console.WriteLine(e + " DDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD");
      }

      return new { Results = (await _Repository.Get(query)).ToList() };
    }
  }
}

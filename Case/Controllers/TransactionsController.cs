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
    private readonly IRepository<Transaction> _Repository;

    public TransactionsController(IRepository<Transaction> repository) {
        _Repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<object>> Get() {
        return new { Results = (await _Repository.GetAll()).ToList() };
    }
  }
}

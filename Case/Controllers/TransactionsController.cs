using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Case.Data;
using Microsoft.AspNetCore.Authorization;

namespace Case.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionsRepository _Repository;

        public class ResultFormat
        {
            public List<Transaction> Results;
        }

        public TransactionsController(ITransactionsRepository repository)
        {
            _Repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<ResultFormat>> Get([FromQuery] TransactionQuery query)
        {
            return new ResultFormat() { Results = (await _Repository.Get(query)).ToList() };
        }

        [HttpGet]
        [Route("secure")]
        [Authorize(Policy = "Transactions")]
        public async Task<ActionResult<ResultFormat>> GetSecure([FromQuery] TransactionQuery query)
        {
            return new ResultFormat() { Results = (await _Repository.Get(query)).ToList() };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Case.Models;
using Case.Business;
using Microsoft.AspNetCore.Authorization;

namespace Case.Controllers
{
    [Route("api/v1/[controller]")]
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

        // This endpoint is not behind an authentication layer and is
        // only provided for demonstration purposes.
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<ResultFormat>> Get([FromQuery] TransactionsQuery query)
        {
            return new ResultFormat() { Results = (await _Repository.GetTransactions(query)).ToList() };
        }

        [HttpGet]
        [Route("secure")]
        [Authorize(Policy = "Transactions")]
        public async Task<ActionResult<ResultFormat>> GetSecure([FromQuery] TransactionsQuery query)
        {
            return new ResultFormat() { Results = (await _Repository.GetTransactions(query)).ToList() };
        }
    }
}

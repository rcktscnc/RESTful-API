using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Case.Business;

namespace Case.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IRepository _Repository;

        public TransactionsController(IRepository repository)
        {
            _Repository = repository;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> Get()
        {
            var transactions = await _Repository.GetAllTransactions();
            return transactions.ToList();
        }
    }
}

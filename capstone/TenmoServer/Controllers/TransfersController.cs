using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.DAO;
using TenmoServer.Models;

namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class TransfersController : ControllerBase
    {
        private readonly ITransferDao transferDao; // have private variable to represent dao object

        public TransfersController(ITransferDao transferDao)
        {
            this.transferDao = transferDao;
        }
        
        // POST: TransfersController/Create
        [HttpPost()]
        public ActionResult<Transfer> CreateTransfer(User user, Transfer transfer)
        {
            return Ok();
        }

        // GET: TransfersController/Details/5
        [HttpGet("{transferId}")]
        public ActionResult<Transfer> GetSpecificTransfer(User user, int transferId);
        {
            return Ok();
        }

        // GET: TransfersController
        [HttpGet()]
        public ActionResult<List<Transfer>> GetTransfers(User user)
        {
            return Ok();
        }

        // Put: TransfersController/Edit/5
        [HttpPut("{transferId}")]
        public ActionResult UpdateSendingTransferStatus(int transferId)
        {
            return Ok();
        }

        [HttpPut()]
        public ActionResult UpdateBalanceForTransferAccounts(Transfer transfer)
        {
        return Ok();
        }


}
}

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
    //[Authorize]
    public class TransfersController : ControllerBase
    {
        private readonly ITransferDao transferDao; // have private variable to represent dao object

        public TransfersController(ITransferDao transferDao)
        {
            this.transferDao = transferDao;
        }

        //POST: TransfersController/Create
        [HttpPost] // TODO: how to make endpoints work? throwing exception that this route is implemented multiple times???
        public ActionResult<Transfer> CreateTransfer(Transfer transfer)
        {
            Transfer t = transferDao.CreateTransfer(transfer);
            transferDao.UpdateBalanceForTransferAccounts(transfer);
            return Created($"/transfers/{t.AccountFrom}/{t.TransferId}", t);
        }

        // GET: TransfersController/Details/5
        [HttpGet("{transferId}")] // transfers/userid/transfer id //removed userid from 
        public ActionResult<Transfer> GetSpecificTransfer(int transferId)
        {
            Transfer transfer = transferDao.GetSpecificTransfer(transferId);
            if (transfer != null)
            {
                return Ok(transfer);
            }
            else
            {
                return NotFound();
            }
        }

        // GET: TransfersController
        [HttpGet("list/{userId}")] //possibly use user url
        public ActionResult<List<TransferHistory>> GetTransfers(int userId) // status code 500
        {
            if (User.Identity.Name != null)
            {
                return transferDao.GetTransfers(userId);
            }
            else
            {
                return Unauthorized("Please login to view your transfers.");
            }
        }

        [HttpPut("updateBalances")]
        public ActionResult<bool> UpdateBalanceForTransferAccounts(Transfer transfer)
        {
            bool result = transferDao.UpdateBalanceForTransferAccounts(transfer);
            if (result)
            {
                return Ok();
            }
            else
            {
                return StatusCode(500);
            }

        }

        [HttpGet("userList")] //users to transfer to
         public ActionResult<List<TransferRecipient>> GetListOfUsers()
         {
            List<TransferRecipient> recipients = transferDao.GetListOfUsers();
            return Ok(recipients);
         }
        

    }
}

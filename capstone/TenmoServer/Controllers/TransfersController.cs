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
            Transfer createdTransfer = transferDao.CreateTransfer(user, transfer);
            return Created($"/transfers/{createdTransfer.TransferId}", createdTransfer);
        }

        // GET: TransfersController/Details/5
        [HttpGet("{transferId}")] // transfers/transfer id
        public ActionResult<Transfer> GetSpecificTransfer(User user, int transferId)
        {
            Transfer transfer = transferDao.GetSpecificTransfer(user, transferId);
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
        [HttpGet()] //possibly use user url
        public ActionResult<List<Transfer>> GetTransfers(User user)
        {
            if (User.Identity.Name != null)
            {
                return transferDao.GetTransfers(user);
            }
            else
            {
                return Unauthorized("Please login to view your transfers.");
            }
            

        }

        // Put: TransfersController/Edit/5
        [HttpPut("{transferId}")]
        public ActionResult UpdateSendingTransferStatus(User user, int transferId)
        {
            Transfer existingTransfer = transferDao.GetSpecificTransfer(user, transferId);
            if (existingTransfer == null)
            {
                return NotFound();
            }

            transferDao.UpdateSendingTransferStatus(transferId);
            return Ok(); //come back to this, maybe, idk....
        }

        ///referring to crazy sql method where we may or may not return to user
        //[HttpPut()]
        //public ActionResult UpdateBalanceForTransferAccounts(Transfer transfer)
        //{
            
            
        //    transferDao.UpdateSendingTransferStatus(transferId);
        //    return Ok();
        //}


    }
}

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

        ////POST: TransfersController/Create
        //[HttpPost("{user.UserId}")] // TODO: how to make endpoints work? throwing exception that this route is implemented multiple times???
        //public ActionResult<Transfer> CreateTransfer(User user, Transfer transfer)
        //{
        //    Transfer createdTransfer = transferDao.CreateTransfer(user, transfer);
        //    return Created($"/transfers/{user.UserId}/{createdTransfer.TransferId}", createdTransfer);
        //}

        // GET: TransfersController/Details/5
        [HttpGet("{user.UserId}/{transferId}")] // transfers/userid/transfer id
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
        //[HttpGet("{user.UserId}/transferList")] //possibly use user url
        //public ActionResult<List<Transfer>> GetTransfers(User user) // status code 500
        //{
        //    if (User.Identity.Name != null)
        //    {
        //        return transferDao.GetTransfers(user);
        //    }
        //    else
        //    {
        //        return Unauthorized("Please login to view your transfers.");
        //    }
        //}

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
            
        //}

        //[HttpGet("userList")] //users to transfer to
        //public ActionResult<List<User>> GetListOfUsers(User user)
        //{
        //    if (User.Identity.Name != null)
        //    {
        //        return transferDao.GetListOfUsers(user);
        //    }
        //    else
        //    {
        //        return Unauthorized("Please login to view your transfers.");
        //    }
        //}


    }
}

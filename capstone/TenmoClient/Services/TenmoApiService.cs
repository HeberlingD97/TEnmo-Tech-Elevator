using RestSharp;
using System.Collections.Generic;
using TenmoClient.Models;

namespace TenmoClient.Services
{
    public class TenmoApiService : AuthenticatedApiService
    {
        public readonly string ApiUrl;

        public TenmoApiService(string apiUrl) : base(apiUrl) { }

        public Account GetAccount(ApiUser user)
        {
            RestRequest request = new RestRequest($"/accounts/{user.UserId}");
            IRestResponse<Account> response = client.Get<Account>(request);

            CheckForError(response);
            return response.Data;
            // TODO:  unable to reach server to test if this method actually works
        }

        public List<TransferHistory> GetPastTransfers(int userId)
        {
            RestRequest request = new RestRequest($"transfers/list/{userId}");
            IRestResponse<List<TransferHistory>> response = client.Get<List<TransferHistory>>(request);

            CheckForError(response);
            return response.Data;

        }

        public Transfer ViewSpecificTransfer(ApiUser user, int transferId)
        {
            RestRequest request = new RestRequest($"/transfers/{transferId}/");
            IRestResponse<Transfer> response = client.Get<Transfer>(request);

            CheckForError(response);
            return response.Data;
        }

        public Transfer CreateTransfer(int fromUser, int toUser, decimal amt)//instead of passing in a new transfer bc datatbase crsates the transfer 
        {
            RestRequest req = new RestRequest("transfers");
            req.AddJsonBody(new Transfer { AccountFrom = fromUser, AccountTo = toUser, Amount = amt });
            IRestResponse<Transfer> response = client.Post<Transfer>(req);
            CheckForError(response);
            return response.Data;
        }
        public List<ApiUser> GetListOfUsers()
        {
            RestRequest req = new RestRequest("transfers/userList");
            IRestResponse<List<ApiUser>> response = client.Get<List<ApiUser>>(req);
            CheckForError(response);
            return response.Data;

        }

        public Transfer UpdateBalanceForTransferAccounts(Transfer updatingTransfer)
        {
            RestRequest req = new RestRequest($"transfer/{updatingTransfer.TransferId}");
            req.AddJsonBody(updatingTransfer);
            IRestResponse<Transfer> response = client.Put<Transfer>(req);
            CheckForError(response);
            return response.Data;

        }

        
    }
}

﻿using RestSharp;
using System.Collections.Generic;
using TenmoClient.Models;

namespace TenmoClient.Services
{
    public class TenmoApiService : AuthenticatedApiService
    {
        public readonly string ApiUrl;

        public TenmoApiService(string apiUrl) : base(apiUrl) { }

        // Add methods to call api here...
        // 3. As an authenticated user of the system, I need to be able to see my Account Balance.
        public decimal GetBalance()
        {
            RestRequest request = new RestRequest("balance");
            IRestResponse<decimal> response = client.Get<decimal>(request);

            CheckForError(response);
            return response.Data;
            // TODO:  unable to reach server to test if this method actually works
        }

    }
}

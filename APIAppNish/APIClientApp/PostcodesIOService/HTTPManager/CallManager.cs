using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APIClientApp.PostcodesIOService.DataHandling;

namespace APIClientApp.PostcodesIOService.HTTPManager
{
    public class CallManager
    {
        private readonly RestClient _client;
        public RestResponse RestResponse { get; set; }

        public CallManager()
        {
            _client = new RestClient(AppConfigReader.BaseUrl);
        }

        public async Task<string> MakeRequestAsync(string postcode)
        {
            var request = new RestRequest();
            request.AddHeader("Content-Type", "application/json");
            request.Resource = $"postcodes/{postcode}";

            RestResponse = await _client.ExecuteAsync(request);
            return RestResponse.Content;

        }

        public async Task<string> MakeBulkRequestAsync()
        {
            var request = new RestRequest();
            request.AddHeader("Content-Type", "application/json");

            var postcodes = new
            {
                Postcodes = new string[] { "OX49 5NU", "M32 0JG", "NE30 1DP" }
            };

            request.AddJsonBody(postcodes);

            RestResponse = await _client.ExecuteAsync(request);
            return RestResponse.Content;

        }

    }
}

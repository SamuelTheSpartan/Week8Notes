using apitestapp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITestApp
{
    public class BulkPostcodeService
    {
        #region Properties
        // Handles comms with the API
        public RestClient Client { get; set; }
        public RestResponse Response { get; set; }
        public JObject ResponseContent { get; set; }
        public BulkPostcodeResponse ResponseObject { get; set; }
        #endregion

        public BulkPostcodeService()
        {
            Client = new RestClient(AppConfigReader.BaseUrl);
        }

        public async Task MakeRequestAsync()
        {
            var bulkPostcodeRequest = new RestRequest("/postcodes/", Method.Post);
            bulkPostcodeRequest.AddHeader("Content-Type", "application/json");

            var postcodes = new
            {
                Postcodes = new string[] { "OX49 5NU", "M32 0JG", "NE30 1DP" }
            };

            bulkPostcodeRequest.AddJsonBody(postcodes);
            RestResponse bulkPostcodeResponse = await Client.ExecuteAsync(bulkPostcodeRequest);
            ResponseContent = JObject.Parse(bulkPostcodeResponse.Content);
            ResponseObject = JsonConvert.DeserializeObject<BulkPostcodeResponse>(bulkPostcodeResponse.Content);



            //var client = new RestClient(options);

            //var bulkPostcodeRequest = new RestRequest("/postcodes/", Method.Post);
            //bulkPostcodeRequest.AddHeader("Content-Type", "application/json");

            //var postcodes = new
            //{
            //    Postcodes = new string[] { "OX49 5NU", "M32 0JG", "NE30 1DP" }
            //};


            ////request.AddStringBody(body, DataFormat.Json);
            //bulkPostcodeRequest.AddJsonBody(postcodes);
            //RestResponse bulkPostcodeResponse = await client.ExecuteAsync(bulkPostcodeRequest);
        }
    }


}

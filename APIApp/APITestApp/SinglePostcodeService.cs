using apitestapp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Security.Cryptography;

namespace APITestApp
{
    public class SinglePostcodeService
    {
        #region Properties
        // Handles comms with the API
        public RestClient Client { get; set; }
        public RestResponse Response { get; set; }
        public JObject ResponseContent { get; set; }
        public SinglePostcodeResponse ResponseObject { get; set; }
        #endregion

        public SinglePostcodeService()
        {
            Client = new RestClient(AppConfigReader.BaseUrl);
        }

        public async Task MakeRequestAsync(string postcode)
        {
            var request = new RestRequest();
            request.AddHeader("Content-Type", "application/json");
            request.Resource = $"postcodes/{postcode}";
            Response = await Client.ExecuteAsync(request);
            ResponseContent = JObject.Parse(Response.Content);
            ResponseObject = JsonConvert.DeserializeObject<SinglePostcodeResponse>(Response.Content);
        }

    }
}

using apitestapp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Security.Cryptography;

namespace APITestApp
{
    public class SingleOutcodeService
    {
        #region Properties
        // Handles comms with the API
        public RestClient Client { get; set; }
        public RestResponse Response { get; set; }
        public JObject ResponseContent { get; set; }
        public SingleOutcodeResponse ResponseObject { get; set; }
        #endregion

        public SingleOutcodeService()
        {
            Client = new RestClient(AppConfigReader.BaseUrl);
        }

        public async Task MakeRequestAsync(string outcode)
        {
            var request = new RestRequest();
            request.AddHeader("Content-Type", "application/json");
            request.Resource = $"outcodes/{outcode}";
            Response = await Client.ExecuteAsync(request);
            ResponseContent = JObject.Parse(Response.Content);
            ResponseObject = JsonConvert.DeserializeObject<SingleOutcodeResponse>(Response.Content);
        }

    }
}

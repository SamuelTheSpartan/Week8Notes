using apitestapp;
using APITestApp.PostcodesIOService.HTTPManager;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;

namespace APITestApp.PostcodesIOService
{
    public class SinglePostcodeService
    {
        public CallManager CallManager { get; set; }
        public JObject JsonResponse { get; set; }
        public SinglePostcodeResponse ResponseObject { get; set; }
        public string PostcodeResponse { get; set; }

        public SinglePostcodeService()
        {
            CallManager = new CallManager();
            ResponseObject = new DTO<SinglePostcodeResponse>();
        }

        /// <summary>
        /// Defines and makes the API request and stores the response
        /// </summary>
        /// <param name="postcode"></param>
        /// <returns></returns>
        public async Task MakRequestAsync(string postcode)
        {
            PostcodeResponse = await CallManager.MakeRequestAsync(postcode);
            JsonResponse = JObject.Parse(PostcodeResponse);
            ResponseObject = JsonConvert.DeserializeObject<SinglePostcodeResponse>(PostcodeResponse);
        }
    }
}

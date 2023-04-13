using APIClientApp;
using APIClientApp.PostcodesIOService.DataHandling;
using APIClientApp.PostcodesIOService.HTTPManager;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
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
        public CallManager CallManager { get; set; }
        public JObject JsonResponse { get; set; }
        public string PostcodeResponse { get; set; }
        public DTO<BulkPostcodeResponse> BulkPostcodeDTO { get; set; }
        #endregion

        public BulkPostcodeService()
        {
            CallManager = new CallManager();
            BulkPostcodeDTO = new DTO<BulkPostcodeResponse>();
        }

        /// <summary>
        /// defines and makes the API request and stores the response
        /// </summary>
        /// <param name="postcode"></param>
        /// <returns></returns>
        public async Task MakeRequestAsync(string[] postcodes)
        {
            PostcodeResponse = await CallManager.MakeBulkRequestAsync(postcodes);
            JsonResponse = JObject.Parse(PostcodeResponse);
            BulkPostcodeDTO.DeserializeResponse(PostcodeResponse);
        }

        public int GetStatusCode()
        {
            return (int)CallManager.RestResponse.StatusCode;
        }

        public string? GetHeaderValue(string name)
        {
            return CallManager.RestResponse.Headers.Where(x => x.Name == name).Select(x => x.Value.ToString()).FirstOrDefault();
        }

        public string GetResponseContentType()
        {
            return CallManager.RestResponse.ContentType;
        }

        public int CodeCount()
        {
            var count = 0;

            foreach (var code in JsonResponse["result"]["codes"])
            {
                count++;
            }

            return count;
        }

    }
}

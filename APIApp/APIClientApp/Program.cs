using RestSharp;
using Newtonsoft;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace APIClientApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Encapsulates the info we need to make the api call
            // Allows us to send authenticated HTTP requests
            var restClient = new RestClient("https://api.postcodes.io/");

            // Set up my request
            var restRequest = new RestRequest();

            // Optional - if I don't set the method to anything, the default will always be Method.Get
            restRequest.Method = Method.Get;

            // Adding my request headers
            restRequest.AddHeader("Content-Type", "application/json");

            string postcode = "EC2Y 5AS";

            restRequest.Resource = $"postcodes/{postcode.ToLower()}";
            // Why .ToLower()?


            RestResponse singlePostcodeResponse = restClient.Execute(restRequest);

            #region Printing stuff out

            Console.WriteLine("Response Content (string)");

            // .Content returns the response body as an unformatted string
            Console.WriteLine(singlePostcodeResponse.Content);

            Console.WriteLine("Response Status (int)");
            Console.WriteLine((int)singlePostcodeResponse.StatusCode);


            Console.WriteLine("\n");



            foreach (var header in singlePostcodeResponse.Headers)
            {
                Console.WriteLine(header);
            }

            Console.WriteLine("\n");


            var headers = singlePostcodeResponse.Headers;

            var responseDateHeader = headers.Where(h => h.Name == "Date").Select(h => h.Value).FirstOrDefault();

            Console.WriteLine(responseDateHeader);


            Console.WriteLine("\n");

            #endregion

            var options = new RestClientOptions("https://api.postcodes.io")
            {
                MaxTimeout = -1,
            };

            var client = new RestClient(options);

            var bulkPostcodeRequest = new RestRequest("/postcodes/", Method.Post);
            bulkPostcodeRequest.AddHeader("Content-Type", "application/json");

            var postcodes = new
            {
                Postcodes = new string[] { "OX49 5NU", "M32 0JG", "NE30 1DP" }
            };


            //request.AddStringBody(body, DataFormat.Json);
            bulkPostcodeRequest.AddJsonBody(postcodes);
            RestResponse bulkPostcodeResponse = await client.ExecuteAsync(bulkPostcodeRequest);

            Console.WriteLine(bulkPostcodeResponse.Content);



            var singlePostcodeJsonResponse = JObject.Parse(singlePostcodeResponse.Content);

            Console.WriteLine("\nResponse content as a Jobject");
            Console.WriteLine(singlePostcodeJsonResponse);
            Console.WriteLine("status");
            Console.WriteLine(singlePostcodeJsonResponse["status"]);
            Console.WriteLine("Admins District");
            Console.WriteLine(singlePostcodeJsonResponse["result"]["admin_district"]);

            var bulkPostcodeJsonResponse = JObject.Parse(bulkPostcodeResponse.Content);

            var adminDistrict = bulkPostcodeJsonResponse["result"][1]["result"]["admin_district"];

            Console.WriteLine($"Admin District of second post code: {adminDistrict}");

            var singlePostcodeObjectResponse = JsonConvert.DeserializeObject<SinglePostcodeResponse>(singlePostcodeResponse.Content);
            Console.WriteLine(singlePostcodeObjectResponse.status);
            Console.WriteLine(singlePostcodeObjectResponse.result.region);




            // Set up my request
            var outcodeRequest = new RestRequest();

            // Adding my request headers
            outcodeRequest.AddHeader("Content-Type", "application/json");

            string outcode = "EC2Y";

            outcodeRequest.Resource = $"outcodes/{outcode.ToLower()}";

            var singleOutcodeResponse = await restClient.ExecuteAsync(outcodeRequest);

            Console.WriteLine(singleOutcodeResponse.Content);

            var singleOutcodeJsonResponse = JObject.Parse(singleOutcodeResponse.Content);

            var adminDistrict2 = singleOutcodeJsonResponse["result"]["admin_district"];

            Console.WriteLine($"\nAdmin districts of this singleOutcodeRepsonse: {adminDistrict2}");


            var singleOutcodeObjectResponse = JsonConvert.DeserializeObject<SingleOutcodeResponse>(singleOutcodeResponse.Content);

            Console.WriteLine(singlePostcodeObjectResponse.status);
            Console.WriteLine(singlePostcodeObjectResponse.result.region);

            Console.WriteLine(singleOutcodeObjectResponse.status);
            Console.WriteLine(singleOutcodeObjectResponse.result.parish[0]);
            Console.WriteLine(singleOutcodeObjectResponse.result.parliamentary_constituency[1]);
            Console.WriteLine(singleOutcodeObjectResponse.result.country[0]);
            Console.WriteLine(singleOutcodeObjectResponse.result.longitude);

        }
    }
}
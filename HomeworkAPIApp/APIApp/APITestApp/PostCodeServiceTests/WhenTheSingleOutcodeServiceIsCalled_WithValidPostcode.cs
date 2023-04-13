using apitestapp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Security.Cryptography;

namespace APITestApp.SinglePostCodeServiceTests
{
    [Category("Happy")]
    public class WhenTheSingleOutcodeServiceIsCalled_WithValidPostcode
    {
        private SingleOutcodeService _singleOutcodeService;

        [OneTimeSetUp]
        public async Task OneTimeSetupAsync()
        {
            _singleOutcodeService = new SingleOutcodeService();
            await _singleOutcodeService.MakeRequestAsync("EC2Y");
        }

        [Test]
        public void StatusIs200_InJsonResponseBody()
        {
            Assert.That(_singleOutcodeService.ResponseContent["status"].ToString(), Is.EqualTo("200"));
        }

        [Test]
        public void StatusIs200_InResponseHeader()
        {
            Assert.That((int)_singleOutcodeService.Response.StatusCode, Is.EqualTo(200));
        }

        /// <summary>
        /// POSTCODE DOESN'T EXIST
        /// DON'T LOOK FOR POSTCODES
        /// </summary>
        //[Test]
        //public void CorrectPostcodeIsReturned()
        //{
        //    var result = _singleOutcodeService.ResponseContent["result"]["postcode"].ToString();
        //    Assert.That(result, Is.EqualTo("EC2Y 5AS"));
        //}

        [Test]
        public void ContentType_IsJson()
        {
            Assert.That(_singleOutcodeService.Response.ContentType, Is.EqualTo("application/json"));
        }

        [Test]
        public void ConnectionIsKeepAlive()
        {
            var result = _singleOutcodeService.Response.Headers.Where(x => x.Name == "Connection").Select(x => x.Value).FirstOrDefault();
            Assert.That(result, Is.EqualTo("keep-alive"));
        }

        [Test]
        public void ObjectStatusIs200()
        {
           Assert.That(_singleOutcodeService.ResponseObject.status, Is.EqualTo(200));
        }

        [Test]
        public void StatusInResponseHeader_SameAsStatusInResponseBody()
        {
           Assert.That((int)_singleOutcodeService.Response.StatusCode, Is.EqualTo(_singleOutcodeService.ResponseObject.status));
        }

        [Test]
        public void AdminDistrict_IsCityOfLondon()
        {
            Assert.That(_singleOutcodeService.ResponseObject.result.admin_district[1], Is.EqualTo("City of London"));
        }
    }
}

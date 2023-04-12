using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITestApp.SinglePostCodeServiceTests
{
    public class WhenTheBulkPostcodeServiceIsCalled_WithValidPostcode
    {

        private BulkPostcodeService _bulkPostcodeService;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _bulkPostcodeService = new BulkPostcodeService();
            _bulkPostcodeService.MakeRequestAsync().GetAwaiter().GetResult();
        }

        [Test]
        public void StatusIs200_InJsonResponseBody()
        {
            Assert.That(_bulkPostcodeService.ResponseContent["status"].ToString(), Is.EqualTo("200"));
        }

        [Test]
        public void StatusIs200_InResponseHeader()
        {
            Assert.That((int)_bulkPostcodeService.Response.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void CorrectPostcodeIsReturned()
        {
            var result = _bulkPostcodeService.ResponseContent["result"]["postcode"].ToString();
            Assert.That(result, Is.EqualTo("EC2Y 5AS"));
        }

        [Test]
        public void ContentType_IsJson()
        {
            Assert.That(_bulkPostcodeService.Response.ContentType, Is.EqualTo("application/json"));
        }

        [Test]
        public void ConnectionIsKeepAlive()
        {
            var result = _bulkPostcodeService.Response.Headers.Where(x => x.Name == "Connection").Select(x => x.Value).FirstOrDefault();
            Assert.That(result, Is.EqualTo("keep-alive"));
        }

        [Test]
        public void ObjectStatusIs200()
        {
            Assert.That(_bulkPostcodeService.ResponseObject.status, Is.EqualTo(200));
        }

        [Test]
        public void StatusInResponseHeader_SameAsStatusInResponseBody()
        {
            Assert.That((int)_bulkPostcodeService.Response.StatusCode, Is.EqualTo(_bulkPostcodeService.ResponseObject.status));
        }

        
        //[Test]
        //public void AdminDistrict_IsCityOfLondon()
        //{
        //    Assert.That(_bulkPostcodeService.ResponseObject.result.admin_district, Is.EqualTo("City of London"));
        //}

    }
}

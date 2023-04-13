using APIClientApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITestApp.BulkPostcodeServiceTests
{
    [Category("Happy")]
    public class WhenTheBulkPostcodeServiceISCalled_WithValidPostcodes
    {

        private BulkPostcodeService _bulkPostcodeService;
        [OneTimeSetUp]
        public async Task OneTimeSetupAsync()
        {
            _bulkPostcodeService = new BulkPostcodeService();
            await _bulkPostcodeService.MakeRequestAsync(new string[] { "OX49 5NU", "M32 0JG", "NE30 1DP" });
        }

        [Test]
        public void StatisIs200_InJsonResponseBody()
        {
            //Assert.That(_bulkPostcodeService.JsonResponse["status"].ToString(), Is.EqualTo("200"));
            Assert.That(_bulkPostcodeService.GetStatusCode(), Is.EqualTo(200));
        }

        [Test]
        public void ContentType_IsJson()
        {
            Assert.That(_bulkPostcodeService.GetResponseContentType(), Is.EqualTo("application/json"));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITestApp.PostcodesIOService.DataHandling
{
    public class DTO<IResponse> where IResponse : new()
    {
        //Model the data returned by the API call6

        public IResponse Response { get; set; }
    }
}

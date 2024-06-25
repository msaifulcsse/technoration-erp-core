using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModels
{
    public class VmAuthenticationTracer
    {
        public string IPAddress { get; set; }
        public string CountryName { get; set; }
        public string Region { get; set; }
        public string CityName { get; set; }
        public string PostalCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string TimeZone { get; set; }
        public string Organization { get; set; }
    }
}

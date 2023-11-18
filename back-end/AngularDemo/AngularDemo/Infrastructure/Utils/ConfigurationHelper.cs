using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CEZ.AngularDemo.WebAPI.Infrastructure.Utils
{
    public static class ConfigurationHelper
    {
        private static IConfiguration _configuration;

        public static void InitializeConfiguration(IConfiguration conf)
        {
            _configuration = conf;
        }

        public static IConfiguration GetConfiguration()
        {
            return _configuration;
        }
    }
}

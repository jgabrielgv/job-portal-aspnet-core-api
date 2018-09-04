using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Helpers
{
    public class AppSettings : IAppSettings
    {
        public string ApiJwtKey => Environment.GetEnvironmentVariable("ApiJwtKey");
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway_Data_Analyzer.Utilities
{
    public interface IConfigurationServices
    {
        IConfiguration Configuration { get; }
        IHttpContextAccessor HttpContextAccessor { get; }
        Uri UserMangementBaseUri { get; }
        string Authenticate { get; }
    }
}

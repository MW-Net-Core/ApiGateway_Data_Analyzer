using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway_Data_Analyzer.Utilities
{
    public class ConfigurationServices : IConfigurationServices
    {
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ConfigurationServices(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _config = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public IConfiguration Configuration { get; }
        public IHttpContextAccessor HttpContextAccessor { get; }
        public Uri UserMangementBaseUri
        {
            get
            {
                var uri = _config.GetValue<string>("AppSettings:UserManagmentBaseURI");
                return new Uri(uri);
            }
        }
        public string Authenticate => $"{UserMangementBaseUri}{_config.GetValue<string>("AppSettings:ApiEndpoints:Authenticate")}";
    }
}

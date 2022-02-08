using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiGateway_Data_Analyzer.Utilities
{
    public interface IHttpService
    {
        HttpClient InitializeConfiguration(IHttpClientFactory clientfactory);
        void SetAuthorizationHeaderForHttpClient(IHttpContextAccessor httpContextAccessor);

    }
}

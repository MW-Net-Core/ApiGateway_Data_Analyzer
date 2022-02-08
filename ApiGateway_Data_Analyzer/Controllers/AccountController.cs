using ApiGateway_Data_Analyzer.Utilities;
using ApiGateway_Data_Analyzer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiGateway_Data_Analyzer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfigurationServices _config;
        private readonly HttpClient _http;


        public AccountController(
           IHttpClientFactory clientFactory,
           IConfigurationServices configurationService,
           IHttpContextAccessor httpContextAccessor,
           IHttpService httpServiceExtensions
           )
        {
            _config = configurationService;

            // Note: Below code initializes the HttpClient, ConfigurationService and setup the custom Authorization header for HttpClient
            if (httpServiceExtensions != null)
            {
                _http = httpServiceExtensions.InitializeConfiguration(clientFactory);
                httpServiceExtensions.SetAuthorizationHeaderForHttpClient(httpContextAccessor);
            }
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<VMResponse> Register(VMRegister reg)
        {

            using (_http) //client's object which is making a request which is furture responsed
            {
                _http.BaseAddress = new Uri(_config.Authenticate);   // path to send the post data
                _http.DefaultRequestHeaders.Accept.Clear(); //not necessary in registers case it's a safe check
                _http.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));    // data type must be json what to send

                if (reg.Password.Equals(reg.ConfirmPassword))   //checking password
                {
                    #region // Deprecated
                    //var payload = new Dictionary<string, string>
                    //{
                    //  {"UserName", reg.UserName},
                    //  {"Email", reg.Email},
                    //  {"Password", reg.Password}
                    //};

                    //string strPayload = JsonConvert.SerializeObject(payload);   // data conversion to json
                    //HttpContent c = new StringContent(strPayload, Encoding.UTF8, "application/json");
                    #endregion


                    var result = await _http.PostAsync("Register", reg.AsJson());   //await used as its async (hit on register method of usermanagement service which gives a response)

                    //the above 2 cases AsJson converting c# obj to json


                    if (result.IsSuccessStatusCode)
                    {
                        var response = await result.Content.ReadAsStringAsync();    //get data from user managment api on successfully registration of the user
                        return new VMResponse { status = "Sucess", message = "Client registered sucessfully" };

                    }
                    else
                    {
                        return new VMResponse { status = "Error", message = "Client not registered sucessfully" };
                    }

                }
                else  //password doesn't matched
                {
                    return new VMResponse { status = "Error", message = "Password Does not match" };
                }
            }
        }




    }
}

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Text;


namespace ApiGateway_Data_Analyzer.Utilities
{
    public static class JsonExtensions
    {
        public static StringContent AsJson(this object o)
            => new StringContent(JsonConvert.SerializeObject(o), Encoding.UTF8, "application/json");


        public static JObject AsObject(this string s)
            => JObject.Parse(s);


        public static T AsObject<T>(this string content) where T : class
            => JsonConvert.DeserializeObject<T>(content);
    }
}

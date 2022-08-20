using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CanWeFixItApi.IntegrationTests {

    public static class ContentHelper {
        public static StringContent GetStringContent(object obj)
            => new StringContent(JsonConvert.SerializeObject(obj), Encoding.Default, "application/json");

        public static StringContent GetStringContentIgnoreNull(object obj)
            => new StringContent(JsonConvert.SerializeObject(obj, Formatting.None,
                new JsonSerializerSettings {
                    NullValueHandling = NullValueHandling.Ignore
                }), Encoding.Default, "application/json");



        public static StringContent GetStringCSVContent(string CSVString)
            => new StringContent(CSVString, Encoding.Default, "text/plain");

        public static async Task<T> DeserialiseResponse<T>(HttpResponseMessage response) {
            string json = await response.Content.ReadAsStringAsync();
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        }

        public static async Task<string> DeserialiseRawResponse<T>(HttpResponseMessage response) {
            return await response.Content.ReadAsStringAsync();
        }
    }

}

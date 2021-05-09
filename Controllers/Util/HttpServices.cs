using System;
using System.Text;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TestTuya.Controllers{

    public class HttpServices{

        public string responseBody;
        public async Task<ApiResponse> CallServices(object objecto, string url)
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try	
            {
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                // Pass the handler to httpclient(from you are calling api)
                HttpClient client = new HttpClient(clientHandler);
                HttpContent c = new StringContent(JsonConvert.SerializeObject(objecto), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, c);
                response.EnsureSuccessStatusCode();
                this.responseBody = await response.Content.ReadAsStringAsync();
                ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseBody);
                return apiResponse;
            }
            catch(HttpRequestException e)
            {
                 return new ApiResponse{
                    code= 201,
                    message = e.Message,
                    type = "Error"            
                };
            }
        }
    }
}
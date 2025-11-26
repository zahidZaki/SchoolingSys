//using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace SchoolingSysApiGateway.HttpCient
{
    public class HttpClientHelper
    {
        private readonly HttpClient _httpClient;

        public HttpClientHelper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

       // Generic method for making GET requests
        public async Task<TResponse> GetAsync<TResponse>(string url, string token = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TResponse>(jsonResponse);
        }

        // Generic method for making POST requests with a body
        public async Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest body, string token = null)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json")
                };

                if (!string.IsNullOrEmpty(token))
                {
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                }

                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var scheduleResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<TResponse>(jsonResponse);

                return JsonSerializer.Deserialize<TResponse>(jsonResponse);
            }
            catch (Exception ex)
            {
                return JsonSerializer.Deserialize<TResponse>(ex.Message);
            }
        }



        // Generic method for making PUT requests with a body
        public async Task<TResponse> PutAsync<TRequest, TResponse>(string url, TRequest body, string token = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, url)
            {
                Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json")
            };

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TResponse>(jsonResponse);
        }

        public async Task<TResponse> DeleteAsync<TRequest, TResponse>(string url, TRequest body, string token = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, url)
            {
                Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json")
            };

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TResponse>(jsonResponse);
        }

        public async Task<TResponse> PostAsyncWithModel<TRequest, TResponse>(string url, TRequest body, string token = null)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json")
                };

                if (!string.IsNullOrEmpty(token))
                {
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                }

                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var jsonResponse = await response.Content.ReadAsStringAsync();

                return Newtonsoft.Json.JsonConvert.DeserializeObject<TResponse>(jsonResponse);

            }
            catch (Exception ex)
            {
                return JsonSerializer.Deserialize<TResponse>(ex.Message);
            }
        }

        public async Task<string> SendRequestAsync(string body, string url, string token = null)
        {
            var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

            try
            {
                url = url + "?ekfts=" + Uri.EscapeDataString(body);
                var request = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = content
                };
                if (!string.IsNullOrEmpty(token))
                {
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                }
                // Send the POST request
                var response = await _httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return jsonResponse;
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error calling API: {response.StatusCode}, {errorResponse}");
                }
            }
            catch
            {
                throw;
            }
        }


        public async Task<string> SendGetRequestAsync(string url, string uid, string token = null)
        {
            try
            {
                // Append the id as a query parameter to the URL
                url = $"{url}?userId={Uri.EscapeDataString(uid)}";

                var request = new HttpRequestMessage(HttpMethod.Get, url);

                // Add token to the request if provided
                if (!string.IsNullOrEmpty(token))
                {
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                }

                // Send the GET request
                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return jsonResponse;
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error calling API: {response.StatusCode}, {errorResponse}");
                }
            }
            catch
            {
                throw;
            }
        }

    }
}

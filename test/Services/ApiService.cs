using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using test.Models; 

namespace test.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            var loginData = new { username, password };
            var json = JsonSerializer.Serialize(loginData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://dummyjson.com/auth/login", content);

            return response.IsSuccessStatusCode;
        }


        public async Task<List<Product>> GetProductsAsync()
        {
            var response = await _httpClient.GetAsync("https://dummyjson.com/products/");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine(json); // Log the raw JSON response
                var productResponse = JsonSerializer.Deserialize<ProductResponse>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return productResponse?.Products ?? new List<Product>();
            }

            return new List<Product>();
        }

        public async Task<Product> GetProductDetailsAsync(int productId)
        {
            var response = await _httpClient.GetAsync($"https://dummyjson.com/products/{productId}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var product = JsonSerializer.Deserialize<Product>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return product;
            }

            return null;
        }

    }

    public class ProductResponse
    {
        public List<Product> Products { get; set; }
    }
}

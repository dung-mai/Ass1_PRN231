using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;

namespace eStoreClient.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private readonly HttpClient client;
        private string ProductApiUrl = "";
        public Product Product { get; set; } = null!;

        public DetailsModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            ProductApiUrl = $"https://localhost:7263/api/Products/{id}";
            HttpResponseMessage responseMessage = await client.GetAsync(ProductApiUrl);
            if (responseMessage.IsSuccessStatusCode)
            {
                string strData = await responseMessage.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                var result = JsonSerializer.Deserialize<Product>(strData, options);
                if (result != null)
                {
                    Product = result;
                    return Page();
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
        }
    }
}

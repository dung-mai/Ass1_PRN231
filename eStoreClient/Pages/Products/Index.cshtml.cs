using BusinessObject.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Net.Http.Headers;
using System.Text.Json;
using Utility;

namespace eStoreClient.Pages.Products
{
    [Authorize(Roles = UserRoles.Admin)]
    public class IndexModel : PageModel
    {
        private readonly HttpClient client;
        private string ProductApiUrl = "";
        public List<ProductResponseDTO> Products { get; set; } = null!;

        public IndexModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            var token = HttpContext.Request.Cookies["jwt"];
            if (token != null)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            ProductApiUrl = "https://localhost:7263/api/Products";
        }

        public async Task OnGetAsync()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(ProductApiUrl);
            string strData = await responseMessage.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var resultList = JsonSerializer.Deserialize<List<ProductResponseDTO>>(strData, options);
            Products = resultList ?? new List<ProductResponseDTO>();
        }
    }
}

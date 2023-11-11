using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using BusinessObject.DTO;
using System.Net.Http.Headers;
using System.Text.Json;

namespace eStoreClient.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient client;
        private string OrderApiUrl = "";
        public List<OrderItemDTO> Orders { get; set; } = null!;

        public IndexModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            OrderApiUrl = "https://localhost:7263/api/orders";
        }

        public async Task OnGetAsync()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(OrderApiUrl);
            string strData = await responseMessage.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var resultList = JsonSerializer.Deserialize<List<OrderItemDTO>>(strData, options);
            Orders = resultList ?? new List<OrderItemDTO>();
        }
    }
}

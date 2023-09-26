using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using System.Net.Http.Headers;
using BusinessObject.DTO;
using System.Text.Json;

namespace eStoreClient.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly HttpClient client;
        private string ProductApiUrl = "";

        public EditModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        [BindProperty]
        public ProductDTO Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            ProductApiUrl = $"https://localhost:7263/api/Products/{id}";
            HttpResponseMessage responseMessage = await client.GetAsync(ProductApiUrl);
            if (!responseMessage.IsSuccessStatusCode)
            {
                return NotFound();   
            }

            string strData = await responseMessage.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var result = JsonSerializer.Deserialize<ProductDTO>(strData, options);
            if (result == null)
            {
                return NotFound();
            }

            Product = result;
            ViewData["CategoryId"] = new SelectList( await GetCategories(), "CategoryId", "CategoryName");
            return Page();
        }


        private async Task<List<CategoryDTO>> GetCategories()
        {
            string categoryApiURL = "https://localhost:7263/api/Category";
            HttpResponseMessage responseMessage = await client.GetAsync(categoryApiURL);
            string strData = await responseMessage.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var resultList = JsonSerializer.Deserialize<List<CategoryDTO>>(strData, options);
            return resultList ?? new List<CategoryDTO>();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPostAsync()
        {
            ProductApiUrl = $"https://localhost:7263/api/Products/{Product.ProductId}";
            var response = client.PutAsJsonAsync(ProductApiUrl, Product).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                return Page();
            }
        }
    }
}

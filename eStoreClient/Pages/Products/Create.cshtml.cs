using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.Models;
using System.Net.Http.Headers;
using BusinessObject.DTO;
using System.Text.Json;
using Utility;
using Microsoft.AspNetCore.Authorization;

namespace eStoreClient.Pages.Products
{
    [Authorize(Roles = UserRoles.Admin)]
    public class CreateModel : PageModel
    {
        private readonly HttpClient client;
        private string ProductApiUrl = "";

        public CreateModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        public async Task<IActionResult> OnGet()
        {
            ViewData["CategoryId"] = new SelectList(await GetCategories(), "CategoryId", "CategoryName");
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


        [BindProperty]
        public Product Product { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPostAsync()
        {
            ProductApiUrl = "https://localhost:7263/api/Products";
            var response = client.PostAsJsonAsync(ProductApiUrl, Product).Result;
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

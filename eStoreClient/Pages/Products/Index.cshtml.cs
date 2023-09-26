﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using System.Net.Http.Headers;
using System.Text.Json;
using BusinessObject.DTO;

namespace eStoreClient.Pages.Products
{
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

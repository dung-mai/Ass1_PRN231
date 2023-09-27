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

namespace eStoreClient.Pages.Members
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient client;
        private string MemberApiUrl = "";
        public List<MemberResponseDTO> Members { get; set; } = null!;

        public IndexModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            MemberApiUrl = "https://localhost:7263/api/Members";
        }

        public async Task OnGetAsync()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(MemberApiUrl);
            string strData = await responseMessage.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var resultList = JsonSerializer.Deserialize<List<MemberResponseDTO>>(strData, options);
            Members = resultList ?? new List<MemberResponseDTO>();
        }
    }
}

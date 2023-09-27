using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using BusinessObject.DTO;
using System.Net.Http.Headers;
using System.Text.Json;

namespace eStoreClient.Pages.Members
{
    public class EditModel : PageModel
    {
        private readonly HttpClient client;
        private string MemberApiUrl = "";

        public EditModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        [BindProperty]
        public MemberResponseDTO Member { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            MemberApiUrl = $"https://localhost:7263/api/Members/{id}";
            HttpResponseMessage responseMessage = await client.GetAsync(MemberApiUrl);
            if (!responseMessage.IsSuccessStatusCode)
            {
                return NotFound();
            }

            string strData = await responseMessage.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var result = JsonSerializer.Deserialize<MemberResponseDTO>(strData, options);
            if (result == null)
            {
                return NotFound();
            }

            Member = result;
            return Page();
        }

        public IActionResult OnPostAsync()
        {
            MemberApiUrl = $"https://localhost:7263/api/Members/{Member.MemberId}";
            var response = client.PutAsJsonAsync(MemberApiUrl, Member).Result;
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

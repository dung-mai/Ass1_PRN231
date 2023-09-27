using BusinessObject.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;

namespace eStoreClient.Pages.Members
{
    public class DetailsModel : PageModel
    {
        private readonly HttpClient client;
        private string MemberApiUrl = "";
        public MemberResponseDTO Member { get; set; } = null!;

        public DetailsModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            MemberApiUrl = $"https://localhost:7263/api/Members/{id}";
            HttpResponseMessage responseMessage = await client.GetAsync(MemberApiUrl);
            if (responseMessage.IsSuccessStatusCode)
            {
                string strData = await responseMessage.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                var result = JsonSerializer.Deserialize<MemberResponseDTO>(strData, options);
                if (result != null)
                {
                    Member = result;
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

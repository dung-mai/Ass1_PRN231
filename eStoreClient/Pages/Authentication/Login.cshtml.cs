using BusinessObject.DTO;
using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Security.Principal;
using System.Text.Json;
using Utility;

namespace eStoreClient.Pages.Authentication
{
    public class LoginModel : PageModel
    {
        private readonly HttpClient client;
        private string MemberApiUrl = "";
        public LoginModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        public IActionResult OnGet()
        {
            return CheckHasLogin();
        }

        public async Task<IActionResult> OnPost(LoginDTO loginDTO)
        {
            MemberApiUrl = $"https://localhost:7263/api/Members/login";
            HttpResponseMessage response = client.PostAsJsonAsync(MemberApiUrl, loginDTO).Result;
            if (!response.IsSuccessStatusCode)
            {
                TempData["error"] = "Vui lòng kiểm tra lại email và mật khẩu!";
                return Page();
            }
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            if (strData.Contains("fail"))
            {
                TempData["error"] = "Vui lòng kiểm tra lại email và mật khẩu!";
                return Page();
            }
            else if(strData.Contains("admin"))
            {
                HttpContext.Session.SetString("loggedInAccount", ConstantValues.ADMIN_ROLE);
                return DefaultPageByRole(ConstantValues.ADMIN_ROLE);
            } else
            {
                var result = JsonSerializer.Deserialize<MemberResponseDTO>(strData, options);
                HttpContext.Session.SetString("loggedInAccount", result.MemberId.ToString());
                return DefaultPageByRole(ConstantValues.MEMBER_ROLE);
            }
        }

        private IActionResult DefaultPageByRole(string account)
        {
            if (account == ConstantValues.ADMIN_ROLE)
            {
                return RedirectToPage(ConstantValues.DEFAULT_ADMIN_PAGE);
            }
            else
            {
                return RedirectToPage(ConstantValues.DEFAULT_MEMBER_PAGE);
            }
        }

        private IActionResult CheckHasLogin()
        {
            string? account = HttpContext.Session.GetString(ConstantValues.LOGIN_ACCOUNT_SESSION_NAME);

            if (account is null)
            {
                return Page();
            }
            else
            {
                return DefaultPageByRole(account);
            }
        }
    }
}

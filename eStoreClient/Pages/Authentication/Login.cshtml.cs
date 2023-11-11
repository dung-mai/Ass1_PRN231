using BusinessObject.DTO;
using BusinessObject.DTO.Response;
using BusinessObject.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.Protocol.Plugins;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using Utility;

namespace eStoreClient.Pages.Authentication
{
    public class LoginModel : PageModel
    {
        private readonly HttpClient client;
        private string LoginApi = "";
        public LoginModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            LoginApi = $"https://localhost:7263/api/login";
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<ActionResult> OnPost(LoginDTO loginDTO)
        {
            var jwt = await LoginAsync(loginDTO);
            if (!string.IsNullOrEmpty(jwt))
            {
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTime.UtcNow.AddHours(1)
                };
                Response.Cookies.Append("jwt", jwt, cookieOptions);

                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(jwt) as JwtSecurityToken;
                if (jsonToken != null)
                {
                    var claims = jsonToken.Claims;
                    //var roles = claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).ToList();
                    var claimsIdentity = new ClaimsIdentity(claims,
                        CookieAuthenticationDefaults.AuthenticationScheme);

                    var properties = new AuthenticationProperties()
                    {
                        AllowRefresh = true,
                        IsPersistent = true
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity), properties);
                }
                return RedirectToAction("Index", "Home");
            }

            else
            {
                ViewData["ErrorMessage"] = "Tên đăng nhập hoặc mật khẩu không đúng.";
                return Page();
            }
        }

        public async Task<string> LoginAsync(LoginDTO loginDTO)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(loginDTO), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(LoginApi, jsonContent);
            var strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };
            var jwt = JsonSerializer.Deserialize<LoginResponse>(strData, options);
            return jwt.Token;
        }
    }
}

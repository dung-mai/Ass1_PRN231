using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Utility;

namespace eStoreClient.Pages.Utils
{
    public class PageModelBase : PageModel
    {
        //public string? LoggedInAccount { get; set; }
        //public string[] authorizedRoles = new string[] { };

        //public PageModelBase()
        //{

        //}

        //private IActionResult DefaultPageByRole(Account account)
        //{
        //    if (account.Role?.RoleName?.ToLower() == ConstantValues.ADMIN_ROLE)
        //    {
        //        return RedirectToPage(ConstantValues.DEFAULT_ADMINPAGE);
        //    }
        //    else
        //    {
        //        return RedirectToPage(ConstantValues.DEFAULT_MANAGE_PAGE);
        //    }
        //}

        //private IActionResult RedirectToPage(object dEFAULT_ADMINPAGE)
        //{
        //    throw new NotImplementedException();
        //}

        //private IActionResult RedirectToPage(object dEFAULT_MANAGE_PAGE)
        //{
        //    throw new NotImplementedException();
        //}

        //public bool HasAuthorized()
        //{
        //    LoggedInAccount = HttpContext.Session.Get<Account>(ConstantValues.LOGIN_ACCOUNT_SESSION_NAME);

        //    //NOT LOGIN + GUEST can acess --> true
        //    if (LoggedInAccount is null)
        //    {
        //        if (IsGuestFeature())
        //        {
        //            return true;
        //        }
        //    }
        //    //HAS LOGIN + check role
        //    else
        //    {
        //        foreach (string r in authorizedRoles)
        //        {
        //            if (r == LoggedInAccount?.Role?.RoleName)
        //            {
        //                return true;
        //            }
        //        }
        //    }
        //    return false;
        //}

        //public IActionResult LoginBasedFeatureRedirect()
        //{
        //    if (LoggedInAccount is null)
        //    {
        //        return RedirectToPage("/Login");
        //    }
        //    else
        //    {
        //        return DefaultPageByRole(LoggedInAccount);
        //    }
        //}

        //private bool IsGuestFeature()
        //{
        //    foreach (string role in authorizedRoles)
        //    {
        //        if (role == ConstantValues.GUEST_ROLE)
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}
    }
}

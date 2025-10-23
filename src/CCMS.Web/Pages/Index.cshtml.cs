using Microsoft.AspNetCore.Mvc;

namespace CCMS.Web.Pages;

public class IndexModel : CCMSPageModel
{
    public IActionResult OnGet()
    {
        // لو عايزة تستخدم اسم الصفحة بدل URL:
        // return RedirectToPage("/Dashboard/Index");
        return Redirect("/Dashboard");
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CCMS.Stores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Volo.Abp.Application.Dtos;

namespace CCMS.Web.Pages.Store;

public class IndexModel : PageModel
{
    private readonly IStoreAppService _svc;

    public List<StoreDto> Items { get; set; } = new();

    public IndexModel(IStoreAppService svc) => _svc = svc;

    public async Task OnGetAsync()
    {
        var res = await _svc.GetListAsync(new PagedAndSortedResultRequestDto { MaxResultCount = 200 });
        Items = res.Items?.ToList() ?? new();
    }

    public async Task<IActionResult> OnPostCreateAsync(CreateUpdateStoreDto input)
    {
        await _svc.CreateAsync(input);
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostEditAsync(Guid id, CreateUpdateStoreDto input)
    {
        await _svc.UpdateAsync(id, input);
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostDeleteAsync(Guid id)
    {
        await _svc.DeleteAsync(id);
        return RedirectToPage();
    }
}

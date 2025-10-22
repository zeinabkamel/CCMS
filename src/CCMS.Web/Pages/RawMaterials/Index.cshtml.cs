using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CCMS.Stores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Volo.Abp.Application.Dtos;

namespace CCMS.Web.Pages.RawMaterials;

public class IndexModel : PageModel
{
    private readonly IRawMaterialAppService _materials;
    private readonly IStoreAppService _stores;

    public List<RawMaterialDto> Items { get; set; } = new();
    public List<StoreDto> Stores { get; set; } = new();
    public Dictionary<Guid, string> StoreLookup { get; set; } = new();

    public IndexModel(IRawMaterialAppService materials, IStoreAppService stores)
    {
        _materials = materials;
        _stores = stores;
    }

    public async Task OnGetAsync()
    {
        var mats = await _materials.GetListAsync(new PagedAndSortedResultRequestDto { MaxResultCount = 500 });
        Items = mats.Items?.ToList() ?? new();

        var st = await _stores.GetListAsync(new PagedAndSortedResultRequestDto { MaxResultCount = 500 });
        Stores = st.Items?.ToList() ?? new();

        StoreLookup = Stores.ToDictionary(x => x.Id, x => x.Name);
    }

    public async Task<IActionResult> OnPostCreateAsync(CreateUpdateRawMaterialDto input)
    {
        await _materials.CreateAsync(input);
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostEditAsync(Guid id, CreateUpdateRawMaterialDto input)
    {
        await _materials.UpdateAsync(id, input);
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostDeleteAsync(Guid id)
    {
        await _materials.DeleteAsync(id);
        return RedirectToPage();
    }
}

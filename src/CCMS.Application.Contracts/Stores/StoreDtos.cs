using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace CCMS.Stores;

public class StoreDto : AuditedEntityDto<Guid>
{
    public string Name { get; set; } = default!;
    public string? Location { get; set; }
    public string? Description { get; set; }
}

public class CreateUpdateStoreDto
{
    [Required, StringLength(128)] public string Name { get; set; } = default!;
    [StringLength(128)] public string? Location { get; set; }
    [StringLength(1024)] public string? Description { get; set; }
}

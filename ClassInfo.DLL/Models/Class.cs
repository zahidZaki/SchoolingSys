using System;
using System.Collections.Generic;

namespace ClassInfo.DLL.Models;

public partial class Class
{
    public int? Id { get; set; }

    public int? ClsId { get; set; }

    public string? ClsName { get; set; }

    public string? ClsSection { get; set; }

    public string? ClsRoomNo { get; set; }

    public string? ClsTeacherId { get; set; }
}

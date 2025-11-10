using System;
using System.Collections.Generic;

namespace Teacherinfo.DLL.Models;

public partial class TeacherTable
{
    public int TeacherId { get; set; }

    public string? TeacherName { get; set; }

    public string? TeacherEmail { get; set; }

    public string? TeacherPhone { get; set; }

    public DateTime? TeacherHireDate { get; set; }

    public string? TeacherQualification { get; set; }

    public bool? TeacherIsActive { get; set; }
}

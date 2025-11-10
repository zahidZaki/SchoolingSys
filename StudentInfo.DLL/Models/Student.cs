using System;
using System.Collections.Generic;

namespace StudentInfo.DLL.Models;

public partial class Student
{
    public int StdId { get; set; }

    public string? StdRollNumber { get; set; }

    public string? StdFirstName { get; set; }

    public string? StdLastName { get; set; }

    public DateTime? StdDateOfBirth { get; set; }

    public string? StdGender { get; set; }

    public string? StdEmail { get; set; }

    public int? ClsId { get; set; }
}

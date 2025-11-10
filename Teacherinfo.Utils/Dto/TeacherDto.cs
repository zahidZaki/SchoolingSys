using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teacherinfo.Utils.Dto
{
    public class TeacherDto
    {
        public int TeacherId { get; set; }

        public string? TeacherName { get; set; }

        public string? TeacherEmail { get; set; }

        public string? TeacherPhone { get; set; }

        public DateTime? TeacherHireDate { get; set; }

        public string? TeacherQualification { get; set; }

        public bool? TeacherIsActive { get; set; }
    }
}

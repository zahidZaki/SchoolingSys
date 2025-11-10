using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfo.Utils.Dto
{
    public class StudentDto
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
}

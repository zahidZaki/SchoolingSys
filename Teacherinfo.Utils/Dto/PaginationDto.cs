using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teacherinfo.Utils.Dto
{
    public class PaginationDto
    {
        public int? Take { get; set; }
        public int? Skip { get; set; }
        public string? SortOrder { get; set; }
        public string? SortColumn { get; set; }
        public string? SortField { get; set; }
        public string? SortDirection { get; set; }
        public string? SearchText { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    
    }
}

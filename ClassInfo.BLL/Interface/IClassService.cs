using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassInfo.Utils.Dto;

namespace ClassInfo.BLL.Interface
{
    public interface IClassService
    {
        Task<(List<ClassDto> data, string record, int totalCount)> GetAllClasses(PaginationDto request);
        Task<ClassDto?> GetClassById(int id);
        Task<ResponseObj> AddClass(ClassDto dto);
        Task<ResponseObj> UpdateClass(ClassDto dto);
        Task<string> DeleteClass(int id);
     
    }
}

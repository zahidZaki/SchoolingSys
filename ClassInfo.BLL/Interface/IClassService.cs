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
        Task<(List<ClassDto> data, string record, int totalCount)> GetAll(PaginationDto request);
        Task<ClassDto?> GetById(int id);
        Task<ResponseObj> Add(ClassDto dto);
        Task<ResponseObj> Update(ClassDto systemOutageDto);
        Task<string> Delete(int id);
        Task<ResponseObj> AddOneHourToOutage(ClassDto systemOutageDto);

        Task<(List<ClassDto> data, string record)> GetFacilityDownTime();
    }
}

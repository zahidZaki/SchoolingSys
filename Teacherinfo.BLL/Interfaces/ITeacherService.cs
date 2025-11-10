using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teacherinfo.Utils.Dto;

namespace Teacherinfo.BLL.Interfaces
{
    public interface ITeacherService
    {
        Task<(List<TeacherDto> data, string record, int totalCount)> GetAll(PaginationDto request);
        Task<TeacherDto?> GetById(int id);
        Task<ResponseObj> Add(TeacherDto dto);
        Task<ResponseObj> Update(TeacherDto systemOutageDto);
        Task<string> Delete(int id);
        Task<ResponseObj> AddOneHourToOutage(TeacherDto systemOutageDto);

        Task<(List<TeacherDto> data, string record)> GetFacilityDownTime();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInfo.Utils.Dto;

namespace StudentInfo.BLL.Interfaces
{
    public interface IStudentService
    {
        Task<(List<StudentDto> data, string record, int totalCount)> GetAll(PaginationDto request);
        Task<StudentDto?> GetById(int id);
        Task<ResponseObj> Add(StudentDto dto);
        Task<ResponseObj> Update(StudentDto systemOutageDto);
        Task<string> Delete(int id);
        Task<ResponseObj> AddOneHourToOutage(StudentDto systemOutageDto);

        Task<(List<StudentDto> data, string record)> GetFacilityDownTime();
    }
}

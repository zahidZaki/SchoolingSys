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
        Task<(List<StudentDto> data, string record, int totalCount)> GetAllStudent(PaginationDto request);
        Task<StudentDto?> GetStudentById(int id);
        Task<ResponseObj> AddStudent(StudentDto dto);
        Task<ResponseObj> UpdateStudent(StudentDto dto);
        Task<string> DeleteStudent(int id);
        Task<ResponseObj> AddOneHourToOutage(StudentDto dto);
        Task<(List<StudentDto> data, string record)> GetFacilityDownTime();
    }
}

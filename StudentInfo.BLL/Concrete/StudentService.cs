using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudentInfo.DLL.ExtensionMethods;
using AutoMapper;
using StudentInfo.BLL.Concrete.Base;
using StudentInfo.BLL.Interfaces;
using StudentInfo.DLL.Interfaces.IBase;
using StudentInfo.DLL.Models;
using StudentInfo.Utils.Dto;
using Microsoft.EntityFrameworkCore;
using StudentInfo.Utils.ExtensionMethods;

namespace StudentInfo.BLL.Concrete
{
    public class StudentService :BaseService ,IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public StudentService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<(List<StudentDto> data, string record, int totalCount)> GetAllStudent(PaginationDto request)
        {
            try
            {
                string records = "NoRecords";

                var resultList = await _unitOfWork.StudentDbContext
                    .LoadStoredProc("[dbo].[usp_GetHelpDeskSystemOutages]")
                    .WithSqlParam("@Take", request.Take ?? 0)
                    .WithSqlParam("@Skip", request.Skip ?? 0)
                    .WithSqlParam("@SortOrder", request.SortOrder)
                    .WithSqlParam("@SortColumn", request.SortColumn)
                    .WithSqlParam("@SearchText", request.SearchText)
                    .ExecuteStoredProc<StudentDto>();


                foreach (var item in resultList)
                {
                    if (item != null)
                    {
                        records = "RecordsFound";

                    }
                }
                var filteredQuery = _unitOfWork.StudentDbContext.Students.AsQueryable();
                if (!string.IsNullOrEmpty(request.SearchText))
                {
                    filteredQuery = filteredQuery.Where(t =>
                        (request.SearchText == null || t.StdFirstName.Contains(request.SearchText)));
                }
                var totalCount = await filteredQuery.CountAsync();

                return (resultList, records, totalCount);
            }
            catch (Exception ex)
            {
                throw;
            }
        }




        public async Task<StudentDto?> GetStudentById(int id)
        {
            try
            {


                var entity = await _unitOfWork.StudentDbContext.Students.FirstOrDefaultAsync(x => x.StdId == id);
                var model = _mapper.Map<StudentDto>(entity);

                return model ?? new StudentDto();

            }
            catch (Exception)
            {
                throw;
            }

        }
        public async Task<ResponseObj> AddStudent(StudentDto model)
        {
            try
            {
                var entity = _mapper.Map<Student>(model);

                var now = DateTime.Now.ToEST();


                await _unitOfWork.StudentDbContext.Students.AddAsync(entity);
                await _unitOfWork.StudentDbContext.SaveChangesAsync();


                return new ResponseObj { data = entity };
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseObj> UpdateStudent(StudentDto dto)
        {
            try
            {
                var res = await _unitOfWork.StudentDbContext.Students.Where(x => x.StdId == dto.StdId).FirstOrDefaultAsync();
                if (res == null)
                {
                    return (new ResponseObj { data = null });
                }
                res.StdId =dto.StdId;


                _unitOfWork.StudentDbContext.Students.Update(res);
                await _unitOfWork.StudentDbContext.SaveChangesAsync();

                return (new ResponseObj { data = res });
            }
            catch (Exception)
            {
                throw;

            }
        }
        public async Task<string> DeleteStudent(int id)
        {
            try
            {
                var entity = await _unitOfWork.StudentDbContext.Students.FindAsync(id);
                if (entity == null)
                {
                    return $"Student with ID  does not exist.";
                }


                _unitOfWork.StudentDbContext.Students.Remove(entity);
                await _unitOfWork.StudentDbContext.SaveChangesAsync();

                return "Student deleted successfully.";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseObj> AddOneHourToOutage(StudentDto dto)
        {
            try
            {
                var entity = await _unitOfWork.StudentDbContext.Students.Where(x => x.StdId == dto.StdId).FirstOrDefaultAsync();
                if (entity == null)
                {
                    return (new ResponseObj { data = null });
                }

                //entity.HsoEndTime = DateTime.Now.ToEST().AddHours(1);
                //entity.HsoModifiedByName = systemOutageDto.HsoModifiedByName;

                _unitOfWork.StudentDbContext.Students.Update(entity);
                await _unitOfWork.StudentDbContext.SaveChangesAsync();



                return (new ResponseObj { data = entity });
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<(List<StudentDto> data, string record)> GetFacilityDownTime()
        {
            try
            {
                var resultList = await _unitOfWork.StudentDbContext
            .LoadStoredProc("[usp_GetFacilityDownTime]")
            .ExecuteStoredProc<StudentDto>();

                string recordStatus = (resultList != null && resultList.Any()) ? "RecordsFound" : "NoRecords";

                return (resultList ?? new List<StudentDto>(), recordStatus);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}

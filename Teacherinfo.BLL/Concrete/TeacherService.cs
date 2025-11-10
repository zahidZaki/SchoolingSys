using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Teacherinfo.DLL.ExtensionMethods;
using AutoMapper;
using Teacherinfo.BLL.Interfaces;
using Teacherinfo.DLL.Interfaces.IBase;
using Teacherinfo.Utils.Dto;
using Teacherinfo.Utils.ExtensionMethods;
using Microsoft.EntityFrameworkCore;
using Teacherinfo.DLL.Models;
using Teacherinfo.BLL.Concrete.Base;

namespace Teacherinfo.BLL.Concrete
{
    public class TeacherService : BaseService ,ITeacherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper  _mapper;
        public TeacherService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<(List<TeacherDto> data, string record, int totalCount)> GetAll(PaginationDto request)
        {
            try
            {
                string records = "NoRecords";

                var resultList = await _unitOfWork.TeachterDbContext
                    .LoadStoredProc("[dbo].[usp_GetHelpDeskSystemOutages]")
                    .WithSqlParam("@Take", request.Take ?? 0)
                    .WithSqlParam("@Skip", request.Skip ?? 0)
                    .WithSqlParam("@SortOrder", request.SortOrder)
                    .WithSqlParam("@SortColumn", request.SortColumn)
                    .WithSqlParam("@SearchText", request.SearchText)
                    .ExecuteStoredProc<TeacherDto>();


                foreach (var item in resultList)
                {
                    if (item != null)
                    {
                        records = "RecordsFound";

                    }
                }
                var filteredQuery = _unitOfWork.TeachterDbContext.TeacherTables.AsQueryable();
                if (!string.IsNullOrEmpty(request.SearchText))
                {
                    filteredQuery = filteredQuery.Where(t =>
                        (request.SearchText == null || t.TeacherName.Contains(request.SearchText)));
                }
                var totalCount = await filteredQuery.CountAsync();

                return (resultList, records, totalCount);
            }
            catch (Exception ex)
            {
                throw;
            }
        }




        public async Task<TeacherDto?> GetById(int id)
        {
            try
            {


                var entity = await _unitOfWork.TeachterDbContext.TeacherTables.FirstOrDefaultAsync(x => x.TeacherId == id);
                var model = _mapper.Map<TeacherDto>(entity);

                return model ?? new TeacherDto();

            }
            catch (Exception)
            {
                throw;
            }

        }
        public async Task<ResponseObj> Add(TeacherDto model)
        {
            try
            {
                var entity = _mapper.Map<TeacherTable>(model);

                var now = DateTime.Now.ToEST();
                

                await _unitOfWork.TeachterDbContext.TeacherTables.AddAsync(entity);
                await _unitOfWork.TeachterDbContext.SaveChangesAsync();


                return new ResponseObj { data = entity };
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseObj> Update(TeacherDto systemOutageDto)
        {
            try
            {
                var res = await _unitOfWork.TeachterDbContext.TeacherTables.Where(x => x.TeacherId == systemOutageDto.TeacherId).FirstOrDefaultAsync();
                if (res == null)
                {
                    return (new ResponseObj { data = null });
                }
                res.TeacherId = systemOutageDto.TeacherId;


                _unitOfWork.TeachterDbContext.TeacherTables.Update(res);
                await _unitOfWork.TeachterDbContext.SaveChangesAsync();

                return (new ResponseObj { data = res });
            }
            catch (Exception)
            {
                throw;

            }
        }
        public async Task<string> Delete(int id)
        {
            try
            {
                var entity = await _unitOfWork.TeachterDbContext.TeacherTables.FindAsync(id);
                if (entity == null)
                {
                    return $"System Outage with ID  does not exist.";
                }


                _unitOfWork.TeachterDbContext.TeacherTables.Remove(entity);
                await _unitOfWork.TeachterDbContext.SaveChangesAsync();

                return "System Outage deleted successfully.";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseObj> AddOneHourToOutage(TeacherDto systemOutageDto)
        {
            try
            {
                var entity = await _unitOfWork.TeachterDbContext.TeacherTables.Where(x => x.TeacherId == systemOutageDto.TeacherId).FirstOrDefaultAsync();
                if (entity == null)
                {
                    return (new ResponseObj { data = null });
                }

                //entity.HsoEndTime = DateTime.Now.ToEST().AddHours(1);
                //entity.HsoModifiedByName = systemOutageDto.HsoModifiedByName;

                _unitOfWork.TeachterDbContext.TeacherTables.Update(entity);
                await _unitOfWork.TeachterDbContext.SaveChangesAsync();



                return (new ResponseObj { data = entity });
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<(List<TeacherDto> data, string record)> GetFacilityDownTime()
        {
            try
            {
                var resultList = await _unitOfWork.TeachterDbContext
            .LoadStoredProc("[usp_GetFacilityDownTime]")
            .ExecuteStoredProc<TeacherDto>();

                string recordStatus = (resultList != null && resultList.Any()) ? "RecordsFound" : "NoRecords";

                return (resultList ?? new List<TeacherDto>(), recordStatus);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}

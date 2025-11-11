using System;
using System.Collections.Generic;
using System.Linq;
using ClassInfo.Utils.ExtensionMethods;
using ClassInfo.DLL.ExtensionMethods;
using AutoMapper;
using ClassInfo.BLL.Concrete.Base;
using ClassInfo.BLL.Interface;
using ClassInfo.DLL.Interfaces.IBase;
using ClassInfo.Utils.Dto;
using Microsoft.EntityFrameworkCore;
using ClassInfo.DLL.Models;

namespace ClassInfo.BLL.Concrete
{
    public class ClassService : BaseService, IClassService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ClassService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<(List<ClassDto> data, string record, int totalCount)> GetAll(PaginationDto request)
        {
            try
            {
                string records = "NoRecords";

                var resultList = await _unitOfWork.ClassesDbContext
                    .LoadStoredProc("[dbo].[usp_GetHelpDeskSystemOutages]")
                    .WithSqlParam("@Take", request.Take ?? 0)
                    .WithSqlParam("@Skip", request.Skip ?? 0)
                    .WithSqlParam("@SortOrder", request.SortOrder)
                    .WithSqlParam("@SortColumn", request.SortColumn)
                    .WithSqlParam("@SearchText", request.SearchText)
                    .ExecuteStoredProc<ClassDto>();


                foreach (var item in resultList)
                {
                    if (item != null)
                    {
                        records = "RecordsFound";

                    }
                }
                var filteredQuery = _unitOfWork.ClassesDbContext.Classes.AsQueryable();
                if (!string.IsNullOrEmpty(request.SearchText))
                {
                    filteredQuery = filteredQuery.Where(t =>
                        (request.SearchText == null || t.ClsName.Contains(request.SearchText)));
                }
                var totalCount = await filteredQuery.CountAsync();

                return (resultList, records, totalCount);
            }
            catch (Exception ex)
            {
                throw;
            }
        }




        public async Task<ClassDto?> GetById(int id)
        {
            try
            {


                var entity = await _unitOfWork.ClassesDbContext.Classes.FirstOrDefaultAsync(x => x.ClsId == id);
                var model = _mapper.Map<ClassDto>(entity);

                return model ?? new ClassDto();

            }
            catch (Exception)
            {
                throw;
            }

        }
        public async Task<ResponseObj> Add(ClassDto model)
        {
            try
            {
                var entity = _mapper.Map<Class>(model);

                var now = DateTime.Now.ToEST();


                await _unitOfWork.ClassesDbContext.Classes.AddAsync(entity);
                await _unitOfWork.ClassesDbContext.SaveChangesAsync();


                return new ResponseObj { data = entity };
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseObj> Update(ClassDto systemOutageDto)
        {
            try
            {
                var res = await _unitOfWork.ClassesDbContext.Classes.Where(x => x.ClsId == systemOutageDto.ClsId).FirstOrDefaultAsync();
                if (res == null)
                {
                    return (new ResponseObj { data = null });
                }
                res.ClsId = systemOutageDto.ClsId;


                _unitOfWork.ClassesDbContext.Classes.Update(res);
                await _unitOfWork.ClassesDbContext.SaveChangesAsync();

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
                var entity = await _unitOfWork.ClassesDbContext.Classes.FindAsync(id);
                if (entity == null)
                {
                    return $"System Outage with ID  does not exist.";
                }


                _unitOfWork.ClassesDbContext.Classes.Remove(entity);
                await _unitOfWork.ClassesDbContext.SaveChangesAsync();

                return "System Outage deleted successfully.";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseObj> AddOneHourToOutage(ClassDto systemOutageDto)
        {
            try
            {
                var entity = await _unitOfWork.ClassesDbContext.Classes.Where(x => x.ClsId == systemOutageDto.ClsId).FirstOrDefaultAsync();
                if (entity == null)
                {
                    return (new ResponseObj { data = null });
                }

                //entity.HsoEndTime = DateTime.Now.ToEST().AddHours(1);
                //entity.HsoModifiedByName = systemOutageDto.HsoModifiedByName;

                _unitOfWork.ClassesDbContext.Classes.Update(entity);
                await _unitOfWork.ClassesDbContext.SaveChangesAsync();



                return (new ResponseObj { data = entity });
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<(List<ClassDto> data, string record)> GetFacilityDownTime()
        {
            try
            {
                var resultList = await _unitOfWork.ClassesDbContext
            .LoadStoredProc("[usp_GetFacilityDownTime]")
            .ExecuteStoredProc<ClassDto>();

                string recordStatus = (resultList != null && resultList.Any()) ? "RecordsFound" : "NoRecords";

                return (resultList ?? new List<ClassDto>(), recordStatus);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
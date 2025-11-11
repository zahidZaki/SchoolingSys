using AutoMapper;
using ClassInfo.Api.Controllers.Base;
using ClassInfo.BLL.Interface;
using ClassInfo.Utils.Dto;
using ClassInfo.Utils.ExtensionMethods;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static ClassInfo.Utils.Dto.ResponseMessageExtensions;

namespace ClassInfo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassInfoController : BaseController
    {
            private readonly IClassService _teacherService;

            public ClassInfoController(IConfiguration configuration, IMapper mapper, IClassService teacherService)
                : base(configuration, mapper)
            {
                _teacherService = teacherService;
            }

            [HttpPost]
            [Route("GetAllTeacher")]
            public async Task<IActionResult> GetAllSystemOutages([FromBody] PaginationDto paginationDto)
            {
                var (resultList, records, totalCount) = await _teacherService.GetAll(paginationDto);
                if (resultList == null)
                {
                    return new JsonResult(new { status = ApiResponseStatus.Success.ToInt(), msg = "System Outage not found." });
                }
                return new JsonResult(new { status = ApiResponseStatus.Success.ToInt(), msg = ResponseMessages.Success.GetMessage(), data = resultList, records = records, totalCount });
            }

            [HttpGet]
            [Route("GetTeacherById")]
            public async Task<JsonResult> GetSystemOutageById(int id)
            {
                ResponseObj obj = new ResponseObj();
                obj.data = await _teacherService.GetById(id);

                if (obj.data == null)
                {
                    return new JsonResult(new { status = ApiResponseStatus.Failure.ToInt(), msg = "System Outage not found." });
                }

                return new JsonResult(new
                {
                    status = ApiResponseStatus.Success.ToInt(),
                    msg = ResponseMessages.Success.GetMessage(),
                    data = obj.data
                });
            }


            [HttpPost]
            [Route("AddTeacher")]
            public async Task<IActionResult> AddSystemOutage([FromBody] ClassDto systemOutageDto)
            {
                var resultList = await _teacherService.Add(systemOutageDto);

                return new JsonResult(new { status = ApiResponseStatus.Success.ToInt(), msg = ResponseMessages.Success.GetMessage(), data = resultList });

            }

            [HttpPut]
            [Route("UpdateTeacher")]
            public async Task<IActionResult> UpdateSystemOutage([FromBody] ClassDto systemOutageDto)
            {
                var res = await _teacherService.Update(systemOutageDto);


                if (res != null)
                {
                    return new JsonResult(new
                    {
                        status = ApiResponseStatus.Success.ToInt(),
                        msg = ResponseMessages.Success.GetMessage(),
                        data = res.data
                    });
                }
                if (res == null)
                {
                    return new JsonResult(new
                    {
                        status = ApiResponseStatus.Success.ToInt(),
                        msg = ResponseMessages.Success.GetMessage(),
                    });
                }

                return new JsonResult(new
                {
                    status = ApiResponseStatus.Success.ToInt(),
                    msg = ResponseMessages.ErrorOccurred.GetMessage()
                });
            }

            [HttpDelete]
            [Route("DeleteTeacher")]
            public async Task<IActionResult> DeleteSystemOutage(int id)
            {
                var res = await _teacherService.Delete(id);


                if (res == "System Outage deleted successfully.")
                {
                    return new JsonResult(new { status = ApiResponseStatus.Success.ToInt(), msg = res });
                }
                if (res == "System Outage with ID  does not exist.")
                {
                    return new JsonResult(new { status = ApiResponseStatus.Success.ToInt(), msg = res });
                }
                return new JsonResult(new { status = ApiResponseStatus.Success.ToInt(), msg = ResponseMessages.ErrorOccurred.GetMessage() });

            }
            [HttpPost]
            [Route("ToggleOneHour")]
            public async Task<IActionResult> ToggleOneHour([FromBody] ClassDto systemOutageDto)
            {
                var res = await _teacherService.AddOneHourToOutage(systemOutageDto);

                if (res == null)
                {
                    return new JsonResult(new { status = ApiResponseStatus.Success.ToInt(), msg = ResponseMessages.Success.GetMessage() });
                }

                return new JsonResult(new { status = ApiResponseStatus.Success.ToInt(), msg = ResponseMessages.Success.GetMessage(), date = res.data });
            }

            [HttpGet]
            [Route("GetFacilityDownTime")]
            public async Task<JsonResult> GetFacilityDownTime()
            {

                var res = await _teacherService.GetFacilityDownTime();
                return new JsonResult(new { status = ApiResponseStatus.Success.ToInt(), msg = ResponseMessages.Success.GetMessage(), data = res.data, records = res.record });
            }
        }
    }

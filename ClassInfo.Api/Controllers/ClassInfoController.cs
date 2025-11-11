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
            private readonly IClassService _classService;

            public ClassInfoController(IConfiguration configuration, IMapper mapper, IClassService classService)
                : base(configuration, mapper)
            {
            _classService = classService;
            }

            [HttpPost]
            [Route("GetAllClasses")]
            public async Task<IActionResult> GetAllClasses([FromBody] PaginationDto paginationDto)
            {
                var (resultList, records, totalCount) = await _classService.GetAllClasses(paginationDto);
                if (resultList == null)
                {
                    return new JsonResult(new { status = ApiResponseStatus.Success.ToInt(), msg = "Class not found." });
                }
                return new JsonResult(new { status = ApiResponseStatus.Success.ToInt(), msg = ResponseMessages.Success.GetMessage(), data = resultList, records = records, totalCount });
            }

            [HttpGet]
            [Route("GetClassById")]
            public async Task<JsonResult> GetClassById(int id)
            {
                ResponseObj obj = new ResponseObj();
                obj.data = await _classService.GetClassById(id);

                if (obj.data == null)
                {
                    return new JsonResult(new { status = ApiResponseStatus.Failure.ToInt(), msg = "Class not found." });
                }

                return new JsonResult(new
                {
                    status = ApiResponseStatus.Success.ToInt(),
                    msg = ResponseMessages.Success.GetMessage(),
                    data = obj.data
                });
            }


            [HttpPost]
            [Route("AddClass")]
            public async Task<IActionResult> AddClass([FromBody] ClassDto systemOutageDto)
            {
                var resultList = await _classService.AddClass(systemOutageDto);

                return new JsonResult(new { status = ApiResponseStatus.Success.ToInt(), msg = ResponseMessages.Success.GetMessage(), data = resultList });

            }

            [HttpPut]
            [Route("UpdateClass")]
            public async Task<IActionResult> UpdateClass([FromBody] ClassDto systemOutageDto)
            {
                var res = await _classService.UpdateClass(systemOutageDto);


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
            [Route("DeleteClass")]
            public async Task<IActionResult> DeleteClass(int id)
            {
                var res = await _classService.DeleteClass(id);


                if (res == "Class deleted successfully.")
                {
                    return new JsonResult(new { status = ApiResponseStatus.Success.ToInt(), msg = res });
                }
                if (res == "Class ID  does not exist.")
                {
                    return new JsonResult(new { status = ApiResponseStatus.Success.ToInt(), msg = res });
                }
                return new JsonResult(new { status = ApiResponseStatus.Success.ToInt(), msg = ResponseMessages.ErrorOccurred.GetMessage() });

            }
         
        }
    }

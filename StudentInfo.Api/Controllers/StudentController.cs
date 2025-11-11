using AutoMapper;
using StudentInfo.Utils.Dto;
using Microsoft.AspNetCore.Mvc;
using StudentInfo.Api.Controllers.Base;
using StudentInfo.BLL.Interfaces;
using StudentInfo.Utils.ExtensionMethods;
using static StudentInfo.Utils.Dto.ResponseMessageExtensions;

namespace StudentInfo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : BaseController
    {
        private readonly IStudentService _studentService;

        public StudentController(IConfiguration configuration, IMapper mapper, IStudentService studentService)
            : base(configuration, mapper)
        {
            _studentService = studentService;
        }

        [HttpPost]
        [Route("GetAllStudent")]
        public async Task<IActionResult> GetAllStudent([FromBody] PaginationDto paginationDto)
        {
            var (resultList, records, totalCount) = await _studentService.GetAllStudent(paginationDto);
            if (resultList == null)
            {
                return new JsonResult(new { status = ApiResponseStatus.Failure.ToInt(), msg = "Student not found." });
            }
            return new JsonResult(new { status = ApiResponseStatus.Success.ToInt(), msg = ResponseMessages.Success.GetMessage(), data = resultList, records = records, totalCount });
        }

        [HttpGet]
        [Route("GetStudentById")]
        public async Task<JsonResult> GetStudentById(int id)
        {
            ResponseObj obj = new ResponseObj();
            obj.data = await _studentService.GetStudentById(id);

            if (obj.data == null)
            {
                return new JsonResult(new { status = ApiResponseStatus.Failure.ToInt(), msg = "Student not found." });
            }

            return new JsonResult(new
            {
                status = ApiResponseStatus.Success.ToInt(),
                msg = ResponseMessages.Success.GetMessage(),
                data = obj.data
            });
        }


        [HttpPost]
        [Route("AddStudent")]
        public async Task<IActionResult> AddStudent([FromBody] StudentDto systemOutageDto)
        {
            var resultList = await _studentService.AddStudent(systemOutageDto);

            return new JsonResult(new { status = ApiResponseStatus.Success.ToInt(), msg = ResponseMessages.Success.GetMessage(), data = resultList });

        }

        [HttpPut]
        [Route("UpdateStudent")]
        public async Task<IActionResult> UpdateStudent([FromBody] StudentDto systemOutageDto)
        {
            var res = await _studentService.UpdateStudent(systemOutageDto);


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
        [Route("DeleteStudent")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var res = await _studentService.DeleteStudent(id);


            if (res == "Student deleted successfully.")
            {
                return new JsonResult(new { status = ApiResponseStatus.Success.ToInt(), msg = res });
            }
            if (res == "Student  ID  does not exist.")
            {
                return new JsonResult(new { status = ApiResponseStatus.Success.ToInt(), msg = res });
            }
            return new JsonResult(new { status = ApiResponseStatus.Success.ToInt(), msg = ResponseMessages.ErrorOccurred.GetMessage() });

        }
      
    }
}

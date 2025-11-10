using AutoMapper;
using ClassInfo.Api.Controllers.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClassInfo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassInfoController : BaseController
    {
        public ClassInfoController(IConfiguration configuration, IMapper mapper) : base(configuration, mapper)
        {
        }
    }
}

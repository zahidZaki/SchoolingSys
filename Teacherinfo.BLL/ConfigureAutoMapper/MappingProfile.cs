using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Teacherinfo.DLL.Models;
using Teacherinfo.Utils.Dto;

namespace Teacherinfo.BLL.ConfigureAutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TeacherTable, TeacherDto>().ReverseMap();
        }
    }
}

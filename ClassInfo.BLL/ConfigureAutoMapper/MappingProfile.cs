using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ClassInfo.DLL.Models;
using ClassInfo.Utils.Dto;

namespace ClassInfo.BLL.ConfigureAutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Class, ClassDto>().ReverseMap();
        }
    }
}

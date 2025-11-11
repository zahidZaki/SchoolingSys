using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ClassInfo.DLL.Interfaces.IBase;

namespace ClassInfo.BLL.Concrete.Base
{
    public class BaseService
    {
        protected readonly IUnitOfWork _unitofWork;
        protected readonly IMapper _mapper;
        public BaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitofWork = unitOfWork;
            this._mapper = mapper;
        }
    }
}

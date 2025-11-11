using AutoMapper;
using SchoolingSysApiGateway.Interfaces.IBase;

namespace SchoolingSysApiGateway.Concrete.Base
{
    public class BaseService
    {
        protected readonly IUnitOfWork _unitofWork;
        protected readonly IMapper _mapper;
        public BaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitofWork = unitOfWork;
            _mapper = mapper;
        }
    }
}

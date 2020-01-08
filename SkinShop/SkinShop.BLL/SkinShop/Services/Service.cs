using SkinShop.BLL.Identity.Interfaces;
using SkinShop.BLL.Identity.Services;
using SkinShop.BLL.SkinShop.Interfaces;
using SkinShop.BLL.SkinShop.Mappers;
using SkinShop.DL.Interfaces.SkinShop;
using SkinShop.DL.Repositories.SkinShop;

namespace SkinShop.BLL.SkinShop.Services
{
    public class Service
    {
        private IService _storeService;

        private IServiceCRUD _serviceForCRUD;

        private ICommonOperations _commonOperations;

        private IUnitOfWorK _unitOfWork;

        private MappersForDTO _MapperDTO;

        public Service()
        {
            _MapperDTO = new MappersForDTO();
            _unitOfWork = new UnitOfWork("DefaultConnection");
        }

        public IService StoreService
        {
            get
            {
                if (_storeService != null)
                    return _storeService;
                return new ApplicationService(_unitOfWork);
            }
        }

        public IServiceCRUD ServiceForCRUD
        {
            get
            {
                if (_serviceForCRUD != null)
                    return _serviceForCRUD;
                return new ApplicationServiceCRUD(_unitOfWork);
            }
        }

        public ICommonOperations CommonOperations
        {
            get
            {
                if (_commonOperations != null)
                    return _commonOperations;
                return new CommonOperations();
            }
        }
    }
}

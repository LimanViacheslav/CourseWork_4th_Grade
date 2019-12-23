using SkinShop.BLL.Identity.Interfaces;
using SkinShop.BLL.Identity.Services;
using SkinShop.BLL.SkinShop.Interfaces;
using SkinShop.BLL.SkinShop.Mappers;
using SkinShop.DAL.Identity.Repositories;
using SkinShop.DAL.SkinShop.Interfaces;
using SkinShop.DAL.SkinShop.Repositories;

namespace SkinShop.BLL.SkinShop.Services
{
    public class Service
    {
        private IService _storeService;

        private IUserService _userService;

        private IServiceCRUD _serviceForCRUD;

        private ICommonOperations _commonOperations;

        private IUnitOfWorkSkinShop _unitOfWork;

        private MappersForDTO _MapperDTO;

        public Service()
        {
            _MapperDTO = new MappersForDTO();
            _unitOfWork = new UnitOfWorkSkinShop("DefaultConnection");
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

        public IUserService UserService
        {
            get
            {
                if (_userService != null)
                    return _userService;
                return new UserService(new UnitOfWorkIdentity("DefaultConnection"));
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

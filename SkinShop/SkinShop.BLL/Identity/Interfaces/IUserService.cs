using SkinShop.BLL.Identity.IdentityDTO;
using SkinShop.BLL.Identity.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SkinShop.BLL.Identity.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<OperationDetails> Create(UserDTO userDto);
        Task<ClaimsIdentity> Authenticate(UserDTO userDto);
        Task SetInitialData(UserDTO adminDto, List<string> roles);
        Task<OperationDetails> CreateEmployee(UserDTO userDTO);
    }
}

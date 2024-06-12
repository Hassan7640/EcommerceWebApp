using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Persistence
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetUserById(string id);
        Task<ApplicationUser> GetUserByUsername(string username);

        Task<IEnumerable<ApplicationUser>> GetUsers();
        Task<ApplicationUser> UpdateUserDetails(string userId, ApplicationUser applicationUser);

    }
}

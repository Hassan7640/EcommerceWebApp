using Application.Contracts.Persistence;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly EcomDbContext _ecomDbContext;
        private readonly IMapper _mapper;

        public UserRepository(EcomDbContext ecomDbContext, IMapper mapper)
        {
            _ecomDbContext = ecomDbContext;
            _mapper = mapper;
        }

        public async Task<ApplicationUser> GetUserById(string id)
        {
            var user = await _ecomDbContext.ApplicationUsers.FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }

        public async Task<ApplicationUser> GetUserByUsername(string username)
        {
            var user = await _ecomDbContext.ApplicationUsers.FirstOrDefaultAsync(x => x.UserName.ToLower().Trim() == username);
            return user;
        }


        public async Task<IEnumerable<ApplicationUser>> GetUsers()
        {
            var users = await _ecomDbContext.ApplicationUsers.ToListAsync();
            return users;
        }

        public async Task<ApplicationUser> UpdateUserDetails(string userId, ApplicationUser applicationUser)
        {
            try
            {
                var user = await GetUserById(userId);
                if (user == null)
                {
                    throw new CustomException("User not found.");
                }
                var updateUser = _mapper.Map<ApplicationUser>(applicationUser);
                _ecomDbContext.ApplicationUsers.Update(updateUser);
                var updatedUser = await _ecomDbContext.SaveChangesAsync();
                if (updatedUser > 0)
                {
                    return applicationUser;
                }
                throw new CustomException("update user function failed");
            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}

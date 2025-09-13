using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;

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
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> getUserId(string username)
        {
            var query = await _context.Users.Where(x => x.UserName == username).FirstOrDefaultAsync();
            if (query != null)
                return query.Id;
            return "";

        }
      
    }
}

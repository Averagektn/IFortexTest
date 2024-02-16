using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Enums;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dbContext;

        public UserService(IServiceProvider serviceProvider)
        {
            _dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
        }

        public Task<User> GetUser()
        {
            var topOrderUser = _dbContext.Users
                .OrderByDescending(u => u.Orders.Count)
                .FirstOrDefaultAsync();

            return topOrderUser;
        }

        public Task<List<User>> GetUsers()
        {
            var inactiveUsers = _dbContext.Users.Where(u => u.Status == UserStatus.Inactive).ToListAsync();

            return inactiveUsers;
        }
    }
}

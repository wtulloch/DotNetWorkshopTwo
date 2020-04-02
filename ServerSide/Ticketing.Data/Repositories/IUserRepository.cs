using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;
using Ticketing.Models.DbModels;
using Ticketing.Models.Dtos;

namespace Ticketing.Data.Repositories
{
    public interface IUserRepository
    {
        Task<UserDto> GetUserByIdAsync(int id);
        Task<UserDto> RegisterAsync(RegisterUserDto newUser);
        Task<bool> ValidateUserAsync(string email, string password);
    }
}
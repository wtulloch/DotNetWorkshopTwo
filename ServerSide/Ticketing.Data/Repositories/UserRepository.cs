using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ticketing.Models.DbModels;
using Ticketing.Models.Dtos;

namespace Ticketing.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TicketingDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserRepository(TicketingDbContext context, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _context = context;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FirstAsync(u => u.Id == id);

            await _unitOfWork.CompleteAsync();

            var userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }
        public async Task<UserDto> RegisterAsync(RegisterUserDto newUser)
        {
            if (newUser == null)
            {
                throw new ArgumentNullException(nameof(newUser), "newUser cannot be null");
            }

            var user = _mapper.Map<User>(newUser);

            var isCurrentUser = await _context.Users.AnyAsync(u => u.Email == user.Email);

            if (isCurrentUser)
            {
                throw new UserExistsException();
            }

            await  _context.Users.AddAsync(user);
            await _unitOfWork.CompleteAsync();

            var userDto = _mapper.Map <UserDto>(user);
           
            return userDto;
        }

        public async Task<bool> ValidateUserAsync(string email, string password)
        {
            return await Task.FromResult(true);
        }
    }
}
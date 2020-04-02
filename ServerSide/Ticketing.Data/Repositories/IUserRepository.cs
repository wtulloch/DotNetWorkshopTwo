using Ticketing.Models.DbModels;

namespace Ticketing.Data.Repositories
{
    public interface IUserRepository
    {
        User Register(User newUser);
    }
}
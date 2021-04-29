using System.Threading.Tasks;
using BookProject.Models;

namespace BookProject.Repository
{
    public interface IAccountRepository
    {
        Task CreateUserAsync(SignUpUserModel signUpModel);
    }
}
using System.Threading.Tasks;
using BookProject.Models;

namespace BookProject.Service
{
    public interface IEmailService
    {
        Task SendTestEmail(UserEmailOptions userEmailOptions);
    }
}
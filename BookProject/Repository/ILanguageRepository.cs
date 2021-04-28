using System.Collections.Generic;
using System.Threading.Tasks;
using BookProject.Models;

namespace BookProject.Repository
{
    public interface ILanguageRepository
    {
        Task<List<LanguageModel>> GetLanguages();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookProject.Data;
using BookProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BookProject.Repository
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly BookStoreContext _context = null;

        public LanguageRepository(BookStoreContext context)
        {
            this._context = context;
        }

        public async Task<List<LanguageModel>> GetLanguages()
        {
            return await this._context.Language.Select(x => new LanguageModel()
            {
                Id = x.Id,
                Text = x.Text,
                Description = x.Description
            }).ToListAsync();
        }
    }
}

using System;
using System.Threading.Tasks;
using BookProject.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookProject.Components
{
    public class TopBooksViewComponent : ViewComponent
    {
        private readonly IBookRepository _bookRepository;
        public TopBooksViewComponent(IBookRepository bookRepository)
        {
            this._bookRepository = bookRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(int count)
        {
            var books = await this._bookRepository.GetTopBooksAsync(count);
            return View(books);
        }
    }
}

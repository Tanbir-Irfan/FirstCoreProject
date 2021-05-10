using Microsoft.AspNetCore.Mvc;
using BookProject.Repository;
using System.Collections.Generic;
using BookProject.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Authorization;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookProject.Controllers
{
    [Route("[Controller]/[action]")]
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository = null;
        private readonly ILanguageRepository _languageRepository = null;
        private readonly IWebHostEnvironment _webHostEnvironment = null;

        //private string title;// This is for viewdata attribute test

        public BookController(IBookRepository bookRepository,
            ILanguageRepository languageRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            this._bookRepository = bookRepository;
            this._languageRepository = languageRepository;
            this._webHostEnvironment = webHostEnvironment;
        }

        [Route("~/all-books")]
        public async Task<ViewResult> GetAllBooks()
        {
            List <BookModel> list = await this._bookRepository.GetAllBooks();
            return View(list);
        }

        //[Route("~/book-details/{id:int}", Name = "bookDetailsRoute")]
        [Route("/book-details/{id:int:min(1)}", Name = "bookDetailsRoute")]
        public async Task<ViewResult> GetBook(int id)
        {
            //ViewData["title"] = "Try This";
            BookModel book = await this._bookRepository.GetBookByID(id);
            return View(book);
        }

        public List<BookModel> SearchBook(string bookName, string authorName)
        {
            return this._bookRepository.SearchBook(bookName, authorName);
        }

        [Authorize]
        public ViewResult AddNewBook(bool isSuccess = false, int bookId = 0) {
            //to send by defualt value to the language property
            var book = new BookModel()
            {
                //Language = "1"
            };

            // ViewBag.Language = new List<string>()
            //{
            //    "Hindi",
            //    "English",
            //    "Spanish",
            //    "Danish"
            //}

            //ViewBag.Language = new SelectList(new List<string>()
            //{
            //    "Hindi",
            //    "English",
            //    "Spanish",
            //    "Danish"
            //}, "Spanish");

            //ViewBag.Language = new SelectList(GetLanguage(), "Id", "Text");

            //ViewBag.Language = GetLanguage().Select(x => new SelectListItem() {
            //    Text = x.Text,
            //    Value = x.Id.ToString()
            //}).ToList();

            //ViewBag.Language = new List<SelectListItem>()
            //{
            //    new SelectListItem(){Text="Hindi", Value = "1"},
            //    new SelectListItem(){Text="Bangla", Value = "2",},
            //    new SelectListItem(){Text="Urdu", Value = "3",},
            //    new SelectListItem(){Text="English", Value = "4"}
            //    new SelectListItem(){Text="Chinese", Value = "5",},
            //    new SelectListItem(){Text="Sudani", Value = "6"}
            //};

            //SelectListGroup group1 = new SelectListGroup() { Name = "Group 1" };
            //SelectListGroup group2 = new SelectListGroup() { Name = "Group 2", Disabled = true };
            //SelectListGroup group3 = new SelectListGroup() { Name = "Group 3" };

            //ViewBag.Language = new List<SelectListItem>()
            //{
            //    new SelectListItem(){Text="Hindi", Value = "1", Group = group1},
            //    new SelectListItem(){Text="Bangla", Value = "2", Group = group1},
            //    new SelectListItem(){Text="Urdu", Value = "3", Group = group2},
            //    new SelectListItem(){Text="English", Value = "4", Group =  group2},
            //    new SelectListItem(){Text="Chinese", Value = "5", Group = group3},
            //    new SelectListItem(){Text="Sudani", Value = "6", Group = group3}
            //};


            //ViewBag.Language = new SelectList(await _languageRepository.GetLanguages(), "Id", "Text"); // replace with dependency


            ViewBag.isSuccess = isSuccess;
            ViewBag.BookId = bookId;
            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModel bookModel)
        {
            //int id = this._bookRepository.AddNewBook(bookModel);
            if (ModelState.IsValid)
            {
                if(bookModel.CoverPhoto != null)
                {
                    string bookFolder = "book-img/cover/";
                    bookModel.CoverImageUrl = await UploadImage(bookFolder, bookModel.CoverPhoto);
                }

                if (bookModel.GalleryPhoto != null)
                {
                    string bookFolder = "book-img/gallery/";
                    bookModel.Gallery = new List<GalleryModel>();
                    foreach (var file in bookModel.GalleryPhoto)
                    {
                        var gallery = new GalleryModel()
                        {
                            Name = file.FileName,
                            URL = await UploadImage(bookFolder, file)
                        };
                        bookModel.Gallery.Add(gallery);
                    }
                }

                if (bookModel.BookPdf != null)
                {
                    string bookFolder = "book-pdf/";
                    bookModel.BookPdfUrl = await UploadImage(bookFolder, bookModel.BookPdf);
                }

                int id = await this._bookRepository.AddNewBook(bookModel);
                if (id > 0)
                {
                    //return RedirectToAction("AddNewBook");
                    return RedirectToAction(nameof(AddNewBook), new { isSuccess = true, bookId = id });
                }
            }

            //ViewBag.Language = new SelectList(new List<string>()
            //{
            //    "Hindi",
            //    "English",
            //    "Spanish",
            //    "Danish"
            //}, "Spanish");

            //ViewBag.Language = new SelectList(GetLanguage(), "Id", "Text");

            //ViewBag.Language = new List<SelectListItem>()
            //{
            //    new SelectListItem(){Text="Hindi", Value = "1"},
            //    new SelectListItem(){Text="Bangla", Value = "2"},
            //    new SelectListItem(){Text="Urdu", Value = "3", Disabled = true},
            //    new SelectListItem(){Text="English", Value = "4"}
            //};

            //SelectListGroup group1 = new SelectListGroup() { Name = "Group 1" };
            //SelectListGroup group2 = new SelectListGroup() { Name = "Group 2", Disabled = true };
            //SelectListGroup group3 = new SelectListGroup() { Name = "Group 3" };

            //ViewBag.Language = new List<SelectListItem>()
            //{
            //    new SelectListItem(){Text="Hindi", Value = "1", Group = group1},
            //    new SelectListItem(){Text="Bangla", Value = "2", Group = group1},
            //    new SelectListItem(){Text="Urdu", Value = "3", Group = group2},
            //    new SelectListItem(){Text="English", Value = "4", Group =  group2},
            //    new SelectListItem(){Text="Chinese", Value = "5", Group = group3},
            //    new SelectListItem(){Text="Sudani", Value = "6", Group = group3}
            //};

            //ViewBag.Language = new SelectList(await _languageRepository.GetLanguages(), "Id", "Text");

            ModelState.AddModelError("", "This is the error message from Model");
            //ViewBag.isSuccess = false;
            //ViewBag.BookId = 0;
            return View();
        }

        private async Task<string> UploadImage(string folderPath, IFormFile file)
        {
            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;
            string serverFolder = Path.Combine(this._webHostEnvironment.WebRootPath, folderPath);
            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            return "/" + folderPath;
        }

        //private List<LanguageModel> GetLanguage()
        //{
        //    return new List<LanguageModel>()
        //    {
        //        new LanguageModel(){Id = 1, Text = "Hindi"},
        //        new LanguageModel(){Id = 2, Text = "English"},
        //        new LanguageModel(){Id = 3, Text = "Spanish"},
        //        new LanguageModel(){Id = 4, Text = "Danish"}
        //    };
        //}
    }
}

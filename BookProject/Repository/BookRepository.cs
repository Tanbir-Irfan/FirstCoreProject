using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookProject.Data;
using BookProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BookProject.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context = null;

        public IConfiguration Configuration { get; }

        public BookRepository(BookStoreContext context, IConfiguration configuration)
        {
            this._context = context;
            this.Configuration = configuration;
        }

        public async Task<List<BookModel>> GetAllBooks()
        {
            //var books = new List<BookModel>();
            //var allBooks = await this._context.Books.ToListAsync();
            //if(allBooks?.Any() == true)
            //{
            //    foreach (var book in allBooks)
            //    {
            //        books.Add(new BookModel()
            //        {
            //            Author = book.Author,
            //            Category = book.Category,
            //            Description = book.Description,
            //            Id = book.Id,
            //            LanguageId = book.LanguageId,
            //            Language = book.Language.Text,
            //            Title = book.Title,
            //            TotalPage = book.TotalPage
            //        });
            //    }
            //}

            return (await this._context.Books.Select(book => new BookModel()
            {
                Author = book.Author,
                Category = book.Category,
                Description = book.Description,
                Id = book.Id,
                Language = book.Language.Text,
                LanguageId = book.LanguageId,
                Title = book.Title,
                TotalPage = book.TotalPage,
                CoverImageUrl = book.CoverImageUrl,
                BookPdfUrl = book.BookPdfUrl
            }).ToListAsync());

            //return books;
        }

        public async Task<List<BookModel>> GetTopBooksAsync(int count)
        {
            return (await this._context.Books.Select(book => new BookModel()
            {
                Author = book.Author,
                Category = book.Category,
                Description = book.Description,
                Id = book.Id,
                Language = book.Language.Text,
                LanguageId = book.LanguageId,
                Title = book.Title,
                TotalPage = book.TotalPage,
                CoverImageUrl = book.CoverImageUrl,
                BookPdfUrl = book.BookPdfUrl
            }).Take(count).ToListAsync());
        }

        public async Task<int> AddNewBook(BookModel bookModel)
        {
            var newBook = new Books()
            {
                Author = bookModel.Author,
                CreatedOn = DateTime.UtcNow,
                Description = bookModel.Description,
                Title = bookModel.Title,
                LanguageId = bookModel.LanguageId,
                TotalPage = bookModel.TotalPage.HasValue ? bookModel.TotalPage.Value : 0,
                UpdatedOn = DateTime.UtcNow,
                CoverImageUrl = bookModel.CoverImageUrl,
                BookPdfUrl = bookModel.BookPdfUrl
            };

            newBook.BookGallery = bookModel.Gallery.Select(file => new BookGallery()
            {
                Name = file.Name,
                URL = file.URL
            }).ToList();


            //newBook.BookGallery = new List<BookGallery>();
            //foreach (var file in bookModel.Gallery)
            //{
            //    newBook.BookGallery.Add(new BookGallery() {
            //        Name = file.Name,
            //        URL = file.URL
            //    });
            //}

            //this._context.Books.Add(newBook);
            //this._context.SaveChanges();

            await this._context.Books.AddAsync(newBook);
            await this._context.SaveChangesAsync();

            return newBook.Id;
        }


        public async Task<BookModel> GetBookByID(int id)
        {
            //BookModel bookDetails = null;
            //var allBooks = await this._context.Books.ToListAsync();
            //if (allBooks?.Any() == true)
            //{
            //    foreach (var book in allBooks)
            //    {
            //        if(book.Id == id)
            //        {
            //            bookDetails = new BookModel()
            //            {
            //                Author = book.Author,
            //                Category = book.Category,
            //                Description = book.Description,
            //                Id = book.Id,
            //                Language = book.Language,
            //                Title = book.Title,
            //                TotalPage = book.TotalPage
            //            };
            //        }
            //    }
            //}

            //var book = await this._context.Books.FindAsync(id);
            //if (book != null)
            //{
            //    var bookDetails = new BookModel() {
            //        Author = book.Author,
            //        Category = book.Category,
            //        Description = book.Description,
            //        Id = book.Id,
            //        Language = book.Language.Text,
            //        LanguageId = book.LanguageId,
            //        Title = book.Title,
            //        TotalPage = book.TotalPage
            //    };

            //    return bookDetails;
            //}

            return (await this._context.Books.Where(x => x.Id == id).Select(book => new BookModel()
            {
                Author = book.Author,
                Category = book.Category,
                Description = book.Description,
                Id = book.Id,
                Language = book.Language.Text,
                LanguageId = book.LanguageId,
                Title = book.Title,
                TotalPage = book.TotalPage,
                CoverImageUrl = book.CoverImageUrl,
                BookPdfUrl = book.BookPdfUrl,
                Gallery = book.BookGallery.Select(gallery => new GalleryModel()
                {
                    Name = gallery.Name,
                    URL = gallery.URL,
                    Id = gallery.Id
                }).ToList()
            }).FirstOrDefaultAsync());

            //return null;
        }

        public List<BookModel> SearchBook(string title, string authorName)
        {
            //return DataSource().Where(x => x.Title.Contains(title) ||x.Author.Contains(authorName)).ToList();
            return null;
        }

        //private List<BookModel> DataSource()
        //{
        //    return new List<BookModel>()
        //    {
        //        new BookModel(){Id = 1, Title = "MVC CORE", Author = "Marina Tomas", Description = "This is all about MVC", Language = "English", Category = "Program", TotalPage = 1100},
        //        new BookModel(){Id = 2, Title = "Python Django", Author = "Jasinda Polo", Description = "This is all about Django", Language = "Frech", Category = "Action", TotalPage = 750 },
        //        new BookModel(){Id = 3, Title = "PHP", Author = "Penas Bek", Description = "This is all about PHP", Language = "Rusian", Category = "Technical", TotalPage = 1700},
        //        new BookModel(){Id = 4, Title = "Java", Author = "Trosa Marur", Description = "This is all about Java", Language = "Chinese", Category = "Action", TotalPage = 2000 }
        //        //new BookModel(){Id = 5, Title = "C/C++", Author = "Titoc Manki", Description = "This is all about C/C++", Language = "Bengali", Category = "Developer", TotalPage = 1500 }
        //    };
        //}
        public string BookLogoName()
        {
            //return "Tesla Book";
            return Configuration["AppName"];
        }
    }
}

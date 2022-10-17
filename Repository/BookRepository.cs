using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class BookRepository
    {
        private readonly BookStoreDB _context = null;

        public BookRepository(BookStoreDB context)
        {
            _context = context;
        }

        public async Task<int> AddNewBook(BookModel model)
        {
            var newBook = new Books()
            {
                Title = model.Title,
                Author = model.Author,
                Description = model.Description,
                LanguageId = model.LanguageId,
                TotalPages = model.TotalPages.HasValue ? model.TotalPages.Value : 0,
                CoverImageUrl = model.CoverImageUrl,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };
            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();
            return newBook.ID;
        }

        public async Task<List<BookModel>> GetAllBooks()
        {
            var books = new List<BookModel>();
            var allbooks = await _context.Books.ToListAsync();
            if (allbooks?.Any() == true)
            {
                foreach (var book in allbooks)
                {
                    books.Add(new BookModel()
                    {
                        ID = book.ID,
                        Title = book.Title,
                        Author = book.Author,
                        LanguageId = book.LanguageId,
                        Description = book.Description,
                        CoverImageUrl = book.CoverImageUrl,
                        TotalPages = book.TotalPages,
                        CreatedOn = DateTime.UtcNow,
                        UpdatedOn = DateTime.UtcNow
                    });
                }
            }

            //var allbooks = await (from x in _context.Books
            //                      join y in _context.Language
            //                      on x.LanguageId equals y.Id select new {
            //                        x.ID,
            //                        x.Title,
            //                        x.Author,
            //                        y.Name,
            //                        x.Description,
            //                        x.TotalPages,
            //                        x.CreatedOn,
            //                        x.UpdatedOn
            //                      }).ToListAsync(); ;
            //if (allbooks?.Any() == true)
            //{
            //    foreach (var book in allbooks)
            //    {
            //        books.Add(new BookModel()
            //        {
            //            ID = book.ID,
            //            Title = book.Title,
            //            Author = book.Author,
            //            Language = book.Name,
            //            Description = book.Description,
            //            TotalPages = book.TotalPages,
            //            CreatedOn = DateTime.UtcNow,
            //            UpdatedOn = DateTime.UtcNow
            //        });
            //    }
            //}

            return books;
        }

        public async Task<BookModel> GetBookById(int id)
        {
            return await _context.Books.Where(x => x.ID == id)
                .Select(book => new BookModel()
            {
                Title = book.Title,
                Author = book.Author,
                LanguageId = book.LanguageId,
                Language = book.Language.Name,
                Description = book.Description,
                CoverImageUrl = book.CoverImageUrl,
                TotalPages = book.TotalPages,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            }).FirstOrDefaultAsync();

            // When we search something by other columns or logic use this way
            //_context.Books.Where(x => x.ID == id).FirstOrDefaultAsync();

            // When we search something by primary key we can use the find method
            //var book = await _context.Books.FindAsync(id);
            //if (book != null)
            //{
            //    var bookDetails = new BookModel()
            //    {
                        //Title = book.Title,
                        //Author = book.Author,
                        //LanguageId = book.LanguageId,
                        //Language = book.Language.Name,
                        //Description = book.Description,
                        //TotalPages = book.TotalPages,
                        //CreatedOn = DateTime.UtcNow,
                        //UpdatedOn = DateTime.UtcNow
            //    };
            //    return bookDetails;
            //}
            //return null;
        }

        public List<BookModel> SearchBook(string title, string authorName)
        {
            return null;
            //return DataSource().Where(x => x.Title.Contains(title) || x.Author.Contains(authorName)).ToList();
        }
    }
}

using BookStore.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository _bookRepository = null;
        private readonly LanguageRepository _languageRepository = null;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BookController(BookRepository bookRepository, 
            LanguageRepository languageRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = bookRepository;
            _languageRepository = languageRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> AddNewBook(bool isSuccess = false, int bookId = 0)
        {
            //Initialize a value for dropdown
            //var model = new BookModel() { Language = "Bangla" };

            /// DropDown : There have some ways to use dropdown in application
            // Direct way to define dropdown value using selectList
            //ViewBag.language = new SelectList(new List<string>() { "English", "Bangla", "Arabic" });

            //ViewBag.language = new SelectList(GetLanguage(), "Id", "Text");

            //ViewBag.language = GetLanguage().Select(x => new SelectListItem()
            //{
            //    Text = x.Text,
            //    Value = x.Id.ToString()
            //}).ToList();

            //ViewBag.language = new List<SelectListItem>() 
            //{
            //    new SelectListItem(){ Text = "English", Value = "1", Disabled = true },
            //    new SelectListItem(){ Text = "Bangla", Value = "2", Selected = true },
            //    new SelectListItem(){ Text = "Arabic", Value = "3"},
            //};

            //var group1 = new SelectListGroup() { Name = "Group 1" };
            //var group2 = new SelectListGroup() { Name = "Group 2", Disabled = true };
            //var group3 = new SelectListGroup() { Name = "Group 3" };
            //ViewBag.language = new List<SelectListItem>()
            //{
            //    new SelectListItem(){ Text = "English", Value = "1", Group = group1 },
            //    new SelectListItem(){ Text = "Bangla", Value = "2", Group = group1 },
            //    new SelectListItem(){ Text = "Arabic", Value = "3", Group = group2 },
            //    new SelectListItem(){ Text = "Urdhu", Value = "4", Group = group2 },
            //    new SelectListItem(){ Text = "Tamil", Value = "5", Group = group3 },
            //    new SelectListItem(){ Text = "Hindi", Value = "6", Group = group3 }
            //};

            //ViewBag.language = new List<SelectListItem>()
            //{
            //    new SelectListItem(){ Text = "English", Value = "1" },
            //    new SelectListItem(){ Text = "Bangla", Value = "2" },
            //    new SelectListItem(){ Text = "Arabic", Value = "3" },
            //    new SelectListItem(){ Text = "Urdhu", Value = "4" }
            //};

            ViewBag.language = new SelectList(await _languageRepository.GetLanguages(), "Id", "Name");

            ViewBag.isSuccess = isSuccess;
            ViewBag.BookId = bookId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModel bookModel)
        {
            if (ModelState.IsValid)
            {
                if(bookModel.CoverPhoto != null)
                {
                    string folder = "books/cover/";
                    folder += Guid.NewGuid().ToString() + bookModel.CoverPhoto.FileName;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                    bookModel.CoverImageUrl = "/" + folder;

                    await bookModel.CoverPhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                }

                int id = await _bookRepository.AddNewBook(bookModel);
                if (id > 0)
                {
                    return RedirectToAction(nameof(AddNewBook), new { isSuccess = true, bookId = id });
                }
            }
            ViewBag.language = new SelectList(await _languageRepository.GetLanguages(), "Id", "Name");

            //ViewBag.language = new List<SelectListItem>()
            //{
            //    new SelectListItem(){ Text = "English", Value = "1" },
            //    new SelectListItem(){ Text = "Bangla", Value = "2" },
            //    new SelectListItem(){ Text = "Arabic", Value = "3" },
            //    new SelectListItem(){ Text = "Urdhu", Value = "4" }
            //};
            //ViewBag.language = new SelectList(GetLanguage(), "Id", "Text");
            //ViewBag.language = new List<string>() { "English", "Bangla", "Arabic" };
            ModelState.AddModelError("", "You have to fill-up all the field as required");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var data = await _bookRepository.GetAllBooks();
            return View(data);
        }

        public async Task<IActionResult> GetBook(int id)
        {
            var data = await _bookRepository.GetBookById(id);
            return View(data);
        }

        public List<BookModel> SearchBooks(string bookName, string authorName)
        {
            return _bookRepository.SearchBook(bookName, authorName);
        }

        //private List<LanguageModel> GetLanguage()
        //{
        //    return new List<LanguageModel>()
        //    {
        //        new LanguageModel(){Id=1,Text="English"},
        //        new LanguageModel(){Id=2,Text="Bangla"},
        //        new LanguageModel(){Id=3,Text="Arabic"},
        //    };
        //}
    }
}
 
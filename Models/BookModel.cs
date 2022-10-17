using BookStore.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class BookModel
    {
        public int ID { get; set; }
        [StringLength(100, MinimumLength = 5)]

        [Required(ErrorMessage = "Please give title for the book")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please give an author name for the book")]
        public string Author { get; set; }

        [StringLength(500, MinimumLength = 10, ErrorMessage = "Description must be in between (10 - 500) character")]
        public string Description { get; set; }
        public string Catagory { get; set; }

        [Display(Name = "Language")]
        [Required(ErrorMessage = "Must have to select a language")]
        public int LanguageId { get; set; }
        public string Language { get; set; }
        public List<string> MultiLanguage { get; set; }

        public LanguageEnum LanguageEnum { get; set; }

        [Display(Name = "Total pages of the book")]
        [Required(ErrorMessage = "Total pages of the book must be defined")]
        public int? TotalPages { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

        [Display(Name = "Choose the cover photo of your book")]
        [Required]
        public IFormFile CoverPhoto { get; set; }
        public string CoverImageUrl { get; set; }
    }
}

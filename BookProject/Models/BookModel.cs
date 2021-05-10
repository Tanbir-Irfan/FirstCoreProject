using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BookProject.Enums;
using BookProject.Helper;
using Microsoft.AspNetCore.Http;

namespace BookProject.Models
{
    public class BookModel
    {
        //[DataType(DataType.EmailAddress)]
        //[Display(Name = "Email")]
        //[EmailAddress(ErrorMessage = "Please enter the valid email address")]
        //public string MyField { get; set; }

        public int Id { get; set; }

        //[StringLength(maximumLength: 30, MinimumLength = 5)]
        //[Required(ErrorMessage = "Please enter the title of your book")]
        //public string Title { get; set; }

        [MyCustomValidation("Azure", ErrorMessage = "Please enter the title of your book with MVC")]
        //[MyCustomValidation(ErrorMessage = "Please enter the title of your book with MVC", Text = "Azure")]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [StringLength(maximumLength: 200, MinimumLength = 30, ErrorMessage = "Please enter correctly")]
        public string Description { get; set; }

        public string Category { get; set; }

        [Required(ErrorMessage = "Please enter the page number manually")]
        [Display(Name = "Total Page")]
        public int? TotalPage { get; set; }

        //[Required(ErrorMessage = "Please choose the languages of your book")]
        //[Display(Name = "Multi-Language")]
        //public List<string> MultiLanguage { get; set; }

        [Required(ErrorMessage = "Please choose the languages of your book")]
        [Display(Name = "Enum-Language")]
        public LanguageEnum LanguageEnum { get; set; }

        //[Required(ErrorMessage = "Please choose the language of your book")]
        //public string Language { get; set; }

        [Required(ErrorMessage = "Please choose the language of your book")]
        public int LanguageId { get; set; }

        public string Language { get; set; }

        [Display(Name = "Choose a cover photo of your book")]
        [Required(ErrorMessage = "Please choose the photo of your book")]
        public IFormFile CoverPhoto { get; set; }

        public string CoverImageUrl { get; set; }

        [Display(Name = "Choose gallery photos of your book")]
        [Required(ErrorMessage = "Please choose the photo of your book")]

        public IFormFileCollection GalleryPhoto { get; set; }

        public List<GalleryModel> Gallery { get; set; }

        [Display(Name = "Upload Your Book In PDF format")]
        [Required(ErrorMessage = "Please choose the pdf of your book")]
        public IFormFile BookPdf { get; set; }

        public string BookPdfUrl { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace BookProject.Enums
{
    public enum LanguageEnum
    {
        [Display(Name = "Hindi Language")]
        Hindi = 10,
        [Display(Name = "English Language")]
        English = 11,
        Urdu = 12,
        Bengali = 13,
        Sudani = 14,
        Arabic = 15
    }
}

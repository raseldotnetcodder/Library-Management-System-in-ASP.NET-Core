using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Enums
{
    public enum LanguageEnum
    {
        [Display(Name = "Global Language")]
        English,
        [Display(Name = "Mother Language")]
        Bangla,
        Urdhu,
        Hindi,
        Arabic,
        Tamil
    }
}

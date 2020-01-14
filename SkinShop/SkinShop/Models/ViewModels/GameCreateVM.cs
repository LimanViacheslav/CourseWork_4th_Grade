using SkinShop.Models.SkinShop;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SkinShop.Models.ViewModels
{
    public class GameCreateVM
    {
        [ScaffoldColumn(true)]
        public int Id { get; set; }
        
        [Display(Name = "Название")]
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Недопустимая длина")]
        public string Name { get; set; }

        [Display(Name = "Жанр")]
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Недопустимая длина")]
        public string Genre { get; set; }

        [Display(Name = "Тип")]
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Недопустимая длина")]
        public string Type { get; set; }

        [Display(Name = "Ссылка на игру")]
        [Required]
        [Url]
        public string GameURL { get; set; }

        [Display(Name = "Это игра в которой может быть несколько одинаковых предметов?")]
        public bool IsThingGame { get; set; }

        [Display(Name = "Изображение")]
        public HttpPostedFileBase Image { get; set; }

        public ImageDM ImageInDatebase { get; set; }

        [Display(Name = "Подпись к изображению")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Недопустимая длина")]
        public string Alt { get; set; }
    }
}
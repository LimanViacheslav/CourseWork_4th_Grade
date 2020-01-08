using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace SkinShop.Models.ViewModels
{
    public class SkinCreateVM
    {
        [ScaffoldColumn(true)]
        public int Id { get; set; }

        [Display(Name = "Название")]
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Недопустимая длина")]
        public string Name { get; set; }

        [Display(Name = "Тип скина")]
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Недопустимая длина")]
        public string SkinType { get; set; }

        [Display(Name = "Редкость скина")]
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Недопустимая длина")]
        public string SkinRarity { get; set; }

        [Display(Name = "Цвет редкости")]
        [Required]
        [StringLength(7, MinimumLength = 4, ErrorMessage = "Недопустимая длина")]
        public string SkinRarityColor { get; set; }

        [Display(Name = "Игра")]
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Недопустимая длина")]
        public virtual string Game { get; set; }

        [Display(Name = "Цена")]
        [Required]
        [Range(0.01, 10000000, ErrorMessage = "Недопустимая длина")]
        public double Price { get; set; }

        [Display(Name = "Скидка(%)")]
        [Required]
        [Range(0, 99, ErrorMessage = "Недопустимая длина")]
        public int Sale { get; set; }

        [Display(Name = "Описание скина")]
        [StringLength(300, MinimumLength = 0, ErrorMessage = "Недопустимая длина")]
        public string Description { get; set; }

        [Display(Name = "Изображение")]
        public HttpPostedFileBase Image { get; set; }

        [Display(Name = "Подпись к изображению")]
        [StringLength(60, MinimumLength = 0, ErrorMessage = "Недопустимая длина")]
        public string Alt { get; set; }
    }
}
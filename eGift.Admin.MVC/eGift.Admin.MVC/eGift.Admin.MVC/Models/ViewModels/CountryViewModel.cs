﻿using System.ComponentModel.DataAnnotations;

namespace eGift.Admin.MVC.Models.ViewModels
{
    public class CountryViewModel : BaseModel
    {
        #region Constructors

        public CountryViewModel()
        {
        }

        #endregion Constructors

        #region Data Models

        public int ID { get; set; }

        [Display(Name = "CountryCode")]
        [Required]
        public string CountryCode { get; set; }

        [Display(Name = "Country")]
        [Required]
        public string CountryName { get; set; }

        [Display(Name = "Description")]
        public string? Description { get; set; }

        #endregion Data Models
    }
}
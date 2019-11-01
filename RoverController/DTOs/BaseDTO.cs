using System;
using System.ComponentModel.DataAnnotations;

namespace RoverController.Web.DTOs
{
    public abstract class BaseDTO
    {
        [Display(Name = "Id", ShortName = "#")]
        public int Id { get; set; }

        #region Date-User-Notes-Status

        //[Required]
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}", NullDisplayText = "", ApplyFormatInEditMode = true)]
        [Display(Name = "Entered On")]
        public DateTime? CreatedDate { get; set; }

        public string CreatedDateFormatted { get; set; }

        [MaxLength(128)]
        public string CreatedByUserId { get; set; }

        [Display(Name = "Entered By")]
        public string CreatedByName { get; set; }

        #endregion Date-User-Notes-Status

        public BaseDTO()
        {
            CreatedDate = DateTime.Now;
        }
    }
}
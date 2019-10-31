using RoverController.Web.API.DataTable;
using System;
using System.ComponentModel.DataAnnotations;

namespace RoverController.Web.DTOs
{
    public abstract class BaseDTO : SearchDetail
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
        [Display(Name = "Entered By")]
        public string CreatedByUserId { get; set; }

        public string CreatedByName { get; set; }

        #endregion Date-User-Notes-Status

        public BaseDTO()
        {
            CreatedDate = DateTime.Now;
        }
    }
}
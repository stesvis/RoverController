using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoverController.Web.Models
{
    public abstract class BaseModel
    {
        [Key]
        public int Id { get; set; }

        #region Date-User-Status

        [Required]
        public DateTime? CreatedDate { get; set; }

        [Required]
        [MaxLength(128)]
        public string CreatedByUserId { get; set; }

        [JsonIgnore]
        [ForeignKey("CreatedByUserId")]
        public virtual ApplicationUser CreatedByUser { get; set; }

        #endregion Date-User-Status
    }
}
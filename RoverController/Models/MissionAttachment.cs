using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoverController.Web.Models
{
    public class MissionAttachment : BaseModel
    {
        /// <summary>
        /// The parent mission
        /// </summary>
        [Required]
        public int MissionId { get; set; }

        [JsonIgnore]
        [ForeignKey("MissionId")]
        public virtual Mission Mission { get; set; }

        [Required]
        public string OriginalFilename { get; set; }

        [Required]
        public string FileType { get; set; }

        public int FileSize { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public string AWSPublicUrl { get; set; }
    }
}
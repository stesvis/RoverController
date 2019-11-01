using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace RoverController.Web.DTOs
{
    public class MissionAttachmentDTO : BaseDTO
    {
        /// <summary>
        /// The parent mission
        /// </summary>
        [Required]
        public int MissionId { get; set; }

        [JsonIgnore]
        public MissionDTO Mission { get; set; }

        public string OriginalFilename { get; set; }

        public string FileType { get; set; }

        public int FileSize { get; set; }

        public string FileName { get; set; }

        public string AWSPublicUrl { get; set; }
    }
}
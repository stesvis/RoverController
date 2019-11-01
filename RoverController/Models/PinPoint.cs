using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoverController.Web.Models
{
    public class PinPoint : BaseModel
    {
        /// <summary>
        /// The parent mission
        /// </summary>
        [Required]
        public int MissionId { get; set; }

        [JsonIgnore]
        [ForeignKey("MissionId")]
        public virtual Mission Mission { get; set; }

        /// <summary>
        /// The X postion
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// The Y position
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// The facing direction
        /// </summary>
        public string Direction { get; set; }
    }
}
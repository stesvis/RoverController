using System.ComponentModel.DataAnnotations;

namespace RoverController.Web.DTOs
{
    public class PinPointDTO : BaseDTO
    {
        /// <summary>
        /// The parent mission
        /// </summary>
        [Required]
        public int MissionId { get; set; }

        /// <summary>
        /// The X postion
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// The Y position
        /// </summary>
        public int Y { get; set; }

        public string Direction { get; set; }
    }
}
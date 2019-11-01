using Newtonsoft.Json;
using RoverController.Web.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RoverController.Models
{
    public class Mission : BaseModel
    {
        [Required]
        public int MaxX { get; set; }

        [Required]
        public int MaxY { get; set; }

        /// <example>1</example>
        [Required]
        public int InitialX { get; set; }

        /// <example>2</example>
        [Required]
        public int InitialY { get; set; }

        /// <example>N</example>
        [Required]
        public string InitialDirection { get; set; }

        /// <example>LMLMLMLMM</example>
        [Required]
        public string Instructions { get; set; }

        /// <example>1</example>
        [Required]
        public int FinalX { get; set; }

        /// <example>2</example>
        [Required]
        public int FinalY { get; set; }

        /// <example>N</example>
        [Required]
        public string FinalDirection { get; set; }

        [JsonIgnore]
        public virtual ICollection<PinPoint> PinPoints { get; set; }
    }
}
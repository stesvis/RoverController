using Newtonsoft.Json;
using RoverController.Web.Models;
using System.Collections.Generic;

namespace RoverController.Models
{
    public class Mission : BaseModel
    {
        public int MaxX { get; set; }

        public int MaxY { get; set; }

        /// <example>1</example>
        public int InitialX { get; set; }

        /// <example>2</example>
        public int InitialY { get; set; }

        /// <example>N</example>
        public string InitialDirection { get; set; }

        /// <example>LMLMLMLMM</example>
        public string Instructions { get; set; }

        /// <example>1</example>
        public int FinalX { get; set; }

        /// <example>2</example>
        public int FinalY { get; set; }

        /// <example>N</example>
        public string FinalDirection { get; set; }

        [JsonIgnore]
        public virtual ICollection<PinPoint> PinPoints { get; set; }
    }
}
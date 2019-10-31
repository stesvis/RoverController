using RoverController.Web.Models;

namespace RoverController.Models
{
    public class Position : BaseModel
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
    }
}
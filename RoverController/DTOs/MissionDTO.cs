using RoverController.Web.DTOs;

namespace RoverController.DTOs
{
    public class MissionDTO : BaseDTO
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

        /// <example>3</example>
        public int FinalY { get; set; }

        /// <example>N</example>
        public string FinalDirection { get; set; }

        #region Custom Properties

        /// <example>1 2 N</example>
        public string Input
        {
            get { return $"{InitialX} {InitialY} {InitialDirection}"; }
        }

        /// <example>1 3 N</example>
        public string Output
        {
            get { return $"{FinalX} {FinalY} {FinalDirection}"; }
        }

        #endregion Custom Properties
    }
}
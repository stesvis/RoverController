using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RoverController.Web.DTOs
{
    public class MissionDTO : BaseDTO
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

        //[JsonIgnore]
        public virtual ICollection<PinPointDTO> PinPoints { get; set; }

        public virtual ICollection<MissionAttachmentDTO> Attachments { get; set; }

        #endregion Custom Properties

        public MissionDTO()
        {
            PinPoints = new HashSet<PinPointDTO>();
            Attachments = new HashSet<MissionAttachmentDTO>();
        }
    }
}
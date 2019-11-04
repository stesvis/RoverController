using Newtonsoft.Json;
using System;

namespace RoverController.Mobile.DTOs
{
    public class MissionAttachmentDTO
    {
        [JsonProperty("missionId")]
        public int MissionId { get; set; }

        [JsonProperty("originalFilename")]
        public string OriginalFilename { get; set; }

        [JsonProperty("fileType")]
        public string FileType { get; set; }

        [JsonProperty("fileSize")]
        public int FileSize { get; set; }

        [JsonProperty("fileName")]
        public string FileName { get; set; }

        [JsonProperty("awsPublicUrl")]
        public string AwsPublicUrl { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty("createdDateFormatted")]
        public string CreatedDateFormatted { get; set; }

        [JsonProperty("createdByUserId")]
        public string CreatedByUserId { get; set; }

        [JsonProperty("createdByName")]
        public string CreatedByName { get; set; }
    }
}
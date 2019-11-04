using Newtonsoft.Json;

namespace RoverController.Mobile.DTOs
{
    public class MissionAttachmentDTO : BaseDTO
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
    }
}
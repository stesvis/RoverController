using Newtonsoft.Json;
using Prism.Mvvm;
using System;

namespace RoverController.Mobile.DTOs
{
    public class BaseDTO : BindableBase
    {
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
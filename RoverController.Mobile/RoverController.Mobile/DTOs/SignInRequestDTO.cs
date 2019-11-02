using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoverController.Mobile.DTOs
{
    public class SignInRequestDTO
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("grant_type")]
        public string GrantType { get; set; }
    }
}
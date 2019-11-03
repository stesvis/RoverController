using Newtonsoft.Json;
using Prism.Mvvm;

namespace RoverController.Mobile.DTOs
{
    public class MissionRequestDTO : BindableBase
    {
        [JsonProperty("maxX")]
        public int MaxX { get; set; }

        [JsonProperty("maxY")]
        public int MaxY { get; set; }

        private int? _initialX;
        [JsonProperty("initialX")]
        public int? InitialX
        {
            get { return _initialX; }
            set { SetProperty(ref _initialX, value); }
        }

        private int? _initialY;
        [JsonProperty("initialY")]
        public int? InitialY
        {
            get { return _initialY; }
            set { SetProperty(ref _initialY, value); }
        }

        private string _initialDirection;
        [JsonProperty("initialDirection")]
        public string InitialDirection
        {
            get { return _initialDirection; }
            set { SetProperty(ref _initialDirection, value); }
        }

        private string _instructions;
        [JsonProperty("instructions")]
        public string Instructions
        {
            get { return _instructions; }
            set { SetProperty(ref _instructions, value); }
        }
    }
}
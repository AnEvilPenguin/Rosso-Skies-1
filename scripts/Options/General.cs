using Godot;
using Newtonsoft.Json.Linq;

namespace RossoSkies1.scripts.Options
{
    internal class General : Options
    {
        private bool _fullScreen = true;

        public General()
        {
            Name = "General";
            _defaultSettings = JObject.FromObject(this);
        }

        public bool FullScreen
        {
            get => _fullScreen;
            set
            {
                _fullScreen = value;

                var mode = _fullScreen ?
                    DisplayServer.WindowMode.Fullscreen :
                    DisplayServer.WindowMode.Windowed;

                DisplayServer.WindowSetMode(mode);
            }
        }
    }
}

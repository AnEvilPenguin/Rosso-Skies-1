using Godot;
using Newtonsoft.Json.Linq;

namespace RossoSkies1.scripts.Settings
{
    internal class General : Options
    {
        private static General _default = new General();

        private bool _fullScreen = true;

        public General()
        {
            Name = "General";
            _defaultSettings = _default;
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

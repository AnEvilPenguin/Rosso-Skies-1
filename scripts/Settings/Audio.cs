using Newtonsoft.Json.Linq;

namespace RossoSkies1.scripts.Settings
{
    internal class Audio : Options
    {
        private static Audio _default = new Audio();

        public Audio()
        {
            Name = "Audio";
            _defaultSettings = _default;
        }
    }
}

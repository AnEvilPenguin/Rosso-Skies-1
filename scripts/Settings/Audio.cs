using Newtonsoft.Json.Linq;

namespace RossoSkies1.scripts.Settings
{
    internal class Audio : Options
    {
        public Audio()
        {
            Name = "Audio";
            _defaultSettings = JObject.FromObject(this);
        }
    }
}

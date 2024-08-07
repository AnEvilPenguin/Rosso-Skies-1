using Newtonsoft.Json.Linq;

namespace RossoSkies1.scripts.Options
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

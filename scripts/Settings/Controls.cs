using Newtonsoft.Json.Linq;

namespace RossoSkies1.scripts.Settings
{
    internal class Controls : Options
    {

        public Controls()
        {
            Name = "Controls";
            _defaultSettings = JObject.FromObject(this);
        }
    }
}

using Newtonsoft.Json.Linq;

namespace RossoSkies1.scripts.Options
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

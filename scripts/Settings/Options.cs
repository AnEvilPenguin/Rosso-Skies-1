using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace RossoSkies1.scripts.Settings
{
    internal class Options : IEquatable<Options>
    {
        [NonSerializedAttribute]
        protected Options _defaultSettings;

        [NonSerializedAttribute]
        public string Name;

        public void OverrideSettings(JObject overrides)
        {
            if (overrides.ContainsKey(Name))
                JsonConvert.PopulateObject(overrides.GetValue(Name).ToString(), this);
        }

        public bool HasChanges() =>
            !_defaultSettings.Equals(this);

        public virtual JObject ToJObject() =>
            JObject.FromObject(this);

        public bool Equals(Options other) =>
            JToken.DeepEquals(other.ToJObject(), ToJObject());

        private JObject Reduce(JObject accumulator, JProperty current)
        {
            if (!HasChanges()) return accumulator;

            accumulator.Add(current);
            return accumulator;
        }
    }
}

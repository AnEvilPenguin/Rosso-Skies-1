using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using Godot;

namespace RossoSkies1.scripts.Settings
{
    internal class Options : IEquatable<Options>
    {
        [NonSerializedAttribute]
        protected Options _defaultSettings;

        [NonSerializedAttribute]
        public string Name;

        public int Columns { get; protected set; } = 2;

        public void OverrideSettings(JObject overrides)
        {
            if (overrides.ContainsKey(Name))
                JsonConvert.PopulateObject(overrides.GetValue(Name).ToString(), this);
        }

        public bool HasChanges() =>
            !_defaultSettings.Equals(this);

        public virtual List<Label> GetHeaders() =>
            new List<Label>();

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

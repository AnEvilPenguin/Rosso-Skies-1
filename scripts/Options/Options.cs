﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace RossoSkies1.scripts.Options
{
    internal class Options
    {
        protected JObject _defaultSettings;

        [NonSerializedAttribute]
        public string Name;

        public bool HasChanges()
        {
            var currentObject = JObject.FromObject(this);

            return !JToken.DeepEquals(_defaultSettings, currentObject);
        }

        public void OverrideSettings(JObject overrides)
        {
            if (overrides.ContainsKey(Name))
                JsonConvert.PopulateObject(overrides.GetValue(Name).ToString(), this);
        }

        public JObject GetChanges()
        {
            var jObject = new JObject();

            var currentObject = JObject.FromObject(this);

            if (JToken.DeepEquals(_defaultSettings, currentObject))
                return jObject;

            currentObject.Properties()
                .Where(p => p.Value != _defaultSettings.GetValue(p.Name))
                .Aggregate(jObject, Reduce);

            return jObject;
        }

        private JObject Reduce(JObject accumulator, JProperty current)
        {
            accumulator.Add(current);
            return accumulator;
        }
    }
}

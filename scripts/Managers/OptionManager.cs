using RossoSkies1.scripts.Settings;
using RossoSkies1.scripts.Util;
using System;
using System.IO;
using Godot;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace RossoSkies1.scripts.Managers
{
    partial class OptionManager : Node
    {
        public static OptionManager Instance { get; private set; }

        public static General General = new General();
        public static Controls Controls = new Controls();
        public static Audio Audio = new Audio();

        private static List<Options> _optionsList;

        private const string _fileName = "settings.json";

        public override void _Ready()
        {
            if (Instance != null)
                return;

            _optionsList = new List<Options>() { General, Controls, Audio };

            Instance = this;
            Load();

            PopulateInputMap();
        }

        public static List<Options> GetOptionsCategories() =>
            _optionsList.ToList();

        public static void Load()
        {
            string jsonString = String.Empty;

            var fullPath = Path.Combine(Constants.FolderPath, _fileName);

            if (!File.Exists(fullPath))
            {
                // Nothing to load, just use defaults
                return;
            }

            var settingOverrides = JObject.Parse(File.ReadAllText(fullPath));

            General.OverrideSettings(settingOverrides);
            Controls.OverrideSettings(settingOverrides);
            Audio.OverrideSettings(settingOverrides);
        }

        public static void Save()
        {
            // TODO deactivate apply button
            // Also need to enable it on changed signal.

            var changedSettings = new JObject();

            _optionsList.Aggregate(changedSettings, Reduce);

            if (changedSettings.Count == 0)
                return;

            var fullPath = Path.Combine(Constants.FolderPath, _fileName);

            using (StreamWriter file = File.CreateText(fullPath))
            using (JsonTextWriter writer = new JsonTextWriter(file) { Formatting = Formatting.Indented })
            {
                changedSettings.WriteTo(writer);
            }

            PopulateInputMap();
        }

        private static void PopulateSettingOverrides(JObject overrides, Object settings, string key)
        {
            if (overrides.ContainsKey(key))
                JsonConvert.PopulateObject(overrides.GetValue(key).ToString(), settings);
        }

        private static void PopulateInputMap()
        {
            InputMap.LoadFromProjectSettings();

            Controls.GetControls()
                .ForEach((control) => {
                    InputMap.AddAction(control.Name);

                    InputMap.ActionAddEvent(control.Name, control.KeyboardControl.ToInputEvent());
                    InputMap.ActionAddEvent(control.Name, control.ControllerControl.ToInputEvent());
                });
        }

        private static JObject Reduce(JObject accumulator, Options current)
        {
            if (current.HasChanges())
            {
                var changes = current.ToJObject();

                accumulator.Add(current.Name, changes);
            }

            return accumulator;
        }
    }
}

﻿using RossoSkies1.scripts.Options;
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

        private static List<Options.Options> optionsList;

        private const string _fileName = "settings.json";

        public override void _Ready()
        {
            if (Instance != null)
                return;

            optionsList = new List<Options.Options>() { General, Controls, Audio };

            Instance = this;
            Load();

            General.FullScreen = true;
        }

        public static void Load()
        {
            string jsonString = String.Empty;

            var fullPath = Path.Combine(Constants.FolderPath, _fileName);

            if (!File.Exists(fullPath))
            {
                // Nothing to load, just use defaults
            }

            var settingOverrides = JObject.Parse(File.ReadAllText(fullPath));

            General.OverrideSettings(settingOverrides);
            Controls.OverrideSettings(settingOverrides);
            Audio.OverrideSettings(settingOverrides);
        }

        private static void PopulateSettingOverrides(JObject overrides, Object settings, string key)
        {
            if (overrides.ContainsKey(key))
                JsonConvert.PopulateObject(overrides.GetValue(key).ToString(), settings);
        }

        public static void Save()
        {
            if (!optionsList.Exists(o => o.HasChanges()))
                return;

            var changedSettings = new JObject();

            optionsList.Where(o => o.HasChanges())
                .Aggregate(changedSettings, Reduce);

            var fullPath = Path.Combine(Constants.FolderPath, _fileName);

            using (StreamWriter file = File.CreateText(fullPath))
            using (JsonTextWriter writer = new JsonTextWriter(file) { Formatting = Formatting.Indented })
            {
                changedSettings.WriteTo(writer);
            }
        }

        private static JObject Reduce(JObject accumulator, Options.Options current)
        {
            var changes = current.GetChanges();

            accumulator.Add(current.Name, changes);

            return accumulator;
        }
    }
}

using System;
using System.Collections.Generic;
using Module.AnimationUtility.Editor.InternalUtility;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace Module.AnimationUtility.Editor.AnimatorParamGenerator
{
    internal class AnimatorControllerBindGeneratorSetting : ScriptableObject
    {
        public string namespaceName;
        [SelectFolder] public string outputFolder;
        public List<AnimatorSettingGroup> settingGroupList;

        public static AnimatorControllerBindGeneratorSetting LoadOrCreate()
        {
            const string path = "Assets/AnimatorParamGeneratorSettings.asset";
            var asset = AssetDatabase.LoadAssetAtPath<AnimatorControllerBindGeneratorSetting>(path);
            if (asset == null)
            {
                asset = CreateInstance<AnimatorControllerBindGeneratorSetting>();
                AssetDatabase.CreateAsset(asset, path);
                AssetDatabase.SaveAssets();
            }

            return asset;
        }
    }

    [Serializable]
    internal class AnimatorSettingGroup
    {
        public AnimatorController controller;
        public string className;
    }
}
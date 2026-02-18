using System;
using System.Collections.Generic;
using Module.AnimationUtility.Editor.InternalUtility;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace Module.AnimationUtility.Editor.BindingGenerator
{
    internal class AnimatorControllerBindingGeneratorSetting : ScriptableObject
    {
        public string namespaceName;
        [SelectFolder] public string outputFolder;
        public List<AnimatorSettingGroup> settingGroupList;

        public static AnimatorControllerBindingGeneratorSetting LoadOrCreate()
        {
            const string path = "Assets/" + nameof(AnimatorControllerBindingGeneratorSetting) + ".asset";
            var asset = AssetDatabase.LoadAssetAtPath<AnimatorControllerBindingGeneratorSetting>(path);
            if (asset == null)
            {
                asset = CreateInstance<AnimatorControllerBindingGeneratorSetting>();
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
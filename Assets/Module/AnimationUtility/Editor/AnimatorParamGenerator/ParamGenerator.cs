using UnityEditor;
using UnityEngine;

namespace Module.AnimationUtility.Editor.AnimatorParamGenerator
{
    /// <summary>
    /// AnimatorControllerで定義された`Parameter`の定数をフィールドに持つ
    /// 静的クラスを生成するエディターウィンドウ
    /// </summary>
    public class GeneratorWindow : EditorWindow
    {
        private AnimatorControllerBindingGeneratorSetting _settings;
        private Vector2 _scroll;

        [MenuItem("Tools/Animator Controller Binding Generator")]
        private static void Open()
        {
            var w = GetWindow<GeneratorWindow>("Animator Param Settings");
            w.minSize = new Vector2(480, 320);
        }


        private void OnEnable()
        {
            _settings = AnimatorControllerBindingGeneratorSetting.LoadOrCreate();
        }

        private void OnGUI()
        {
            if (_settings == null)
            {
                _settings = AnimatorControllerBindingGeneratorSetting.LoadOrCreate();
            }

            _scroll = EditorGUILayout.BeginScrollView(_scroll);

            EditorGUILayout.LabelField("Setting Asset", EditorStyles.boldLabel);
            _settings = (AnimatorControllerBindingGeneratorSetting)EditorGUILayout.ObjectField(
                _settings,
                typeof(AnimatorControllerBindingGeneratorSetting),
                false,
                null
            );

            if (GUILayout.Button("Generate"))
            {
                GenerationLogic.GenerateForGroup(_settings);
            }

            EditorGUILayout.EndScrollView();
        }
    }
}
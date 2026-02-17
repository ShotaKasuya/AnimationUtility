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
        private AnimatorControllerBindGeneratorSetting _settings;
        private Vector2 _scroll;

        [MenuItem("Tools/Animator Parameter Settings")]
        private static void Open()
        {
            var w = GetWindow<GeneratorWindow>("Animator Param Settings");
            w.minSize = new Vector2(480, 320);
        }


        private void OnEnable()
        {
            _settings = AnimatorControllerBindGeneratorSetting.LoadOrCreate();
        }

        private void OnGUI()
        {
            if (_settings == null)
            {
                _settings = AnimatorControllerBindGeneratorSetting.LoadOrCreate();
            }

            _scroll = EditorGUILayout.BeginScrollView(_scroll);

            EditorGUILayout.LabelField("Setting Asset", EditorStyles.boldLabel);
            _settings = (AnimatorControllerBindGeneratorSetting)EditorGUILayout.ObjectField(
                _settings,
                typeof(AnimatorControllerBindGeneratorSetting),
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
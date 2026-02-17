using System.IO;
using UnityEditor;
using UnityEngine;

namespace Module.AnimationUtility.Editor.InternalUtility
{
    public class SelectFolderAttribute : PropertyAttribute
    {
    }
    
    [CustomPropertyDrawer(typeof(SelectFolderAttribute))]
    public class SelectFolderDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // ラベル部分
            position = EditorGUI.PrefixLabel(position, label);

            // テキストフィールドとボタンの分割
            Rect textRect = new Rect(position.x, position.y, position.width - 25, position.height);
            Rect buttonRect = new Rect(position.x + position.width - 22, position.y, 22, position.height);

            // テキストフィールド表示
            EditorGUI.PropertyField(textRect, property, GUIContent.none);

            // ボタン
            if (GUI.Button(buttonRect, "…"))
            {
                string defaultFile = property.stringValue;

                // デフォルトファイル名決定
                if (string.IsNullOrEmpty(defaultFile))
                {
                    defaultFile = "Binding.cs";
                }

                string directory = Path.GetDirectoryName(defaultFile);
                if (string.IsNullOrEmpty(directory))
                {
                    directory = Application.dataPath;
                }

                var filePath = EditorUtility.OpenFolderPanel(
                    "Select Folder",
                    directory,
                    "AnimatorBinding"
                );

                if (!string.IsNullOrEmpty(filePath))
                {
                    if (filePath.StartsWith(Application.dataPath))
                    {
                        filePath = "Assets/" + filePath.Substring(Application.dataPath.Length + 1);
                    }

                    property.stringValue = filePath;
                }
            }
        }
    }
}
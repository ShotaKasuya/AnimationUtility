using Module.AnimationUtility.Runtime.ActionEditor;
using UnityEditor;
using UnityEngine;

namespace Module.AnimationUtility.Editor.ActionEditor
{
    [CustomEditor(typeof(ActionAsset))]
    public class ActionAssetEditor : UnityEditor.Editor
    {
        private const float TimelineHeight = 24f;
        private const float Padding = 4f;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawClipField();
            DrawTimeline();
            DrawSegmentListEditor();

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawClipField()
        {
            EditorGUILayout.PropertyField(
                serializedObject.FindProperty("clip")
            );
        }

        private void DrawTimeline()
        {
            var clipProp = serializedObject.FindProperty("clip");
            if (clipProp.objectReferenceValue == null) return;

            AnimationClip clip = (AnimationClip)clipProp.objectReferenceValue;
            var rect = GUILayoutUtility.GetRect(
                EditorGUIUtility.currentViewWidth - 20,
                TimelineHeight + Padding * 2
            );
            EditorGUI.DrawRect(rect, new Color(0.15f, 0.15f, 0.15f));

            DrawSegmentList(rect, clip.length);
        }

        private void DrawSegmentList(Rect timelineRect, float clipLength)
        {
            var segmentListProp = serializedObject.FindProperty("segmentList");

            for (int i = 0; i < segmentListProp.arraySize; i++)
            {
                var segment = segmentListProp.GetArrayElementAtIndex(i);

                var start = segment.FindPropertyRelative("startTime").floatValue;
                var end = segment.FindPropertyRelative("endTime").floatValue;

                float xMin = Mathf.Lerp(
                    timelineRect.x,
                    timelineRect.xMax,
                    start / clipLength);
                var xMax = Mathf.Lerp(
                    timelineRect.x, timelineRect.xMax,
                    end / clipLength);
                var bar = new Rect(
                    xMin, timelineRect.y + Padding,
                    xMax - xMin, TimelineHeight);

                EditorGUI.DrawRect(bar, GetStateColor(
                    (ActionStateType)segment.FindPropertyRelative("stateType").enumValueIndex));
            }
        }

        private Color GetStateColor(ActionStateType stateType)
        {
            return stateType switch
            {
                ActionStateType.Stunned => Color.red,
                ActionStateType.Cancellable => Color.yellow,
                // ActionStateType.Movable => Color.green,
                _ => Color.green
            };
        }

        private void DrawSegmentListEditor()
        {
            var segmentListProp = serializedObject.FindProperty("segmentList");
            var clip = (AnimationClip)serializedObject.FindProperty("clip").objectReferenceValue;

            if (clip == null)
            {
                EditorGUILayout.HelpBox("Animation Clip not found", MessageType.Warning);
                return;
            }

            EditorGUILayout.Space();

            for (int i = 0; i < segmentListProp.arraySize; i++)
            {
                var segment = segmentListProp.GetArrayElementAtIndex(i);

                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.PropertyField(segment.FindPropertyRelative("stateType"));

                var start = segment.FindPropertyRelative("startTime").floatValue;
                var end = segment.FindPropertyRelative("endTime").floatValue;

                EditorGUILayout.MinMaxSlider(
                    "Time", ref start, ref end, 0f, clip.length);

                segment.FindPropertyRelative("startTime").floatValue = start;
                segment.FindPropertyRelative("endTime").floatValue = end;

                if (GUILayout.Button("Remove"))
                {
                    segmentListProp.DeleteArrayElementAtIndex(i);
                    break;
                }

                EditorGUILayout.EndVertical();
            }

            if (GUILayout.Button("Add Section"))
            {
                segmentListProp.InsertArrayElementAtIndex(segmentListProp.arraySize);
            }
        }
    }
}
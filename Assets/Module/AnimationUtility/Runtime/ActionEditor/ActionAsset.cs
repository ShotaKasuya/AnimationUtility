using UnityEngine;

namespace Module.AnimationUtility.Runtime.ActionEditor
{
    [CreateAssetMenu(menuName = "Animation/Action Asset")]
    public class ActionAsset : ScriptableObject
    {
        [SerializeField] private AnimationClip clip;
        [SerializeField] private StateSegment[] segmentList;

        public string ClipName => clip.name;

        public ActionStateType Evaluate(float time)
        {
            foreach (var segment in segmentList)
            {
                if (segment.Contains(time))
                {
                    return segment.stateType;
                }
            }

            return ActionStateType.Movable;
        }
    }
}
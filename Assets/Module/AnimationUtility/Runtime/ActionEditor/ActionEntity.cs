using UnityEngine;

namespace Module.AnimationUtility.Runtime.ActionEditor
{
    public enum ActionStateType
    {
        // 硬直状態
        Stunned,

        // キャンセル可能状態
        Cancellable,

        // 移動可能状態
        Movable,
    }

    [System.Serializable]
    public struct StateSegment
    {
        public ActionStateType stateType;
        [Range(0, 1)] public float startTime;
        [Range(0, 1)] public float endTime;

        public bool Contains(float time)
            => time >= startTime && time <= endTime;
    }

}
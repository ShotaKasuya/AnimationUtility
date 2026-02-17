using System;
using Module.AnimationUtility.Runtime.ActionEditor;
using UnityEngine;

namespace Demo
{
    public class LogActionState : StateMachineBehaviour
    {
        [SerializeField] private ActionAsset actionAsset;
        private SpriteRenderer _spriteRenderer;
        private Color _colorBuf;

        public void SetRenderer(SpriteRenderer renderer)
        {
            _spriteRenderer = renderer;
        }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _colorBuf = _spriteRenderer.color;
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _spriteRenderer.color = _colorBuf;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var currentTime = stateInfo.normalizedTime * stateInfo.length;

            switch (actionAsset.Evaluate(currentTime))
            {
                case ActionStateType.Stunned:
                    _spriteRenderer.color = Color.red;
                    break;
                case ActionStateType.Cancellable:
                    _spriteRenderer.color = Color.yellow;
                    break;
                case ActionStateType.Movable:
                    _spriteRenderer.color = Color.green;
                    break;
            }
        }
    }
}
using UnityEngine;
using UnityEngine.InputSystem;

namespace Demo
{
    public class Invoker : MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            var selfRenderer = GetComponent<SpriteRenderer>();
            foreach (var logActionState in _animator.GetBehaviours<LogActionState>())
            {
                logActionState.SetRenderer(selfRenderer);
            }
            var playerInput = GetComponent<PlayerInput>();
            playerInput.actions["Left"].performed += _ => OnLeftInput();
            playerInput.actions["Right"].performed += _ => OnRightInput();
            playerInput.actions["Up"].performed += _ => OnUpInput();
            playerInput.actions["Down"].performed += _ => OnDownInput();
        }

        private void OnLeftInput()
        {
            _animator.SetTrigger(ControllerBind.Parameter.Left.ToHash());
        }

        private void OnRightInput()
        {
            _animator.SetTrigger(ControllerBind.Parameter.Right.ToHash());
        }

        private void OnUpInput()
        {
            _animator.SetTrigger(ControllerBind.Parameter.Up.ToHash());
        }

        private void OnDownInput()
        {
            _animator.SetTrigger(ControllerBind.Parameter.Down.ToHash());
        }
    }
}
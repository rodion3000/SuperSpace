using System;
using Project.Dev.GamePlay.AnimatorLogic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.Dev.GamePlay.NPC.Player
{
    public class PlayerAnimator : MonoBehaviour, IAnimatorStateReader
    {
        //Animation
        private static readonly int MoveHash = Animator.StringToHash("Move");
        private static readonly int AttackHash = Animator.StringToHash("Attack");

        //State Animation
        private readonly int _moveStateHash = Animator.StringToHash("Move");
        private readonly int _attackStateHash = Animator.StringToHash("Attack");

        [SerializeField] private Animator _animator;

        public AnimatorState State { get; private set; }

        private event Action<AnimatorState> EnterState;
        private event Action<AnimatorState> ExitState;

        [HorizontalGroup("Actions")]
        [Button("Move", ButtonStyle.FoldoutButton), GUIColor(0.5f, 0.3f, 0.8f)]
        public void MoveAnim(float velocity) =>
            _animator.SetFloat(MoveHash,velocity);

        [HorizontalGroup("Actions")]
        [Button("Attack"), GUIColor(0.7f, 0.7f, 0.8f)]
        public void AttackAnim() =>
            _animator.SetTrigger(AttackHash);
        private AnimatorState OnState(int stateHash)
        {
            AnimatorState state;

            if (stateHash == _moveStateHash) state = AnimatorState.Run;
            else if (stateHash == _attackStateHash) state = AnimatorState.Attack;
            else state = AnimatorState.Unknow;
            return state;

        }

        public void OnEnter(int stateHash)
        {
            State = OnState(stateHash);
            EnterState?.Invoke(State);
        }

        public void OnExit(int stateHash)
        {
            ExitState?.Invoke(State);
        }
    }
}

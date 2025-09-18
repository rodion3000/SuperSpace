using System;
using Project.Dev.GamePlay.AnimatorLogic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.Dev.GamePlay.NPC.Player1
{
    public class HeroAnimator : MonoBehaviour, IAnimatorStateReader
    {
        // animator params
        private static readonly int IdleHash = Animator.StringToHash("Idle");
        private static readonly int StrafeLeftHash = Animator.StringToHash("StrafeLeft");
        private static readonly int TurnLeftHash = Animator.StringToHash("TurnLeft5");
        private static readonly int TurnRightHash = Animator.StringToHash("TurnRight");
        private static readonly int WalkHash = Animator.StringToHash("Walk");

        //animator states
        private readonly int _idleStateHash = Animator.StringToHash("Idle");
        private readonly int _strafeLeftHash = Animator.StringToHash("StrafeLeft");
        private readonly int _walkingStateHash = Animator.StringToHash("Walk");
        private readonly int _turnLeftHash = Animator.StringToHash("TurnLeft5");
        private readonly int _turnRightHash = Animator.StringToHash("TurnRight");

        [SerializeField] private Animator animator;

        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;
        public AnimatorState State { get; private set; }

        #region Play methods

        [HorizontalGroup("Actions")] [Button("Idle", ButtonStyle.FoldoutButton), GUIColor(0.9f, 0.9f, 0.9f)]
        public void PlayIdle(bool idleState) =>
            animator.SetBool(IdleHash, idleState);

        [HorizontalGroup("Actions")]
        [Button ("StrafeLeft"), GUIColor(0,0,1)]
        public void PlayStrafeLeft() =>
            animator.SetTrigger(StrafeLeftHash);

        [HorizontalGroup("Actions")]
        [Button ("TurnLeft"), GUIColor(0.5f, 0.5f, 0)]
        public void PlayTurnLeft() =>
            animator.SetTrigger(TurnLeftHash);

        [HorizontalGroup("Actions")]
        [Button ("TurnRight"), GUIColor(1f, 0f, 0)]
        public void PlayTurnRight() =>
            animator.SetTrigger(TurnRightHash);

        [HorizontalGroup("Actions")]
        [Button("Walk"), GUIColor(0f, 1f, 0f)]
        public void PlayWalk(float velocity) =>
            animator.SetFloat(WalkHash, velocity);

        #endregion

        public void OnEnter(int stateHash)
        {
            State = StateFor(stateHash);
            StateEntered?.Invoke(State);
        }

        public void OnExit(int stateHash)
        {
            StateExited?.Invoke(State);
        }

        private AnimatorState StateFor(int stateHash)
        {
            AnimatorState state;

            if      (stateHash == _idleStateHash)    state = AnimatorState.Idle;
            else if (stateHash == _walkingStateHash)  state = AnimatorState.Walk;
            else if (stateHash == _turnRightHash)     state = AnimatorState.TurnRight;
            else if (stateHash == _turnLeftHash)   state = AnimatorState.TurnLeft;
            else if (stateHash == _strafeLeftHash)   state = AnimatorState.StrafeLeft;
            else                                     state = AnimatorState.Unknow;

            return state;
        }

    }
}

using Project.Dev.Services.InputService;
using UnityEngine;
using Zenject;

namespace Project.Dev.GamePlay.NPC.Player1
{
    public class HeroMove : MonoBehaviour
    {
        [SerializeField] private int movementSpeed;
        [SerializeField] private CharacterController characterController;
        [SerializeField] private HeroAnimator heroAnimator;
        private IInputService _inputService;
        private Camera _camera;

        [Inject]
        private void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        void Start()
        {
             _camera = Camera.main;
        }

        void Update()
        {
            Move();
        }

        private void Move()
        {
            var movementVector = Vector3.zero;

            if (_inputService.MoveAxis.sqrMagnitude > 0.001f)
            {
                movementVector = _camera.transform.TransformDirection(_inputService.MoveAxis);
                movementVector.y = 0;
                movementVector.Normalize();

                // Убрали transform.forward = movementVector;
            }

            movementVector += Physics.gravity;

            characterController.Move(movementVector * (movementSpeed * Time.deltaTime));
            heroAnimator.PlayWalk(characterController.velocity.magnitude);
        }
    }
}

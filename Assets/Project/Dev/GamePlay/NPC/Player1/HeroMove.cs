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
                var input = _inputService.MoveAxis;

                if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
                {
                    input.y = 0;
                }
                else
                {
                    input.x = 0;
                }

                movementVector = _camera.transform.TransformDirection(input);
                movementVector.y = 0;
                movementVector.Normalize();
            }

            movementVector += Physics.gravity;

            characterController.Move(movementVector * (movementSpeed * Time.deltaTime));

            Vector3 Speed = characterController.velocity;
            Speed.y = 0;
            if (Speed.sqrMagnitude > 0.001f)
            {
                Vector3 localMove = transform.InverseTransformDirection(Speed);
                float horizontalMove = localMove.x;
                float verticalMove = localMove.z;

                if (Mathf.Abs(horizontalMove) > Mathf.Abs(verticalMove))
                {
                    if (horizontalMove > 0)
                        heroAnimator.PlayStrafeRight(characterController.velocity.magnitude);
                    else
                        heroAnimator.PlayStrafeLeft(characterController.velocity.magnitude);
                }
                else
                {
                    if (verticalMove > 0)
                        heroAnimator.PlayWalk(characterController.velocity.magnitude);
                }
            }
            else
            {
                heroAnimator.PlayWalk(0);
                heroAnimator.PlayStrafeLeft(0);
                heroAnimator.PlayStrafeRight(0);
            }
        }
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

namespace SpellCaller
{
    /// <summary>
    /// Responsável pela execução dos comportamentos relacionados a movimentação do player
    /// </summary>
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Parâmetros Movimento")]
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _acceleration;
        [SerializeField] private float _deceleration;

        [Header("Parâmetros Pulo/Gravidade")]
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _groundGravity;
        [SerializeField] private float _gravityForce;

        [Header("Parâmetros HeadBob")]
        [SerializeField] private float _headBobIntensity;
        [SerializeField] private float _headBobSpeed;
        [SerializeField] private float _headBobStopDuration;

        [Header("Referências")]
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Animator _animator;
        [SerializeField] private InputActionReference _moveAction;
        [SerializeField] private InputActionReference _jumpAction;
        [SerializeField] private CameraShake _cameraShake;

        // Não serializadas
        private Vector2 _moveInput = Vector2.zero;
        private Vector3 _curSpeed = Vector3.zero;
        private float _curGravity;

        private void OnValidate() => _characterController = GetComponent<CharacterController>();

        private void OnEnable() => SetInputs(true);

        private void OnDisable() => SetInputs(false);

        private void Update()
        {
            ApplyMovement();
            ApplyGravity();
            HandleHeadBob();
            WalkAnimation();
        }

        #region Inputs

        private void SetInputs(bool value)
        {
            if (value)
            {
                _moveAction.action.performed += GetMoveInput;
                _moveAction.action.canceled += ResetMoveInput;
                _jumpAction.action.performed += GetJumpInput;
            }
            else
            {
                _moveAction.action.performed -= GetMoveInput;
                _moveAction.action.canceled -= ResetMoveInput;
                _jumpAction.action.performed -= GetJumpInput;
            }
        }

        private void GetMoveInput(InputAction.CallbackContext contextValue) => _moveInput = contextValue.ReadValue<Vector2>();
        private void ResetMoveInput(InputAction.CallbackContext contextValue) => _moveInput = Vector2.zero;
        private void GetJumpInput(InputAction.CallbackContext contextValue) => ApplyJump();

        #endregion

        #region Física

        private void ApplyMovement()
        {
            Vector3 targetHorizontalSpeed = (transform.right * _moveInput.x + transform.forward * _moveInput.y) * _maxSpeed;

            if (targetHorizontalSpeed.magnitude > 0.1f)
                _curSpeed = Vector3.Lerp(_curSpeed, targetHorizontalSpeed, _acceleration * Time.deltaTime);
            else
                _curSpeed = Vector3.Lerp(_curSpeed, Vector3.zero, _deceleration * Time.deltaTime);

            _characterController.Move(_curSpeed * Time.deltaTime);
        }

        private void ApplyJump()
        {
            if (_characterController.isGrounded)
                _curGravity = _jumpForce;
        }

        private void ApplyGravity()
        {
            if (_characterController.isGrounded && _curGravity < 0)
                _curGravity = _groundGravity;

            _curGravity += _gravityForce * Time.deltaTime;

            _characterController.Move(Vector3.up * _curGravity * Time.deltaTime);
        }

        public bool IsMoving()
        {
            return _curSpeed.magnitude > 0.01f;
        }

        private void HandleHeadBob()
        {
            if (IsMoving())
                _cameraShake.StartHeadBob(_headBobIntensity, _headBobSpeed);
            else
                _cameraShake.StopHeadBob(_headBobStopDuration);
        }

        private void WalkAnimation()
        {
            _animator.speed = IsMoving() ? 1f : 0f;
        }

        #endregion
    }
}

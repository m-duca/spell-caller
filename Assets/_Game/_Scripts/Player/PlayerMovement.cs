using UnityEngine;
using UnityEngine.InputSystem;

namespace Duca
{
    /// <summary>
    /// Responsável pela execução dos comportamentos relacionados a movimentação do player
    /// </summary>
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Parâmetros Movimento")]
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _acceleration = 10f;
        [SerializeField] private float _deceleration = 10f;

        [Header("Parâmetros Pulo/Gravidade")]
        [SerializeField] private float _jumpForce = 8f;
        [SerializeField] private float _groundGravity = -2f;
        [SerializeField] private float _gravityForce = -9.81f;

        [Header("Referências")]
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private InputActionReference _moveAction;
        [SerializeField] private InputActionReference _jumpAction;

        // Não serializadas
        private Vector2 _moveInput = Vector2.zero;
        private Vector3 _curHorizontalSpeed = Vector3.zero;
        private Vector3 _targetHorizontalSpeed = Vector3.zero;
        private float _curVerticalSpeed;

        private void OnValidate() => _characterController = GetComponent<CharacterController>();

        private void OnEnable() => SetInputs(true);

        private void OnDisable() => SetInputs(false);

        private void Update()
        {
            ApplyMovement();
            ApplyGravity();
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
            _targetHorizontalSpeed = new Vector3(_moveInput.x, 0f, _moveInput.y) * _moveSpeed;

            if (_targetHorizontalSpeed.magnitude > 0.1f)
                _curHorizontalSpeed = Vector3.Lerp(_curHorizontalSpeed, _targetHorizontalSpeed, _acceleration * Time.deltaTime);
            else
                _curHorizontalSpeed = Vector3.Lerp(_curHorizontalSpeed, Vector3.zero, _deceleration * Time.deltaTime);

            _characterController.Move(_curHorizontalSpeed * Time.deltaTime);
        }

        private void ApplyJump()
        {
            if (_characterController.isGrounded)
                _curVerticalSpeed = _jumpForce;
        }

        private void ApplyGravity()
        {
            if (_characterController.isGrounded && _curVerticalSpeed < 0)
                _curVerticalSpeed = _groundGravity;

            _curVerticalSpeed += _gravityForce * Time.deltaTime;

            _characterController.Move(Vector3.up * _curVerticalSpeed * Time.deltaTime);
        }

        #endregion
    }
}

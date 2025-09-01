using UnityEngine;
using UnityEngine.InputSystem;

namespace SpellCaller
{
    /// <summary>
    /// Controla a rotação da câmera filha do player
    /// </summary>
    public class RotateCamera : MonoBehaviour
    {
        [Header("Parâmetros")]
        [SerializeField] private float _sensitivity;
        [SerializeField] private float _acceleration;
        [SerializeField] private float _deceleration;
        [SerializeField] private float _minPitch;
        [SerializeField] private float _maxPitch;

        [Header("Referências")]
        [SerializeField] private Transform _playerTransf;
        [SerializeField] private InputActionReference _lookAction;

        // Não serializadas
        private float _xRotation;
        private Vector2 _lookInput;
        private Vector2 _currentLook;

        private void Start() => HideCursor();

        private void OnEnable() => _lookAction.action.Enable();
        
        private void OnDisable() => _lookAction.action.Disable();

        private void Update()
        {
            GetLookInput();
            ApplyRotation();
        }

        private void GetLookInput() => _lookInput = _lookAction.action.ReadValue<Vector2>();

        private void ApplyRotation()
        {
            if (_lookInput.sqrMagnitude > 0.01f)
                _currentLook = Vector2.Lerp(_currentLook, _lookInput, _acceleration * Time.deltaTime);
            else
                _currentLook = Vector2.Lerp(_currentLook, Vector2.zero, _deceleration * Time.deltaTime);

            // Rotação horizontal
            _playerTransf.Rotate(Vector3.up * _currentLook.x * _sensitivity);

            // Rotação vertical
            _xRotation -= _currentLook.y * _sensitivity;
            _xRotation = Mathf.Clamp(_xRotation, _minPitch, _maxPitch);

            transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        }

        private void HideCursor()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}

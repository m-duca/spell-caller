using UnityEngine;
using UnityEngine.InputSystem;

namespace SpellCaller
{
    /// <summary>
    /// Controla a rotação da câmera filha do player
    /// </summary>
    public class CameraRotation : MonoBehaviour
    {
        [Header("Parâmetros")]
        [SerializeField] private float _sensitivity;
        [SerializeField] private float _minPitch;
        [SerializeField] private float _maxPitch;

        [Header("Referências")]
        [SerializeField] private Transform _playerTransf;
        [SerializeField] private InputActionReference _lookAction;

        // Não serializadas
        private float _xRotation;
        private Vector2 _lookInput;

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
            // Rotação horizontal (player gira no eixo Y)
            _playerTransf.Rotate(Vector3.up * _lookInput.x * _sensitivity);

            // Rotação vertical
            _xRotation -= _lookInput.y * _sensitivity;
            _xRotation = Mathf.Clamp(_xRotation, _minPitch, _maxPitch);

            transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        }
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

namespace SpellCaller
{
    /// <summary>
    /// Controla a rotação da câmera filha do player
    /// e permite forçar o olhar em uma direção específica.
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
        private bool _forceLookActive;
        private Quaternion _targetPlayerRot;
        private Quaternion _targetCamRot;

        private void OnEnable() => _lookAction.action.Enable();
        private void OnDisable() => _lookAction.action.Disable();

        private void Update()
        {
            if (_forceLookActive)
            {
                ApplyForcedRotation();
                return;
            }

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

        public void ForceLookDirection(Vector3 direction)
        {
            direction.y = 0f;
            if (direction.sqrMagnitude < 0.01f)
                return;

            _targetPlayerRot = Quaternion.LookRotation(direction);

            Vector3 localDir = _playerTransf.InverseTransformDirection(direction);
            float pitch = Mathf.Asin(localDir.y) * Mathf.Rad2Deg;
            pitch = Mathf.Clamp(-pitch, _minPitch, _maxPitch);

            _targetCamRot = Quaternion.Euler(pitch, 0f, 0f);

            _forceLookActive = true;
        }

        private void ApplyForcedRotation()
        {
            _playerTransf.rotation = _targetPlayerRot;
            transform.localRotation = _targetCamRot;

            _xRotation = _targetCamRot.eulerAngles.x;

            _forceLookActive = false;
        }
    }
}

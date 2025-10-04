using UnityEngine;

namespace SpellCaller
{
    public class CameraManager : MonoBehaviour
    {
        public static CameraManager Instance { get; private set; }

        // Propriedades
        public CameraShake CameraShake { get { return _cameraShake; } }
        public CameraRotation CameraRotation {get { return _cameraRotation; } }

        // Não serializadas
        private CameraShake _cameraShake;
        private CameraRotation _cameraRotation;

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(gameObject);

            Instance = this;
            DontDestroyOnLoad(gameObject);

            _cameraShake = GetComponent<CameraShake>();
            _cameraRotation = GetComponent<CameraRotation>();
        }
    }
}

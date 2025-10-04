using UnityEngine;

namespace SpellCaller
{
    /// <summary>
    /// Ponte para acessar os sistemas relacionados ao player
    /// </summary>
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance { get; private set; }

        // Propriedades
        public PlayerMovement PlayerMovement { get { return _playerMovement; } }
        public PlayerSpells PlayerSpells { get { return _playerSpells; } }

        // Não serializadas
        private PlayerSpells _playerSpells;
        private PlayerMovement _playerMovement;

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(gameObject);

            Instance = this;
            DontDestroyOnLoad(gameObject);

            _playerMovement = GetComponent<PlayerMovement>();
            _playerSpells = GetComponent<PlayerSpells>();
        }

        public void SetPosition(Vector3 newPosValue) => gameObject.transform.position = newPosValue;
    }
}

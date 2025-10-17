using UnityEngine;

namespace SpellCaller
{
    /// <summary>
    /// Armazena as informações necessárias para castar uma spell
    /// </summary> <summary>
    [CreateAssetMenu(fileName = "_SpellData", menuName = "Scriptables/SpellData")]
    public class SpellData : ScriptableObject
    {
        [Header("Parâmetros")]
        [SerializeField] private Sprite _iconSpr;
        [SerializeField] private string _name;

        [Header("Spawn")]
        [SerializeField] private GameObject _effectPrefab;
        [SerializeField] private float _spawnCooldown;
        [SerializeField] private float _spawnDistance;
        [SerializeField] private float _spawnLifeTime;
        [SerializeField] private SpawnPointType _spawnPoint = SpawnPointType.Camera;

        [Header("Camera Shake")]
        [SerializeField] private float _shakeDelay;
        [SerializeField] private float _shakeIntensity;
        [SerializeField] private float _shakeDuration;

        [Header("UI Livro")]

        // Propriedades
        public Sprite IconSprite { get { return _iconSpr; } }
        public string Name { get { return _name; } }
        public float SpawnCooldown { get { return _spawnCooldown; } }
        public float ShakeDelay { get { return _shakeDelay; } }
        public float ShakeIntensity { get { return _shakeIntensity; } }
        public float ShakeDuration { get { return _shakeDuration; } }
        public SpawnPointType SpawnPoint { get { return _spawnPoint; } }

        public void Cast()
        {
            if (PlayerManager.Instance == null) return;

            PlayerSpells playerSpells = PlayerManager.Instance.PlayerSpells;

            if (!playerSpells.CanSpawn || playerSpells.GetCurrentSpell().Name != Name) return;

            Debug.Log($"<color=cyan>Lançando feitiço: {_name}</color>");

            playerSpells.SpawnSpell(this, _effectPrefab, _spawnDistance, _spawnLifeTime);
        }
    }
}
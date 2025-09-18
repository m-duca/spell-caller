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
        [SerializeField] private string _name;
        [SerializeField] private GameObject _effectPrefab;
        [SerializeField] private float _spawnDistance;

        // Propriedades
        public string Name { get { return _name; } }

        public void Cast()
        {
            if (PlayerManager.Instance == null) return;

            PlayerSpells playerSpells = PlayerManager.Instance.PlayerSpells;

            if (!playerSpells.CanSpawn || playerSpells.GetCurrentSpell().Name != Name) return;

            Debug.Log($"<color=cyan>Lançando feitiço: {_name}</color>");

            playerSpells.SpawnSpell(_effectPrefab, _spawnDistance);
        }
    }
}
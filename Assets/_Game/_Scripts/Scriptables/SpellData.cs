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

        // Propriedades
        public string Name { get { return _name; } }

        public void Cast()
        {
            if (PlayerManager.Instance == null) return;
            
            if (PlayerManager.Instance.PlayerSpells.GetCurrentSpell().name != Name) return;

            // TODO: Instanciar efeito no transform.forward do Player
            Debug.Log($"<color=cyan>Lançando feitiço: {_name}</color>");
        }
    }
}
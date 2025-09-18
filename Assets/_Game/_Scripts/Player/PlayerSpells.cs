using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SpellCaller
{
    /// <summary>
    /// Armazena os feitiços do jogador e faz a troca do atual
    /// </summary>
    public class PlayerSpells : MonoBehaviour
    {
        [Header("Troca")]
        [SerializeField] private int _curSpellIndex = 0;
        [SerializeField] private InputActionReference _changeSpellAction;
        [SerializeField] private float _changeDelay;

        [Header("Conjurar")]
        [SerializeField] private float _spawnDelay;

        [Header("Referências")]
        [SerializeField] private List<SpellData> _spellDatas;
        [SerializeField] private Transform _cameraTransform;

        // Propriedades
        public List<SpellData> SpellDatas { get { return _spellDatas; } }
        public bool CanSpawn { get { return _canSpawn; } }

        // Não serializadas
        private bool _isOnChangeDelay = false;
        private bool _canSpawn = false;

        private void OnEnable() => _changeSpellAction.action.performed += GetChangeInput;

        private void OnDisable() => _changeSpellAction.action.performed -= GetChangeInput;

        public SpellData GetCurrentSpell()
        {
            return _spellDatas[_curSpellIndex];
        }

        #region Trocar

        private void GetChangeInput(InputAction.CallbackContext contextValue)
        {
            float value = contextValue.ReadValue<Vector2>().y;

            if (value == 0 || _isOnChangeDelay) return;

            int newIncrement = value > 0 ? 1 : -1;

            _curSpellIndex = Mathf.Clamp(_curSpellIndex + newIncrement, 0, _spellDatas.Count - 1);

            _isOnChangeDelay = true;
            StartCoroutine(StopChangeDelay_Coroutine());
        }

        private IEnumerator StopChangeDelay_Coroutine()
        {
            yield return new WaitForSeconds(_changeDelay);
            _isOnChangeDelay = false;
        }

        #endregion


        #region Spawn

        public void SpawnSpell(GameObject prefabValue, float distanceValue)
        {
            Vector3 spawnPos = _cameraTransform.position + _cameraTransform.forward * distanceValue;

            Instantiate(prefabValue, spawnPos, prefabValue.transform.rotation);

            _canSpawn = false;
            StartCoroutine(ResetCanSpawn_Coroutine());
        }

        private IEnumerator ResetCanSpawn_Coroutine()
        {
            yield return new WaitForSeconds(_spawnDelay);
            _canSpawn = true;
        }
        
        #endregion
    }
}

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
        [Header("Debug")]
        [SerializeField] private bool _canForce;
        [SerializeField] private InputActionReference _forceCastAction;

        [Header("Troca")]
        [SerializeField] private int _curSpellIndex = 0;
        [SerializeField] private InputActionReference _changeSpellAction;
        [SerializeField] private float _changeDelay;

        [Header("Referências")]
        [SerializeField] private List<SpellData> _spellDatas;
        [SerializeField] private Transform _cameraTransform;

        // Propriedades
        public List<SpellData> SpellDatas { get { return _spellDatas; } }
        public bool CanSpawn { get { return _canSpawn; } }

        // Não serializadas
        private bool _isOnChangeDelay = false;
        private bool _canSpawn = true;

        private void OnEnable()
        {
            _changeSpellAction.action.performed += GetChangeInput;
            _forceCastAction.action.performed += ForceCastInput;
        }

        private void OnDisable()
        {
            _changeSpellAction.action.performed -= GetChangeInput;
            _forceCastAction.action.performed -= ForceCastInput;
        }

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

        public void SpawnSpell(SpellData dataValue, GameObject prefabValue, float distanceValue, float lifeTimeValue)
        {
            Vector3 spawnPos = _cameraTransform.position + _cameraTransform.forward * distanceValue;

            Destroy(Instantiate(prefabValue, spawnPos, _cameraTransform.rotation), lifeTimeValue);

            _canSpawn = false;
            StartCoroutine(ResetCanSpawn_Coroutine(dataValue.SpawnCooldown));

            CameraManager.Instance?.CameraShake.StartShake(dataValue.ShakeDelay, dataValue.ShakeIntensity, dataValue.ShakeDuration);
        }

        private IEnumerator ResetCanSpawn_Coroutine(float cooldownValue)
        {
            yield return new WaitForSeconds(cooldownValue);
            _canSpawn = true;
        }

        #endregion

        #region  Debug

        private void ForceCastInput(InputAction.CallbackContext contextValue)
        {
            if (!_canForce)
            {
                Debug.LogError("Force Cast desabilitado!");
                return;
            }

            GetCurrentSpell().Cast();
        }

        #endregion
    }
}

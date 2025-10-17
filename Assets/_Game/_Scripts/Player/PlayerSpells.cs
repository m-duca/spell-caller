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
        [SerializeField] private BookAnimationCaller _bookAnimationCaller;
        [SerializeField] private BookUI _bookUI;

        // Propriedades
        public List<SpellData> SpellDatas { get { return _spellDatas; } }
        public bool CanSpawn { get { return _canSpawn; } }

        // Não serializadas
        private bool _isOnChangeDelay = false;
        private bool _canSpawn = true;

        private void Start() => CallChangeBookUI();

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

            SwitchSpell(newIncrement);
        }

        private void SwitchSpell(int incrementValue)
        {
            _bookAnimationCaller.PlayFlip(incrementValue);
            Invoke(nameof(CallChangeBookUI), 1f);

            int newIndex = _curSpellIndex + incrementValue;

            if (newIndex > _spellDatas.Count - 1) newIndex = 0;
            else if (newIndex < 0) newIndex = _spellDatas.Count - 1;

            _curSpellIndex = newIndex;

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
            Quaternion spawnRotation = _cameraTransform.rotation;

            if (dataValue.SpawnPoint == SpawnPointType.Player)
            {
                spawnPos = transform.position + transform.forward * distanceValue;
                spawnRotation = transform.rotation;
            }

            Destroy(Instantiate(prefabValue, spawnPos, spawnRotation), lifeTimeValue);

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

        private void CallChangeBookUI() => _bookUI.ChangeContent(_spellDatas[_curSpellIndex]);
    }
}

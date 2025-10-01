using System.Collections;
using UnityEngine;

namespace SpellCaller
{
    public class TrainingDummyCollision : MonoBehaviour, IDamageable
    {
        [Header("Pârametros")]
        [SerializeField] private int _startLife;
        [SerializeField] private int _currentLife;

        // Não serializadas
        private bool _canApplyContinuosDamage = true;
        private Animator _animator;

        private const string ANIM_PARAM_DAMAGED = "_damaged";
        private const string ANIM_PARAM_ISDYING = "_isDying";

        private void Start()
        {
            _currentLife = _startLife;
            _animator = GetComponent<Animator>();
        }

        public void ApplyDamage(int damageValue, Vector3 positionValue)
        {
            _currentLife -= damageValue;

            _animator.SetTrigger(ANIM_PARAM_DAMAGED);

            if (_currentLife <= 0)
                Die();
        }

        public void ApplyContinuosDamage(int damageValue, float intervalValue, Vector3 positionValue)
        {
            if (!_canApplyContinuosDamage) return;

            ApplyDamage(damageValue, positionValue);

            _canApplyContinuosDamage = false;

            StartCoroutine(ResetCanApplyContinuosDamage_Coroutine(intervalValue));
        }

        private IEnumerator ResetCanApplyContinuosDamage_Coroutine(float intervalValue)
        {
            yield return new WaitForSeconds(intervalValue);

            _canApplyContinuosDamage = true;
        }

        // TODO: Fazer algum polimento / juiceness
        public void ForceDie() => Die();

        public void Die()
        {
            _animator.SetTrigger(ANIM_PARAM_ISDYING);
            Destroy(gameObject, 3f);
        }
    }
}

using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace SpellCaller
{
    public class TrainingDummyCollision : MonoBehaviour, IDamageable
    {
        [Header("Parâmetros")]
        [SerializeField] private int _startLife;
        [SerializeField] private int _currentLife;

        [Header("Feedback de Dano")]
        [SerializeField] private float _rotationDuration;
        [SerializeField] private float _rotationAngle;
        [SerializeField] private Color _damageColor;
        [SerializeField] private float _colorFlashDuration;

        // Não serializadas
        private bool _canApplyContinuosDamage = true;
        private Animator _animator;
        private bool _isDead;
        private Renderer _renderer;
        private Color _originalColor;

        private const string ANIM_PARAM_DAMAGED = "_damaged";
        private const string ANIM_PARAM_ISDYING = "_isDying";

        private void Start()
        {
            _currentLife = _startLife;
            _animator = GetComponent<Animator>();

            _renderer = GetComponentInChildren<Renderer>();
            if (_renderer != null)
                _originalColor = _renderer.material.color;
        }

        private void OnValidate() => _currentLife = _startLife;

        public void ApplyDamage(int damageValue, Vector3 positionValue)
        {
            if (_isDead) return;

            _currentLife -= damageValue;
            _animator.SetTrigger(ANIM_PARAM_DAMAGED);

            transform.DOKill(true);
            transform.DORotate(
                new Vector3(0f, _rotationAngle, 0f),
                _rotationDuration,
                RotateMode.LocalAxisAdd
            )
            .SetEase(Ease.OutBack);

            if (_renderer != null)
            {
                _renderer.material.DOColor(_damageColor, _colorFlashDuration)
                    .OnComplete(() =>
                        _renderer.material.DOColor(_originalColor, _colorFlashDuration)
                    );
            }

            if (_currentLife <= 0)
                Die();
        }

        public void ApplyContinuosDamage(int damageValue, float intervalValue, Vector3 positionValue)
        {
            if (!_canApplyContinuosDamage || _isDead) return;

            _canApplyContinuosDamage = false;
            ApplyDamage(damageValue, positionValue);
            StartCoroutine(ResetCanApplyContinuosDamage_Coroutine(intervalValue));
        }

        private IEnumerator ResetCanApplyContinuosDamage_Coroutine(float intervalValue)
        {
            yield return new WaitForSeconds(intervalValue);
            _canApplyContinuosDamage = true;
        }

        public void ForceDie() => Die();

        public void Die()
        {
            if (_isDead) return;
            _isDead = true;

            GetComponent<CapsuleCollider>().enabled = false;
            this.enabled = false;
            transform.DOKill(true);
            _animator.SetTrigger(ANIM_PARAM_ISDYING);
        }
    }
}

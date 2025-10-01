using UnityEngine;
using DG.Tweening;

namespace SpellCaller
{
    /// <summary>
    /// Executa o movimento de lançamento da bola de fogo
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class FireballMovement : MonoBehaviour
    {
        [Header("Parâmetros Movimento")]
        [SerializeField] private float _forwardForce;
        [SerializeField] private float _upwardForce;

        [Header("Parâmetros Rotação")]
        [SerializeField] private float _spinSpeed;

        // Não serializadas
        private Rigidbody _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();

            ApplyMovement();
            ApplyRotation();
        }

        private void ApplyMovement()
        {
            Vector3 launchDirection = transform.forward * _forwardForce + transform.up * _upwardForce;
            _rb.AddForce(launchDirection, ForceMode.Impulse);
        }

        private void ApplyRotation()
        {
            transform.DORotate(new Vector3(0, 0, 360f), 1f, RotateMode.FastBeyond360)
                     .SetEase(Ease.Linear)
                     .SetLoops(-1, LoopType.Restart);
        }

        private void OnDisable() => transform.DOKill();
    }
}

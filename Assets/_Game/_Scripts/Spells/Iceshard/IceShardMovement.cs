using UnityEngine;

namespace SpellCaller
{
    /// <summary>
    /// Executa o movimento de disparo do fragmento de gelo
    /// </summary>
    public class IceShardMovement : MonoBehaviour
    {
        [Header("Parâmetros")]
        [SerializeField] private float _moveSpeed;

        // Não serializadas
        private Rigidbody _rb;

        private void Start() => _rb = GetComponent<Rigidbody>();

        private void FixedUpdate() => ApplyMovement();

        private void ApplyMovement() => _rb.linearVelocity = transform.forward * _moveSpeed;
    }
}

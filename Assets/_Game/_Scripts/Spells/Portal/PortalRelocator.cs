using UnityEngine;

namespace SpellCaller
{
    /// <summary>
    /// Realoca automaticamente o portal caso ele comece dentro de algum objeto
    /// </summary>
    public class PortalAutoRelocator : MonoBehaviour
    {
        [Header("Parâmetros")]
        [SerializeField] private float _checkRadius = 1f;
        [SerializeField] private float _maxRelocateDistance = 5f;
        [SerializeField] private LayerMask _obstacleMask;
        [SerializeField] private int _maxAttempts = 15;

        // Não serializadas
        private Collider _col;

        private void Start()
        {
            _col = GetComponent<Collider>();

            if (IsInsideObstacle())
                TryRelocate();
        }

        private bool IsInsideObstacle()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, _checkRadius, _obstacleMask);
            foreach (var hit in hits)
            {
                if (hit != _col)
                    return true;
            }
            return false;
        }

        private void TryRelocate()
        {
            for (int i = 0; i < _maxAttempts; i++)
            {
                Vector3 randomDir = Random.insideUnitSphere;
                randomDir.y = 0f;

                Vector3 newPos = transform.position + randomDir.normalized * (_checkRadius * 1.5f);

                if (!Physics.CheckSphere(newPos, _checkRadius, _obstacleMask))
                {
                    transform.position = newPos;
                    return;
                }
            }
        }
    }
}

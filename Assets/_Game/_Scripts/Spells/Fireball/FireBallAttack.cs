using UnityEngine;

namespace SpellCaller
{
    /// <summary>
    /// Lida com o comportamento de ataque da bola de fogo (impacto e queimadura)
    /// </summary>
    public class FireBallAttack : MonoBehaviour
    {
        [Header("Pârametros")]
        [SerializeField] private int _impactDamage;
        [SerializeField] private int _burnDamage;
        [SerializeField] private float _burnInterval;

        [Header("Chamas")]
        [SerializeField] private float _flamesColRadius;
        [SerializeField] private float _flameRayOriginDistance;
        [SerializeField] private Vector3 _flamesSpawnOffset;

        [Header("Referências")]
        [SerializeField] private GameObject _vfxParent;
        [SerializeField] private GameObject _flamesVfxPrefab;

        // Não serializadas
        private GameObject _spawnedFlames;

        private void OnTriggerEnter(Collider triggerValue)
        {
            if (_spawnedFlames != null) return;

            ApplyImpactDamage(triggerValue.gameObject, triggerValue.ClosestPoint(transform.position));
            SpawnFlames();
        }

        private void OnTriggerStay(Collider triggerValue) => ApplyBurnDamage(triggerValue.gameObject, triggerValue.ClosestPoint(transform.position));

        private void OnDestroy() => Destroy(_spawnedFlames);

        private void ApplyImpactDamage(GameObject targetValue, Vector3 colPositionValue)
        {
            targetValue.GetComponent<IDamageable>()?.ApplyDamage(_impactDamage, colPositionValue);
        }

        private void ApplyBurnDamage(GameObject targetValue, Vector3 colPositionValue) => targetValue.GetComponent<IDamageable>()?.ApplyContinuosDamage(_burnDamage, _burnInterval, colPositionValue);

        private void SpawnFlames()
        {
            GetComponent<FireBallMovement>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            _vfxParent.SetActive(false);

            Vector3 rayOrigin = transform.position + Vector3.up;
            RaycastHit hit;

            if (Physics.Raycast(rayOrigin, Vector3.down, out hit, _flameRayOriginDistance))
            {
                Vector3 spawnPosition = hit.point + _flamesSpawnOffset;
                _spawnedFlames = Instantiate(_flamesVfxPrefab, spawnPosition, Quaternion.identity);
            }
            else
            {
                _spawnedFlames = Instantiate(_flamesVfxPrefab, transform.position + _flamesSpawnOffset, Quaternion.identity);
            }

            GetComponent<SphereCollider>().radius = _flamesColRadius;
        }

    }
}

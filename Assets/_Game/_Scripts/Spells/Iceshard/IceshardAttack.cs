using UnityEngine;

namespace SpellCaller
{
    /// <summary>
    /// Lida com o comportamento de ataque do fragmento de gelo (impacto)
    /// </summary>
    public class IceShardAttack : MonoBehaviour
    {
        [Header("Pârametros")]
        [SerializeField] private int _impactDamage;

        [Header("Efeito de Neve")]
        [SerializeField] private float _snowRayOriginDistance;
        [SerializeField] private Vector3 _snowSpawnOffset;

        [Header("Referências")]
        [SerializeField] private GameObject _snowVfxPrefab;

        private void OnTriggerEnter(Collider triggerValue)
        {
            ApplyImpactDamage(triggerValue);
            Destroy(gameObject);
        }

        private void ApplyImpactDamage(Collider colValue)
        {
            colValue.GetComponent<IDamageable>()?.ApplyDamage(_impactDamage, colValue.ClosestPoint(transform.position));
            SpawnSnow(colValue);
        }

        private void SpawnSnow(Collider colValue)
        {
            Bounds targetBounds = colValue.bounds;
            Vector3 spawnPosition = new Vector3(targetBounds.center.x, targetBounds.min.y, targetBounds.center.z) + _snowSpawnOffset;

            Vector3 rayOrigin = spawnPosition + Vector3.up * _snowRayOriginDistance;

            if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, _snowRayOriginDistance * 2f))
                spawnPosition = hit.point + _snowSpawnOffset;

            GameObject snow = Instantiate(_snowVfxPrefab, spawnPosition, _snowVfxPrefab.transform.rotation);
            Destroy(snow, snow.GetComponent<ParticleSystem>().main.duration);
        }
    }
}

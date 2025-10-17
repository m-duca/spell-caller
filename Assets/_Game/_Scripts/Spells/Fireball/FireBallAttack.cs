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

        [Header("Efeito de Chamas")]
        [SerializeField] private float _flamesColRadius;
        [SerializeField] private float _flamesRayOriginDistance;
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
            SpawnFlames(triggerValue);
        }

        private void OnTriggerStay(Collider triggerValue) => ApplyBurnDamage(triggerValue.gameObject, triggerValue.ClosestPoint(transform.position));

        private void OnDestroy() => Destroy(_spawnedFlames);

        private void ApplyImpactDamage(GameObject targetValue, Vector3 colPositionValue)
        {
            targetValue.GetComponent<IDamageable>()?.ApplyDamage(_impactDamage, colPositionValue);
        }

        private void ApplyBurnDamage(GameObject targetValue, Vector3 colPositionValue) => targetValue.GetComponent<IDamageable>()?.ApplyContinuosDamage(_burnDamage, _burnInterval, colPositionValue);

        private void SpawnFlames(Collider colValue)
        {
            GetComponent<FireBallMovement>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            _vfxParent.SetActive(false);

            Bounds targetBounds = colValue.bounds;
            Vector3 spawnPosition = new Vector3(targetBounds.center.x, targetBounds.min.y, targetBounds.center.z) + _flamesSpawnOffset;

            Vector3 rayOrigin = spawnPosition + Vector3.up * _flamesRayOriginDistance;

            if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, _flamesRayOriginDistance * 2f))
                spawnPosition = hit.point + _flamesSpawnOffset;

            GameObject fireball = Instantiate(_flamesVfxPrefab, spawnPosition, _flamesVfxPrefab.transform.rotation);
            Destroy(fireball, fireball.GetComponent<ParticleSystem>().main.duration);

            GetComponent<SphereCollider>().radius = _flamesColRadius;
        }

    }
}

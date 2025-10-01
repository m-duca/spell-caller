using UnityEngine;

namespace SpellCaller
{
    /// <summary>
    /// Lida com o comportamento de ataque da bola de fogo (impacto e queimadura)
    /// </summary>
    public class FireballAttack : MonoBehaviour
    {
        [Header("Pârametros")]
        [SerializeField] private int _impactDamage;
        [SerializeField] private int _burnDamage;
        [SerializeField] private float _burnInterval;

        [Header("Chamas")]
        [SerializeField] private float _flamesColRadius;
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

        private void OnCollisionEnter(Collision colValue)
        {
            if (_spawnedFlames != null) return;

            ApplyImpactDamage(colValue.gameObject, colValue.contacts[0].point);
            SpawnFlames();
        }

        private void OnCollisionStay(Collision colValue) => ApplyBurnDamage(colValue.gameObject, colValue.contacts[0].point);

        private void OnDestroy() => Destroy(_spawnedFlames);

        private void ApplyImpactDamage(GameObject targetValue, Vector3 colPositionValue)
        {
            targetValue.GetComponent<IDamageable>()?.ApplyDamage(_impactDamage, colPositionValue);
        }

        private void ApplyBurnDamage(GameObject targetValue, Vector3 colPositionValue) => targetValue.GetComponent<IDamageable>()?.ApplyContinuosDamage(_burnDamage, _burnInterval, colPositionValue);

        private void SpawnFlames()
        {
            GetComponent<FireballMovement>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;

            _vfxParent.SetActive(false);
            _spawnedFlames = Instantiate(_flamesVfxPrefab, transform.position + _flamesSpawnOffset, _flamesVfxPrefab.transform.rotation);

            GetComponent<SphereCollider>().radius = _flamesColRadius;
        }
    }
}

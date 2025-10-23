using System.Collections.Generic;
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
        [SerializeField] private float _lifeTime;

        [Header("Efeito de Chamas")]
        [SerializeField] private float _flamesColRadius;
        [SerializeField] private float _flamesRayOriginDistance;
        [SerializeField] private Vector3 _flamesSpawnOffset;

        [Header("Referências")]
        [SerializeField] private GameObject _vfxParent;
        [SerializeField] private GameObject _flamesVfxPrefab;

        // Não serializadas
        private List<GameObject> _flames = new ();
        private bool _spawnedFlames = false;

        private void OnTriggerEnter(Collider triggerValue)
        {
            if (_spawnedFlames) return;

            ApplyImpactDamage(triggerValue.gameObject, triggerValue.ClosestPoint(transform.position));
            SpawnFlames(triggerValue);
        }

        private void OnTriggerStay(Collider triggerValue) => ApplyBurnDamage(triggerValue.gameObject, triggerValue.ClosestPoint(transform.position));

        private void OnDestroy()
        {
            foreach (GameObject flame in _flames) 
                Destroy(flame);
        }

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

            Vector3 spawnPosition = transform.position;

            if (colValue.gameObject.GetComponent<IDamageable>() != null)
            {
                Bounds targetBounds = colValue.bounds;
                spawnPosition = new Vector3(targetBounds.center.x, targetBounds.min.y, targetBounds.center.z) + _flamesSpawnOffset;
            }

            Vector3 rayOrigin = spawnPosition + Vector3.up * _flamesRayOriginDistance;

            if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, _flamesRayOriginDistance * 2f))
                spawnPosition = hit.point + _flamesSpawnOffset;

            _flames.Add(Instantiate(_flamesVfxPrefab, spawnPosition, _flamesVfxPrefab.transform.rotation));
            Destroy(gameObject, _lifeTime);

            GetComponent<SphereCollider>().radius = _flamesColRadius;

            _spawnedFlames = true;
        }

    }
}

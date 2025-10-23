using System.Collections.Generic;
using UnityEngine;

namespace SpellCaller
{
    /// <summary>
    /// Lida com o comportamento de ataque do trovão (impacto)
    /// </summary>
    public class ThunderBoltAttack : MonoBehaviour
    {
        [Header("Pârametros")]
        [SerializeField] private int _impactDamage;
        [SerializeField] private float _searchRadius;

        [Header("Referências")]
        [SerializeField] private GameObject _thunderVfxPrefab;

        // Não serializadas
        private List<GameObject> _thunders = new();

        private void Start() => FindTargets();

        private void FindTargets()
        {
            Collider[] targets = Physics.OverlapSphere(transform.position, _searchRadius);

            foreach (Collider col in targets)
            {
                IDamageable damageable = col.gameObject.GetComponent<IDamageable>();

                if (damageable == null || col.gameObject.layer == LayerMask.NameToLayer("Player"))
                    continue;

                damageable.ApplyDamage(_impactDamage, col.gameObject.transform.position);

                SpawnThunder(col);
            }
        }

        private void SpawnThunder(Collider colValue)
        {
            Vector3 spawnPosition = colValue.bounds.center;

            _thunders.Add(Instantiate(_thunderVfxPrefab, spawnPosition, _thunderVfxPrefab.transform.rotation));
        }

        private void OnDestroy()
        {
            foreach (GameObject thunder in _thunders) Destroy(thunder);
        }
    }
}

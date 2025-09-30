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

        [Header("Referências")]
        [SerializeField] private GameObject _impactVfxParent;
        [SerializeField] private GameObject _flamesVfxParent;

        private void OnTriggerEnter(Collider triggerValue)
        {
            ApplyImpactDamage(triggerValue.gameObject, triggerValue.ClosestPoint(transform.position));
            EnableBurn();
        }

        private void OnTriggerStay(Collider triggerValue) => ApplyBurnDamage(triggerValue.gameObject, triggerValue.ClosestPoint(transform.position));


        private void OnCollisionEnter(Collision colValue)
        {
            ApplyImpactDamage(colValue.gameObject, colValue.contacts[0].point);
            EnableBurn();
        }

        private void OnCollisionStay(Collision colValue) => ApplyBurnDamage(colValue.gameObject, colValue.contacts[0].point);

        private void ApplyImpactDamage(GameObject targetValue, Vector3 colPositionValue)
        {
            if (!_impactVfxParent.active) return;

            targetValue.GetComponent<IDamageable>()?.ApplyDamage(_impactDamage, colPositionValue);

        }

        private void ApplyBurnDamage(GameObject targetValue, Vector3 colPositionValue) => targetValue.GetComponent<IDamageable>()?.ApplyContinuosDamage(_burnDamage, _burnInterval, colPositionValue);

        private void EnableBurn()
        {
            GetComponent<FireballMovement>().enabled = false;

            _impactVfxParent.SetActive(false);

            _flamesVfxParent.SetActive(true);
        }
    }
}

using Unity.VisualScripting;
using UnityEngine;

namespace SpellCaller
{
    /// <summary>
    /// Responsavel por acionar todos os comportamentos de colisão entre o player / outro objeto 
    /// </summary>
    public class PlayerCollision : MonoBehaviour
    {
        #region Triggers

        private void OnTriggerEnter(Collider triggerValue) => triggerValue.gameObject.GetComponent<ITriggerable>()?.CallTriggerEnter();

        private void OnTriggerExit(Collider triggerValue) => triggerValue.gameObject.GetComponent<ITriggerable>()?.CallTriggerExit();

        private void OnTriggerStay(Collider triggerValue) => triggerValue.gameObject.GetComponent<ITriggerable>()?.CallTriggerStay();

        #endregion

        #region Colliders

        private void OnCollisionEnter(Collision colValue) => colValue.gameObject.GetComponent<ICollideable>()?.CallCollisionEnter();

        private void OnCollisionExit(Collision colValue) => colValue.gameObject.GetComponent<ICollideable>()?.CallCollisionExit();

        private void CollisionStay(Collision colValue) => colValue.gameObject.GetComponent<ICollideable>()?.CallCollisionStay();

        #endregion
    }
}
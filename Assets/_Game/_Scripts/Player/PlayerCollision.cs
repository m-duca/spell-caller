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

        private void OnTriggerEnter(Collider triggerValue)
        {
            if (triggerValue.gameObject.TryGetComponent<ITriggerable>(out ITriggerable trigger))
                trigger.CallTriggerEnter();
        }

        private void OnTriggerExit(Collider triggerValue)
        {
            if (triggerValue.gameObject.TryGetComponent<ITriggerable>(out ITriggerable trigger))
                trigger.CallTriggerExit();
        }

        private void OnTriggerStay(Collider triggerValue)
        {
            if (triggerValue.gameObject.TryGetComponent<ITriggerable>(out ITriggerable trigger))
                trigger.CallTriggerStay();
        }

        #endregion

        #region Colliders

        private void OnCollisionEnter(Collision colValue)
        {
            if (colValue.gameObject.TryGetComponent<ICollideable>(out ICollideable collider))
                collider.CallCollisionEnter();
        }

        private void OnCollisionExit(Collision colValue)
        {
            if (colValue.gameObject.TryGetComponent<ICollideable>(out ICollideable collider))
                collider.CallCollisionExit();
        }

        private void CollisionStay(Collision colValue)
        {
            if (colValue.gameObject.TryGetComponent<ICollideable>(out ICollideable collider))
                collider.CallCollisionStay();
        }

        #endregion
    }
}
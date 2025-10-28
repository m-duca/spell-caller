using FMODUnity;
using UnityEngine;

namespace SpellCaller
{
    /// <summary>
    /// Lógica de acionamento dos teleportes entre portais
    /// </summary>
    public class PortalTeleport : MonoBehaviour, ITriggerable
    {
        [Header("Pârametros")]
        [SerializeField] private float _playerRealocateDistance;

        [Header("SFX")]
        [SerializeField] private EventReference _teleportEventReference;

        private void Start() => CheckPortalsOnScene();

        public void CallTriggerEnter() => TeleportPlayer();

        public void CallTriggerExit() { }

        public void CallTriggerStay() { }

        private void CheckPortalsOnScene()
        {
            PortalTeleport[] portals = FindObjectsByType<PortalTeleport>(FindObjectsSortMode.InstanceID);

            if (portals.Length > 2)
            {
                foreach (PortalTeleport portal in portals)
                {
                    if (portal == this) continue;

                    Destroy(portal.gameObject);
                }
            }
        }

        private void TeleportPlayer()
        {
            PortalTeleport[] portals = FindObjectsByType<PortalTeleport>(FindObjectsSortMode.None);

            foreach (PortalTeleport portal in portals)
            {
                if (portal == this) continue;

                Transform portalTransform = portal.gameObject.transform;

                Vector3 newPlayerPos = portalTransform.position + PlayerManager.Instance.transform.forward * _playerRealocateDistance;
                PlayerManager.Instance?.SetPosition(newPlayerPos);
                CameraManager.Instance?.CameraRotation.ForceLookDirection(portalTransform.forward);
                AudioManager.Instance?.PlayOneShot(_teleportEventReference, transform.position);
            }
        }
    }
}

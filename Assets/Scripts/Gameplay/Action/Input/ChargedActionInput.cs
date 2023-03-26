using UnityEngine;

namespace Unity.MobaRPG.Gameplay.Actions
{
    public class ChargedActionInput : BaseActionInput
    {
        protected float m_StartTime;
        const float k_MouseInputRaycastDistance = 100f;
        // Cache raycast hit array so that we can use non alloc raycasts
        readonly RaycastHit[] k_CachedHit = new RaycastHit[4];

        private void Start()
        {
            Camera camera = Camera.main;

            // Get the cursor position in the world for something to look at
            var ray = camera.ScreenPointToRay(UnityEngine.Input.mousePosition);
            Physics.RaycastNonAlloc(
                ray,
                k_CachedHit,
                k_MouseInputRaycastDistance,
                LayerMask.GetMask(new[] { "Ground" })
            );

            var position = k_CachedHit[0].point;

            // get our particle near the right spot!
            transform.position = m_Origin;

            m_StartTime = Time.time;
            
            var data = new ActionRequestData
            {
                Position = position,
                ActionID = m_ActionPrototypeID,
                ShouldQueue = false,
                TargetIds = null,
            };

            m_SendInput(data);
        }

        public override void OnReleaseKey()
        {
            m_PlayerOwner.RecvStopChargingUpServerRpc();
            Destroy(gameObject);
        }
    }
}

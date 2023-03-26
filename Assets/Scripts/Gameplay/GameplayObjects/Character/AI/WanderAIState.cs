using System;
using System.Collections.Generic;
using Unity.MobaRPG.Gameplay.Actions;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;
using Action = Unity.MobaRPG.Gameplay.Actions.Action;
using Random = UnityEngine.Random;

namespace Unity.MobaRPG.Gameplay.GameplayObjects.Character.AI
{
    public class WanderAIState : AIState
    {
        private AIBrain m_Brain;
        private ServerCharacter m_ServerCharacter;
        private Action m_CurrWanderAction;
        private Vector3 m_StartingPosition;
        private ServerCharacterMovement m_ServerCharacterMovement;

        private const float k_WanderRadius = 5f;
        private const float k_WanderTimer = 4f;
        private float m_Timer = float.MaxValue;

        // Cache the wanderable positions to limit the position calculations
        private List<Vector3> m_ValidPositions = new List<Vector3>();

        public WanderAIState(AIBrain brain, Vector3 startingPosition, ServerCharacterMovement serverCharacterMovement)
        {
            m_Brain = brain;
            m_StartingPosition = startingPosition;
            m_ServerCharacterMovement = serverCharacterMovement;
        }

        public override bool IsEligible()
        {
            return m_Brain.GetHatedEnemies().Count == 0;
        }

        public override void Initialize()
        {
            PopulatePositions();
        }

        public override void Update()
        {
            m_Timer += Time.deltaTime;

            if (m_Timer >= k_WanderTimer)
            {
                if (m_ValidPositions.Count == 0)
                {
                    PopulatePositions();
                }

                Vector3? newPos = RandomPosition();
                if (newPos != null)
                {
                    Debug.Log($"Setting new Wander Position to {newPos}");
                    m_ServerCharacterMovement.SetMovementTarget((Vector3)newPos);
                }
                m_Timer = 0;
            }

            DetectFoes();
        }

        Vector3? RandomPoint(Vector3 center, float range, int layerMask)
        {
            Debug.Log($"RandomPosition starting point {center}");
            NavMeshHit navHit;
            for (int i = 0; i < 30; i++)
            {
                Vector3 randomPoint = center + Random.insideUnitSphere * range;
                randomPoint.y = 0;
                Debug.Log($"Checking random point at: {randomPoint}");
                if (NavMesh.SamplePosition (randomPoint, out navHit, range, layerMask))
                {
                    NavMeshPath path = new NavMeshPath();
                    m_ServerCharacterMovement.NavMeshAgent.CalculatePath(navHit.position, path);
                    Debug.Log($"Path status: {path.status}");
                    if (path.status == NavMeshPathStatus.PathComplete)
                    {
                        Debug.DrawRay(navHit.position, Vector3.up, Color.blue, 30f);
                        Debug.Log($"RandomPosition found point {navHit.position}");
                        return navHit.position;
                    }
                }
            }

            Debug.Log("RandomPoint failed to find a valid position");
            return null;
        }

        void PopulatePositions()
        {
            var validPositions = new List<Vector3>();
            // Generate 10 random positions inside the wander area for this unit
            for (int i = 0; i < 10; i++)
            {
                var randomPoint = RandomPoint(m_StartingPosition, k_WanderRadius, -1);
                if (randomPoint != null)
                {
                    validPositions.Add((Vector3)randomPoint);
                }
            }

            m_ValidPositions = validPositions;
        }

        Vector3? RandomPosition()
        {
            if (m_ValidPositions.Count == 0) { return null; }
            int index = Random.Range(0, m_ValidPositions.Count);
            return m_ValidPositions[index];
        }

        protected void DetectFoes() 
        {
            float detectionRange = m_Brain.BrainShutoffRange;
            // we are doing this check every Update, so we'll use square-magnitude distance to avoid the expensive srt (that's implicit in Vector3.magnitude)
            float detectionRangeSqr = detectionRange * detectionRange;
            Vector3 position = m_Brain.GetMyServerCharacter().physicsWrapper.Transform.position;

            // in this game, NPCs only attack players (and never other NPCs), so we can just iterate over the players to see if any are nearby
            foreach (var character in PlayerServerCharacter.GetPlayerServerCharacters())
            {
                if (m_Brain.IsAppropriateFoe(character) && (character.physicsWrapper.Transform.position - position).sqrMagnitude <= detectionRangeSqr)
                {
                    m_Brain.Hate(character);
                }
            }
        }
    }
}

using UnityEngine.AI;

namespace HonestMistake.Utils
{
    public static class NavMeshAgentExtensions
    {
        public static bool DestinationReached(this NavMeshAgent agent)
        {
            if (agent.pathPending)
                return false;

            if (agent.remainingDistance > agent.stoppingDistance)
                return false;

            if (agent.hasPath && agent.velocity.sqrMagnitude > 0.0f)
                return false;

            if (agent.isStopped)
                return false;

            return true;
        }
    }
}
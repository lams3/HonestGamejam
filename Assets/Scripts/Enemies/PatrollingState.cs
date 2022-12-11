using UnityEngine;

namespace HonestMistake.Enemies
{

    [CreateAssetMenu(menuName="HonestMistake/EnemyStates/Patrolling")]
    public class PatrollingState : State<Enemy>
    {
        [SerializeField] private float patrollingRadius = 5.0f;
        [SerializeField] private float viewingDistanceStanding = 12.0f;
        [SerializeField] private float viewingDistanceCrouching = 5.0f;
        [SerializeField] private float fieldOfView = 75.0f;
        [SerializeField] private State<Enemy> seeSomethingTransition;


        private class InstanceVariables
        {
            public Vector3 patrollingCenter;
        }

        public override void OnStateEnter(Enemy caller)
        {
            caller.StateStorage[this] = new InstanceVariables
            {
                patrollingCenter = caller.transform.position
            };
        }

        public override State<Enemy> Execute(Enemy caller)
        {
            var instanceVars = (InstanceVariables) caller.StateStorage[this];
            float viewingDistance = Detectable.Instance.crouching ? viewingDistanceCrouching : viewingDistanceStanding;

            if (caller.CanSee(Detectable.Instance, viewingDistance, fieldOfView))
                return seeSomethingTransition;

            if (caller.DestinationReached())
                caller.SetDestination(instanceVars.patrollingCenter, patrollingRadius);

            return this;
        }

        public override void OnStateExit(Enemy caller)
        {
            caller.StateStorage.Remove(this);
        }

        public override void OnDrawGizmosSelected(Enemy caller)
        {
            base.OnDrawGizmosSelected(caller);

            float viewingDistance = Detectable.Instance.crouching ? viewingDistanceCrouching : viewingDistanceStanding;

            Mesh mesh = new Mesh();
            mesh.vertices = new Vector3[3]
            {
                caller.EyeTransform.position,
                caller.EyeTransform.position + Quaternion.Euler(0.0f, -fieldOfView / 2.0f, 0.0f) * caller.EyeTransform.forward * viewingDistance,
                caller.EyeTransform.position + Quaternion.Euler(0.0f, fieldOfView / 2.0f, 0.0f) * caller.EyeTransform.forward * viewingDistance,
            };
            mesh.normals = new Vector3[3]
            {
                Vector3.up,
                Vector3.up,
                Vector3.up
            };
            mesh.triangles = new int[3] { 0, 1, 2};

            Gizmos.color = new Color(1.0f, 1.0f, 0.0f, 0.3f);
            Gizmos.DrawMesh(mesh);
        }
    }
}

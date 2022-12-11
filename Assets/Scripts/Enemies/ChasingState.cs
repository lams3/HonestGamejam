using UnityEngine;

namespace HonestMistake.Enemies
{
    [CreateAssetMenu(menuName = "HonestMistake/EnemyStates/Chasing")]
    public class ChasingState : State<Enemy>
    {
        [SerializeField] private float viewingDistanceStanding = 20.0f;
        [SerializeField] private float viewingDistanceCrouching = 20.0f;
        [SerializeField] private float fieldOfView = 179.0f;
        [SerializeField] private State<Enemy> outOfSightTransition;

        public override void OnStateEnter(Enemy caller)
        {
        }

        public override State<Enemy> Execute(Enemy caller)
        {
            float viewingDistance = Detectable.Instance.crouching ? viewingDistanceCrouching : viewingDistanceStanding;

            caller.SetDestination(Detectable.Instance.transform.position);

            if (!caller.CanSee(Detectable.Instance, viewingDistance, fieldOfView))
                return outOfSightTransition;

            return this;
        }

        public override void OnStateExit(Enemy caller)
        {
        }
    }
}

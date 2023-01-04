using HonestMistake.Utils;
using UnityEngine;

namespace HonestMistake.Enemies
{

    [CreateAssetMenu(menuName = "HonestMistake/EnemyStates/Searching")]
    public class SearchingState : State<Enemy>
    {
        [SerializeField] private float viewingDistanceStanding = 15.0f;
        [SerializeField] private float viewingDistanceCrouching = 7.5f;
        [SerializeField] private float fieldOfView = 120.0f;
        [SerializeField] private float targetSpottingError = 2.0f;
        [SerializeField] private string animatorTriggerName = string.Empty;
        [SerializeField] private State<Enemy> seeNothingTransition;
        [SerializeField] private State<Enemy> seeSomethingTransition;

        public override void OnStateEnter(Enemy caller)
        {
            caller.WarningIcon.SetActive(true);
            caller.SetDestination(caller.transform.position, targetSpottingError);
            caller.Animator.SetTrigger(animatorTriggerName);
        }

        public override State<Enemy> Execute(Enemy caller)
        {
            float viewingDistance = Detectable.Instance.crouching ? viewingDistanceCrouching : viewingDistanceStanding;

            if (caller.CanSee(Detectable.Instance, viewingDistance, fieldOfView))
                return seeSomethingTransition;

            if (caller.DestinationReached())
                return seeNothingTransition;

            return this;
        }

        public override void OnStateExit(Enemy caller)
        {
            caller.WarningIcon.SetActive(false);
            caller.StateStorage.Remove(this);
        }

        public override void OnDrawGizmosSelected(Enemy caller)
        {
            base.OnDrawGizmosSelected(caller);

            float viewingDistance = Detectable.Instance.crouching ? viewingDistanceCrouching : viewingDistanceStanding;

            Gizmos.color = new Color(1.0f, 0.1f, 0.0f, 0.3f);
            GizmosExtensions.DrawWireArc(caller.EyeTransform.position, caller.EyeTransform.forward, fieldOfView, viewingDistance);
        }
    }
}

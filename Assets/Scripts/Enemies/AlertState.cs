using HonestMistake.Utils;
using UnityEngine;

namespace HonestMistake.Enemies
{
    [CreateAssetMenu(menuName = "HonestMistake/EnemyStates/Alert")]
    public class AlertState : State<Enemy>
    {
        [SerializeField] private float timeout = 1.0f;
        [SerializeField] private float timeToSee = 0.5f;
        [SerializeField] private float viewingDistanceStanding = 12.0f;
        [SerializeField] private float viewingDistanceCrouching = 5.0f;
        [SerializeField] private float fieldOfView = 90.0f;
        [SerializeField] private string animatorTriggerName = string.Empty;
        [SerializeField] private State<Enemy> seeSomethingTransition;
        [SerializeField] private State<Enemy> timeoutTransition;

        private class InstanceVariables
        {
            public float startTime;
            public float timeInSight;
        }

        public override void OnStateEnter(Enemy caller)
        {
            caller.StateStorage[this] = new InstanceVariables
            {
                startTime = Time.time,
                timeInSight = 0.0f,
            };

            caller.Stop();
            
            caller.WarningIcon.SetActive(true);
            caller.LookAt(Detectable.Instance.transform.position);
            caller.Animator.SetTrigger(animatorTriggerName);
        }

        public override State<Enemy> Execute(Enemy caller)
        {
            var instanceVars = (InstanceVariables) caller.StateStorage[this];

            float viewingDistance = Detectable.Instance.crouching ? viewingDistanceCrouching : viewingDistanceStanding;

            if (caller.CanSee(Detectable.Instance, viewingDistance, fieldOfView))
                instanceVars.timeInSight += Time.deltaTime;

            if (instanceVars.timeInSight >= timeToSee)
                return seeSomethingTransition;

            if (Time.time - instanceVars.startTime >= timeout)
                return timeoutTransition;

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

            Gizmos.color = new Color(1.0f, 0.3f, 0.0f, 0.3f);
            GizmosExtensions.DrawWireArc(caller.EyeTransform.position, caller.EyeTransform.forward, fieldOfView, viewingDistance);
        }
    }
}

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
                timeInSight = 0.0f
            };

            caller.WarningIcon.SetActive(true);
            caller.Stop();
            
            var callerToTarget = Detectable.Instance.transform.position - caller.transform.position;
            caller.transform.forward = Vector3.ProjectOnPlane(callerToTarget, Vector3.up).normalized;
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
    }
}

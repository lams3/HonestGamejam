using HonestMistake.Utils;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace HonestMistake.Enemies
{
    public class Enemy : MonoBehaviour, IStateMachine<Enemy>
    {
        [SerializeReference] private State<Enemy> state;
        [SerializeField] private Transform eyeTransform;
        [SerializeField] private GameObject warningIcon;

        private Dictionary<State<Enemy>, object> stateStorage = new Dictionary<State<Enemy>, object>();

        public Transform EyeTransform => eyeTransform;
        public GameObject WarningIcon => warningIcon;
        public Dictionary<State<Enemy>, object> StateStorage => stateStorage;
        
        
        private NavMeshAgent agent;
        private Animator animator;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            state.OnStateEnter(this);
        }

        private void Update()
        {
            animator.SetFloat("MotionSpeed", 1.0f);
            animator.SetFloat("Speed", agent.velocity.magnitude);
            
            var prevState = state;
            state = state.Execute(this);
            if (prevState != state)
            {
                prevState.OnStateExit(this);
                state.OnStateEnter(this);
            }
        }

        private void OnDrawGizmos()
        {
            state.OnDrawGizmosSelected(this);
        }

        public void SetDestination(Vector3 target, float error = 0.0f)
        {
            Vector2 randomInCircle = Random.insideUnitCircle;
            Vector3 offset = error * new Vector3(randomInCircle.x, 0.0f, randomInCircle.y);
            agent.SetDestination(target + offset);
        }

        public bool CanSee(Detectable detectable, float viewingDistance, float fieldOfView)
        {
            RaycastHit hit = new RaycastHit();
            Vector3 callerToTarget = detectable.transform.position - EyeTransform.position;

            bool targetInRange = callerToTarget.magnitude <= viewingDistance;
            bool targetInFov = targetInRange && Vector3.Angle(EyeTransform.forward, callerToTarget) <= fieldOfView;
            bool somethingInSight = targetInFov && Physics.Raycast(EyeTransform.position, callerToTarget, out hit, viewingDistance, Physics.AllLayers);
            bool targetInSight = somethingInSight && hit.collider.transform.IsChildOf(Detectable.Instance.transform);

            return targetInSight;
        }

        public void Stop()
        {
            agent.isStopped = true;
            agent.ResetPath();
        }

        public bool DestinationReached()
        {
            return agent.DestinationReached();
        }

        private void OnFootstep()
        {
            return;
        }
    }
}

using UnityEngine;

namespace HonestMistake.Enemies
{
    public abstract class State<T> : ScriptableObject where T : IStateMachine<T>
    {
        public abstract void OnStateEnter(T caller);

        public abstract State<T> Execute(T caller);

        public abstract void OnStateExit(T caller);

        public virtual void OnDrawGizmosSelected(T caller) { }
    }
}

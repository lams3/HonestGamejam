using System.Collections.Generic;

namespace HonestMistake.Enemies
{
    public interface IStateMachine<T> where T : IStateMachine<T>
    {
        public Dictionary<State<T>, object> StateStorage { get; }
    }
}

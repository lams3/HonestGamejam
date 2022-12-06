namespace HonestMistake.Interactable
{
    public interface IInteractableHandler
    {
        public void OnInteracted();

#if UNITY_EDITOR
        public void DebugResetCollect();
#endif
    }
}
namespace SpellCaller
{
    /// <summary>
    /// Interface utilizada para lidar com acionamentos de comportamentos que dependem de trigger
    /// </summary> <summary>
    public interface ITriggerable
    {
        void CallTriggerEnter();
        void CallTriggerExit();
        void CallTriggerStay();
    }
}

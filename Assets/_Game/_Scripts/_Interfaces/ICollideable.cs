namespace SpellCaller
{
    /// <summary>
    /// Interface que lida com acionamentos de comportamentos que dependem de colliders
    /// </summary>
    public interface ICollideable
    {
        void CallCollisionEnter();
        void CallCollisionExit();
        void CallCollisionStay();
    }
}

public interface IBehaviourTree
{
    TaskStatus CurrentStatus { get; }

    TaskStatus Tick();
}
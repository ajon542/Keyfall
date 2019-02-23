
public class BehaviourTree : IBehaviourTree
{
    public TaskStatus CurrentStatus { get; private set; }

    public BehaviourTree()
    {
        CurrentStatus = TaskStatus.Incomplete;
    }

    public TaskStatus Tick()
    {
        CurrentStatus = TaskStatus.Success;
        return CurrentStatus;
    }
}
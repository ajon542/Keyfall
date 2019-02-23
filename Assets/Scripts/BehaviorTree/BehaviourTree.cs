
public class BehaviourTree : IBehaviourTree
{
    public TaskStatus CurrentStatus { get; private set; }

    private ITask _task;

    public BehaviourTree(ITask task)
    {
        CurrentStatus = TaskStatus.Incomplete;
        _task = task;
    }

    public TaskStatus Tick()
    {
        CurrentStatus = _task.Tick();
        return CurrentStatus;
    }
}
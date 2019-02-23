public class SucceederTask : ITask
{
    private readonly ITask _child;

    public SucceederTask(ITask child)
    {
        _child = child;
    }

    public TaskStatus Tick()
    {
        var childStatus = _child.Tick();

        if (childStatus == TaskStatus.Incomplete)
        {
            return TaskStatus.Incomplete;
        }

        return TaskStatus.Success;
    }
}
